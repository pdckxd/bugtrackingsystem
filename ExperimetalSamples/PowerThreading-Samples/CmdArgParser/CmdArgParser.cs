/******************************************************************************
Module:  CmdArgParser.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.IO;
using Wintellect;
using Wintellect.CommandArgumentParser;


///////////////////////////////////////////////////////////////////////////////


internal enum WriteMode {
   [CmdArgEnumValueDescription("Create the file (if the file already exists, it is erased). This is the default.")]
   Create,
   [CmdArgEnumValueDescription("Append to an already existing file (if the file doesn't exist it is created).")]
   Append
}


///////////////////////////////////////////////////////////////////////////////


// The fields of this class map to command-line argument switches.
internal sealed class CmdArgOptions: ICmdArgs {
   // Members identifying command-line argument settings
   [CmdArg(ArgName = "P", RequiredArg = true, RequiredValue = CmdArgRequiredValue.Yes,
       Description = "The pathname of the file to be created or appended to")]
   public String pathname = null;

   [CmdArg(ArgName = "T", RequiredArg = true, RequiredValue = CmdArgRequiredValue.Yes,
       Description = "The text to write to the file")]
   public String text = null;

   [CmdArg(ArgName = "M", RequiredArg = false, RequiredValue = CmdArgRequiredValue.Yes,
       Description = "Specifies the mode to use to write to the file.")]
   public WriteMode mode = WriteMode.Create; // Default to creating a new file

   [CmdArg(ArgName = "N", RequiredArg = false, RequiredValue = CmdArgRequiredValue.Yes,
       Description = "The number of times to write the string to the file (1 if not specified)")]
   public Int32 numTimes = 1;     // Default to writing the text to the files just once

   [CmdArg(ArgName = "?", Description = "Show this usage help")]
   public Boolean ShowUsage = false;   // Default to not show usage

   // Called to display the app's usage information and optionally an error message
   public void Usage(String errorInfo) {
      if (errorInfo != null) {
         // A command-line argument error occurred, report it to user
         // errorInfo identifies the argument that is in error.
         Console.WriteLine("Command-line switch error: {0}{1}", errorInfo,
            Environment.NewLine);
      }
      Console.WriteLine(CmdArgParser.Usage(typeof(CmdArgOptions)));

#if false
      String _Usage = 
         @"Usage: WriteTextToFile /P:Pathname /T:String [/M:Create|Append] [/N:NumTimes]
   -P         The pathname of the file to be created or appended to
   -T         The text to write to the file
   -M:Create  Create the file (if the file already exists, it is erased). This is the default.
   -M:Append  Append to an already existing file (if the file doesn't exist it is created)
   -N         The number of times to write the string to the file (1 if not specified)
   -?         Show this usage help";

      Console.WriteLine(_Usage);
#endif
   }


   // Called when all command-line arguments have been parsed
   // The application can do some additional checking here and 
   // thrown an exception if something is wrong
   void ICmdArgs.Validate() {
      if (ShowUsage) return;
   }


   // Called for each non-switch command-line argument
   // The application can add these items to a collection 
   // or perform some other task/checking. Throw an exception if something isn't right.
   void ICmdArgs.ProcessStandAloneArgument(String arg) {
      throw new Exception<InvalidCmdArgumentExceptionArgs>(
                new InvalidCmdArgumentExceptionArgs(arg),
            String.Format("Unrecognized command line argument: '{0}'.", arg));
   }


   // Called to construct an instance of this class populating the fields from command-line arguments
   public static CmdArgOptions Parse(String[] args) {
      CmdArgOptions options = null;
      try {
         options = new CmdArgOptions();
         CmdArgParser.Parse(options, args);
      }
      catch (Exception<InvalidCmdArgumentExceptionArgs> e) {
         if (!options.ShowUsage) {
            options.Usage(e.Message);
            throw;
         }
      }

      if (options.ShowUsage) options.Usage(null);
      return options;
   }
}


///////////////////////////////////////////////////////////////////////////////


internal static class Program {
   public static void Main(String[] args) {
      // Construct an Option object, populating the fields from the command-line arguments
      CmdArgOptions options = CmdArgOptions.Parse(args);

      if (options.ShowUsage) {
         // If the user wants to display usage, return and don't run the rest of the app
         return;
      }

      // Create/Append to the specified file
      using (StreamWriter sw = new StreamWriter(options.pathname, options.mode == WriteMode.Append)) {

         // Write the specified text the number of times specified
         for (Int32 time = 0; time < options.numTimes; time++) {
            sw.WriteLine(options.text);
         }
      }
   }
}


//////////////////////////////// End of File //////////////////////////////////
