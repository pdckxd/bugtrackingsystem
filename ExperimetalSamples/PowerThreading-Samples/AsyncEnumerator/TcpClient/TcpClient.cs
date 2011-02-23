/******************************************************************************
Module:  TcpClient.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Wintellect.Threading.AsyncProgModel;


///////////////////////////////////////////////////////////////////////////////


public static partial class Program {
   public static void Main() {
      const String server = "localhost";

      ServerConnectionSync(server, "This is text sent synchronously");
      Console.WriteLine();

      new ServerConnectionApm(server, "This is text sent using the APM");
      Thread.Sleep(5000);
      Console.WriteLine();

      ServerConnectionEnumerator.Start(server, "This is text sent using an enumerator");
      Thread.Sleep(5000);

      Console.ReadLine();
   }

   private static void ServerConnectionSync(String server, String message) {
      TcpClient client = new TcpClient(server, 12000);
      Stream stream = client.GetStream();
      Console.WriteLine("Sent={0}", message);
      Byte[] StringData = Encoding.UTF8.GetBytes(message);

      // Create an array with the length followed by the data
      Byte[] data = new Byte[1 + StringData.Length];
      data[0] = (Byte)StringData.Length;
      Array.Copy(StringData, 0, data, 1, StringData.Length);

      // Send the request to the server
      stream.Write(data, 0, data.Length);

      // Read the result from the server
      Int32 numBytesRead = stream.Read(data, 0, 1);
      if (numBytesRead == 0) {
         Console.WriteLine("Server closed connection.");
         data = null;
      } else {
         // Read the rest of the data
         Array.Resize(ref data, 1 + data[0]);
         for (Int32 bytesReadSoFar = 0; bytesReadSoFar < data.Length - 1; ) {
            Int32 bytesReadThisTime = stream.Read(data, 1 + bytesReadSoFar,
               data.Length - (bytesReadSoFar + 1));
            if (bytesReadThisTime == 0) {
               data = null;
               break;
            }
            bytesReadSoFar += bytesReadThisTime;
         }
         String result = Encoding.UTF8.GetString(data, 1, data.Length - 1);
         Console.WriteLine("   Received={0}", result);
      }

      if (data == null) {
         Console.WriteLine("Server closed connection.");
      }
      client.Close();
   }
}


///////////////////////////////////////////////////////////////////////////////


public static partial class Program {
   private sealed class ServerConnectionApm {
      private TcpClient m_client;
      private Stream m_stream;
      private Byte[] m_inputData = new Byte[1 + 255];
      private Int32 m_bytesReadSoFar = 0;

      public ServerConnectionApm(String server, String message) {
         m_client = new TcpClient(server, 12000);
         m_stream = m_client.GetStream();
         Byte[] outputString = Encoding.UTF8.GetBytes(message);
         Byte[] outputData = new Byte[1 + outputString.Length];
         outputData[0] = (Byte)outputString.Length;
         Array.Copy(outputString, 0, outputData, 1, outputString.Length);

         m_stream.BeginWrite(outputData, 0, outputData.Length,
            WriteDataCompleted, null);
      }

      private void WriteDataCompleted(IAsyncResult result) {
         m_stream.EndWrite(result);
         m_stream.BeginRead(m_inputData, 0, 1, ReadLengthCompleted, null);
      }

      private void ReadLengthCompleted(IAsyncResult result) {
         // Server closed connection
         if (m_stream.EndRead(result) == 0) { m_client.Close(); return; }

         // Start to read 'length' bytes of data from server
         Int32 dataLength = m_inputData[0];
         Array.Resize(ref m_inputData, 1 + dataLength);
         m_stream.BeginRead(m_inputData, 1, dataLength, ReadDataCompleted, null);
      }

      private void ReadDataCompleted(IAsyncResult ar) {
         // Get number of bytes read from server
         Int32 numBytesReadThisTime = m_stream.EndRead(ar);

         // Server closed connection
         if (numBytesReadThisTime == 0) { m_client.Close(); return; }

         // Continue to read bytes from server until all bytes are in
         m_bytesReadSoFar += (Byte)numBytesReadThisTime;
         if (m_bytesReadSoFar < m_inputData.Length - 1) {
            m_stream.BeginRead(m_inputData, 1 + m_bytesReadSoFar,
               m_inputData.Length - (m_bytesReadSoFar + 1), ReadDataCompleted, null);
            return;
         }

         // All bytes have been read, Show the server's result
         String result = Encoding.UTF8.GetString(m_inputData, 1, m_inputData[0]);
         Console.WriteLine("Received={0}", result);
         m_client.Close();
      }
   }
}

///////////////////////////////////////////////////////////////////////////////


public static partial class Program {
   private static class ServerConnectionEnumerator {
      public static void Start(String server, String message) {
         AsyncEnumerator ae = new AsyncEnumerator();
         ae.BeginExecute(Process(ae, server, message), ae.EndExecute, null);
      }

      private static IEnumerator<Int32> Process(AsyncEnumerator ae, String server, String message) {
         TcpClient client = new TcpClient(server, 12000);
         Stream stream = client.GetStream();
         Byte[] outputString = Encoding.UTF8.GetBytes(message);
         Byte[] outputData = new Byte[1 + outputString.Length];
         outputData[0] = (Byte)outputString.Length;
         Array.Copy(outputString, 0, outputData, 1, outputString.Length);

         stream.BeginWrite(outputData, 0, outputData.Length, ae.End(), null);
         yield return 1;

         stream.EndWrite(ae.DequeueAsyncResult());

         Byte[] inputData = new Byte[1 + 255];
         stream.BeginRead(inputData, 0, 1, ae.End(), null);
         yield return 1;

         // Server closed connection
         if (stream.EndRead(ae.DequeueAsyncResult()) == 0) { client.Close(); yield break; }

         // Start to read 'length' bytes of data from server
         Int32 dataLength = inputData[0];
         Array.Resize(ref inputData, 1 + dataLength);

         Int32 bytesReadSoFar = 0;
         while (bytesReadSoFar < dataLength) {
            stream.BeginRead(inputData, 1, dataLength, ae.End(), null);
            yield return 1;

            // Get number of bytes read from server
            Int32 numBytesReadThisTime = stream.EndRead(ae.DequeueAsyncResult());

            // Server closed connection
            if (numBytesReadThisTime == 0) { client.Close(); yield break; }

            // Continue to read bytes from server until all bytes are in
            bytesReadSoFar += (Byte)numBytesReadThisTime;

         }

         // All bytes have been read, Show the server's result
         String result = Encoding.UTF8.GetString(inputData, 1, inputData[0]);
         Console.WriteLine("Received={0}", result);
         client.Close();
      }
   }
}


//////////////////////////////// End of File //////////////////////////////////
