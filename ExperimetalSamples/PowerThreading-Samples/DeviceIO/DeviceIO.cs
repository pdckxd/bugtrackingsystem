/******************************************************************************
Module:  DeviceIO.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.IO;
using System.Text;
using System.Threading;
using Wintellect.IO;
//using Wintellect.IO.ChangeJournal;


///////////////////////////////////////////////////////////////////////////////


internal static class Program {
   public static void Main() {
      // Video Brightness Sample
      LCDBrightnessForMsdnMagazine();

      // Opportunistic Lock Sample
      OpLockDemo.Demo();

#if false
      // Change Journal Sample
      String s = ChangeJournal.GetVolumeNameForVolumeMountPoint(@"C:\");
      using (ChangeJournal cj = new ChangeJournal('C', true)) {
			foreach (UsnRecord r in cj) 
            Console.WriteLine(r);

         foreach (UsnRecord r in cj.GetFilteredRecords(0, ChangeReason.All, true, 1024)) {
            String s2 = Path.GetExtension(r.Name);
            if (String.Equals(Path.GetExtension(r.Name), ".txt", StringComparison.OrdinalIgnoreCase))
               Console.WriteLine("{0} {1} {2}", r.Name, r.TimestampUtc, r.Reason);
         }
      }
#endif
   }

   #region Video Brightness
   private static void LCDBrightnessForMsdnMagazine() {
      DeviceControlCode s_SetBrightness =
         new DeviceControlCode(DeviceType.Video, 0x127, DeviceMethod.Buffered, DeviceAccess.Any);

      using (DeviceIO lcd = new DeviceIO(@"\\.\LCD", FileAccess.ReadWrite, FileShare.ReadWrite, false)) {
         for (Int32 times = 0; times < 10; times++) {
            for (Int32 dim = 0; dim <= 100; dim += 20) {
               Win32DisplayBrightnessStructure db = new Win32DisplayBrightnessStructure(Win32DisplayBrightnessStructure.DisplayPowerFlags.ACDC,
                  (Byte)((times % 2 == 0) ? dim : 100 - dim));
               lcd.Control(s_SetBrightness, db);
               Thread.Sleep(150);
            }
         }
      }
   }

   private struct Win32DisplayBrightnessStructure {
      [Flags]
      public enum DisplayPowerFlags : byte {
         None = 0x00000000, AC = 0x00000001, DC = 0x00000002, ACDC = AC | DC
      }

      public DisplayPowerFlags m_Power;
      public Byte m_ACBrightness; // 0-100
      public Byte m_DCBrightness; // 0-100

      public Win32DisplayBrightnessStructure(DisplayPowerFlags power, Byte level) {
         m_Power = power;
         m_ACBrightness = m_DCBrightness = level;
      }
   }

   private static void FuncTest_VideoBrightness() {
      using (DisplayBrightness displayBrightness = new DisplayBrightness()) {
         Byte acBrightness, dcBrightness;
         DisplayPowerFlags f = displayBrightness.GetLevel(out acBrightness, out dcBrightness);

         Byte[] levels = displayBrightness.GetSupportedLevels();

         for (Int32 times = 0; times < 5; times++) {
            for (Int32 dim = 0; dim < levels.Length; dim++) {
               displayBrightness.SetLevel(DisplayPowerFlags.ACDC, levels[dim]);
               Thread.Sleep(100);
            }
            for (Int32 brighten = levels.Length - 1; brighten >= 0; brighten--) {
               displayBrightness.SetLevel(DisplayPowerFlags.ACDC, levels[brighten]);
               Thread.Sleep(100);
            }
         }
      }
   }
   #endregion
}


///////////////////////////////////////////////////////////////////////////////


internal static class OpLockDemo {
   private static Byte s_endEarly = 0;
   private static DeviceIO m_device;

   public static void Demo() {
      String filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"FilterOpLock.dat");

      // Attempt to open/create the file for processing (must be asynchronous)
      using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate,
         FileAccess.ReadWrite, FileShare.ReadWrite, 8096, FileOptions.Asynchronous)) {

         m_device = new DeviceIO(fs.SafeFileHandle, true);

         Byte[] data = Encoding.ASCII.GetBytes("This is some text in the FilterOpLock file");
         fs.Write(data, 0, data.Length);

         // Request the Filter Oplock on the file.
         // When another process attempts to access the file, the system will 
         //    1. Block the other process until we close the file
         //    2. Call us back notifying us that another process wants to access the file
         BeginFilter(
            delegate(IAsyncResult result) {
               EndFilter(result);
               Console.WriteLine("Another process wants to access the file or the file closed");
               Thread.VolatileWrite(ref s_endEarly, 1);  // Tell Main thread to end early
            }, null);

         // Pretend we're accessing the file here
         for (Int32 count = 0; count < 1000; count++) {
            Console.WriteLine("Accessing the file ({0})...", count);

            // If the user hits a key or if another application wants to access the file,
            // close the file.
            if (Console.KeyAvailable || (Thread.VolatileRead(ref s_endEarly) == 1)) break;
            Thread.Sleep(150);
         }
      }  // Close the file here allows the other process to continue running
      Console.WriteLine("The file is closed.");
      Console.ReadLine();
      File.Delete(filename);
   }

   // Establish a Request filter opportunistic lock
   private static IAsyncResult BeginFilter(AsyncCallback asyncCallback, Object state) {
      // See FSCTL_REQUEST_FILTER_OPLOCK in Platform SDK's WinIoCtl.h file
      DeviceControlCode RequestFilterOpLock =
         new DeviceControlCode(DeviceType.FileSystem, 23, DeviceMethod.Buffered, DeviceAccess.Any);
      return m_device.BeginControl(RequestFilterOpLock, null, asyncCallback, state);
   }

   private static void EndFilter(IAsyncResult result) { m_device.EndControl(result); }
}


//////////////////////////////// End of File //////////////////////////////////
