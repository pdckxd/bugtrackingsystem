using System;
using Wintellect.Threading.LogicalProcessor;

public static class Program {
   static void Main(string[] args) {
      if (Environment.OSVersion.Version < new Version(6, 0, 0, 0)) {
         Console.WriteLine("This sample requires Windows Vista or later.");
         return;
      }

      Console.WriteLine("Logical Processor Information");
      LogicalProcessorInformation[] lpis = LogicalProcessorInformation.GetLogicalProcessorInformation();
      Array.ForEach(lpis, Console.WriteLine);
   }
}
