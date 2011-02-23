/******************************************************************************
Module:  DisplayBrightness.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.IO;
using Wintellect.IO;


///////////////////////////////////////////////////////////////////////////////


[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32"), Flags]
internal enum DisplayPowerFlags : byte {
   None = 0x00000000,
   AC = 0x00000001,
   DC = 0x00000002,
   ACDC = AC | DC
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class DisplayBrightness : DeviceIO {
   private static readonly DeviceControlCode s_QuerySupportedBrightness =
      new DeviceControlCode(DeviceType.Video, 0x125, DeviceMethod.Buffered, DeviceAccess.Any);

   private static readonly DeviceControlCode s_QueryBrightness =
      new DeviceControlCode(DeviceType.Video, 0x126, DeviceMethod.Buffered, DeviceAccess.Any);

   private static readonly DeviceControlCode s_SetBrightness =
      new DeviceControlCode(DeviceType.Video, 0x127, DeviceMethod.Buffered, DeviceAccess.Any);

   private struct Win32DisplayBrightness {
      public Win32DisplayBrightness(DisplayPowerFlags power, Byte level) {
         m_Power = power;
         m_ACBrightness = m_DCBrightness = level;
      }
      private DisplayPowerFlags m_Power;
      public DisplayPowerFlags Power { get { return m_Power; } }

      private Byte m_ACBrightness; // 0-100
      public Byte ACBrightness { get { return m_ACBrightness; } }

      private Byte m_DCBrightness; // 0-100
      public Byte DCBrightness { get { return m_DCBrightness; } }
   }

   public DisplayBrightness()
      : base(@"\\.\LCD", FileAccess.ReadWrite, FileShare.ReadWrite, false) {
   }

   public Byte[] GetSupportedLevels() {
      return GetArray<Byte>(s_QuerySupportedBrightness, null, 256);
   }

   public void SetLevel(DisplayPowerFlags power, Byte level) {
      Win32DisplayBrightness db = new Win32DisplayBrightness(power, level);
      Control(s_SetBrightness, db);
   }

   public DisplayPowerFlags GetLevel(out Byte acBrightness, out Byte dcBrightness) {
      Win32DisplayBrightness db;
      db = GetObject<Win32DisplayBrightness>(s_QueryBrightness);
      acBrightness = db.ACBrightness;
      dcBrightness = db.DCBrightness;
      return db.Power;
   }
}


//////////////////////////////// End of File //////////////////////////////////
