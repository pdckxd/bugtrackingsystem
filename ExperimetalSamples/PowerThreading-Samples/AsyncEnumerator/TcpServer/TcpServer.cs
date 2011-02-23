/******************************************************************************
Module:  TcpServer.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Wintellect.Threading.AsyncProgModel;


///////////////////////////////////////////////////////////////////////////////


public static partial class TcpServer {
   public static void Main() {
      // Setup listener on "localhost" port 12000
      IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0];
      TcpListener server = new TcpListener(ipAddr, 12000);
      server.Start(); // Network driver can now allow incoming requests 

      // Accept up to 1 client per CPU simultaneously
      Int32 numConcurrentClients = Environment.ProcessorCount;

#if false
      for (Int32 n = 0; n < numConcurrentClients; n++)
         new ClientConnectionApm(server);
#elif true
      for (Int32 n = 0; n < numConcurrentClients; n++)
         ClientConnectionEnumerator.Start(server);
#else
      ClientConnectionSyncStart(server);
      // NOTE: Code below won't ever execute because the server CANNOT be stopped cleanly.
#endif

      // NOTE: The server CAN be stopped cleanly when doing asynchronous operations
      Console.WriteLine("Hit Enter to terminate the server.");
      Console.ReadLine();
   }

   private static void ClientConnectionSyncStart(TcpListener server) {
      Console.WriteLine("Hit Ctrl+C to terminate the server.");
      while (true) ClientConnectionSync(server);
      // NOTE: Code below won't ever execute; the server CANNOT be stopped cleanly.
   }

   private static void ClientConnectionSync(TcpListener server) {
      using (TcpClient client = server.AcceptTcpClient()) {
         using (Stream stream = client.GetStream()) {
            Byte[] inputData = new Byte[1];
            Int32 numBytesRead = stream.Read(inputData, 0, 1);
            if (numBytesRead == 0) {
               // Client closed connection; abandon this client request
               inputData = null;
            } else {
               // Resize the array to 1 bytes (for length) + the size of the data
               Array.Resize(ref inputData, 1 + inputData[0]);

               // Read in all the data (after the length byte)
               for (Int32 bytesReadSoFar = 0; bytesReadSoFar < inputData.Length - 1; ) {
                  Int32 bytesReadThisTime = stream.Read(inputData, 1 + bytesReadSoFar,
                     inputData.Length - (bytesReadSoFar + 1));
                  if (bytesReadThisTime == 0) {
                     // Client closed connection; abandon this client request
                     inputData = null;
                     break;
                  }
                  bytesReadSoFar += bytesReadThisTime;
               }
               // Process the data and return the result
               Byte[] outputData = ProcessData(inputData);
               stream.Write(outputData, 0, outputData.Length);
            }
         }
      }
   }

   private static Byte[] ProcessData(Byte[] inputData) {
      String inputString = Encoding.UTF8.GetString(inputData, 1, inputData[0]);
      String outputString = inputString.ToUpperInvariant();

      Console.WriteLine("Input={0}", inputString);
      Console.WriteLine("   Output={0}", outputString);
      Console.WriteLine();

      Byte[] outputStringBytes = Encoding.UTF8.GetBytes(outputString);
      Byte[] outputData = new Byte[1 + outputStringBytes.Length];
      outputData[0] = (Byte)outputStringBytes.Length;
      Array.Copy(outputStringBytes, 0, outputData, 1, outputStringBytes.Length);
      return outputData;
   }
}


///////////////////////////////////////////////////////////////////////////////


public static partial class TcpServer {
   private sealed class ClientConnectionApm {
      private TcpListener m_server;
      private TcpClient m_client;
      private Stream m_stream;
      private Byte[] m_inputData = new Byte[1];
      private Byte m_bytesReadSoFar = 0;

      public ClientConnectionApm(TcpListener server) {
         m_server = server;
         m_server.BeginAcceptTcpClient(AcceptCompleted, null);
      }

      private void AcceptCompleted(IAsyncResult ar) {
         // Connect to this client
         m_client = m_server.EndAcceptTcpClient(ar);

         // Accept another client
         new ClientConnectionApm(m_server);

         // Start processing this client
         m_stream = m_client.GetStream();
         // Read 1 byte from client which contains length of additional data
         m_stream.BeginRead(m_inputData, 0, 1, ReadLengthCompleted, null);
      }

      private void ReadLengthCompleted(IAsyncResult result) {
         // If client closed connection; abandon this client request
         if (m_stream.EndRead(result) == 0) { m_client.Close(); return; }

         // Start to read 'length' bytes of data from client
         Int32 dataLength = m_inputData[0];
         Array.Resize(ref m_inputData, 1 + dataLength);
         m_stream.BeginRead(m_inputData, 1, dataLength, ReadDataCompleted, null);
      }


      private void ReadDataCompleted(IAsyncResult ar) {
         // Get number of bytes read from client
         Int32 numBytesReadThisTime = m_stream.EndRead(ar);

         // If client closed connection; abandon this client request
         if (numBytesReadThisTime == 0) { m_client.Close(); return; }

         // Continue to read bytes from client until all bytes are in
         m_bytesReadSoFar += (Byte)numBytesReadThisTime;
         if (m_bytesReadSoFar < m_inputData.Length - 1) {
            m_stream.BeginRead(m_inputData, 1 + m_bytesReadSoFar,
               m_inputData.Length - (m_bytesReadSoFar + 1), ReadDataCompleted, null);
            return;
         }

         // All bytes have been read, process the input data
         Byte[] outputData = ProcessData(m_inputData);
         m_inputData = null;  // Allow early GC

         // Write outputData back to client
         m_stream.BeginWrite(outputData, 0, outputData.Length,
            WriteDataCompleted, null);
      }

      private void WriteDataCompleted(IAsyncResult ar) {
         // After result is written to client, close the connection
         m_stream.EndWrite(ar);
         m_client.Close();
      }
   }
}


///////////////////////////////////////////////////////////////////////////////


public static partial class TcpServer {
   private static class ClientConnectionEnumerator {
      public static void Start(TcpListener server) {
         AsyncEnumerator ae = new AsyncEnumerator();
         ae.BeginExecute(Process(ae, server), ae.EndExecute, null);
      }

      private static IEnumerator<Int32> Process(AsyncEnumerator ae, TcpListener server) {
         server.BeginAcceptTcpClient(ae.End(), null);
         yield return 1;

         using (TcpClient client = server.EndAcceptTcpClient(ae.DequeueAsyncResult())) {

            Start(server);  // Accept another client

            using (Stream stream = client.GetStream()) {
               // Read 1 byte from client which contains length of additional data
               Byte[] inputData = new Byte[1];
               stream.BeginRead(inputData, 0, 1, ae.End(), null);
               yield return 1;

               // Client closed connection; abandon this client request
               if (stream.EndRead(ae.DequeueAsyncResult()) == 0) yield break;

               // Start to read 'length' bytes of data from client
               Int32 dataLength = inputData[0];
               Array.Resize(ref inputData, 1 + dataLength);

               for (Byte bytesReadSoFar = 0; bytesReadSoFar < dataLength; ) {
                  stream.BeginRead(inputData, 1 + bytesReadSoFar,
                     inputData.Length - (bytesReadSoFar + 1), ae.End(), null);
                  yield return 1;

                  // Get number of bytes read from client
                  Int32 numBytesReadThisTime = stream.EndRead(ae.DequeueAsyncResult());
                  // Client closed connection; abandon this client request
                  if (numBytesReadThisTime == 0) yield break;

                  // Continue to read bytes from client until all bytes are in
                  bytesReadSoFar += (Byte)numBytesReadThisTime;
               }

               // All bytes have been read, process the input data
               Byte[] outputData = ProcessData(inputData);
               inputData = null;  // Allow early GC

               // Write outputData back to client
               stream.BeginWrite(outputData, 0, outputData.Length, ae.End(), null);
               yield return 1;

               stream.EndWrite(ae.DequeueAsyncResult());
            }
         }
      }
   }
}


//////////////////////////////// End of File //////////////////////////////////
/* The comments/code below used to be at the end of Main:
 * // Stop disposes 'server' causing pending I/Os to complete. When EndXxx
 * // is called, ObjectDisposedException is thrown - this should be fixed
 * server.Stop();
 */
