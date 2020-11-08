using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static System.Environment;

namespace moonshot
{
    class Program
    {
        // -----------------------------------------------------------------------------------
        // Program is a intended to be a clone of the Oregon Trail but apollo mission themed.
        // Made as a submission for Game Off 2020 - MOONSHOT
        // Uses Raylib Engine, Raylib-CS, and dotnet core. 
        // -----------------------------------------------------------------------------------

        static void Main(string[] args)
        {
            // Convert args array into list, replace "/" with "-"
            List<string> argsList = new List<string>();
            args.ToList().ForEach(arg => argsList.Add(arg.Replace("/", "-")));

            // If run with -help, -?, or -h display version and other possible flags.
            if (argsList.Contains("-help", StringComparer.OrdinalIgnoreCase) || argsList.Contains("-h", StringComparer.OrdinalIgnoreCase) || argsList.Contains("-?", StringComparer.OrdinalIgnoreCase))
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                string helpMessage = @"------------------------------------------------
| MOONSHOT - Version: " + version + (Program.Repeater(' ', (25 - version.Length))) + @"|
|- - - - - - - - - - - - - - - - - - - - - - - |
| Run with '-Debug' to get raylib output.      |
------------------------------------------------";
                Console.WriteLine(helpMessage);
                return;
            }

            // Check for game resources
            if (!checkforResources()) { return; }

            // Ensure only one instance is running
            try {
                Process current = Process.GetCurrentProcess();
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        Console.WriteLine("Detected another instance of MOONSHOT running. Only one may run at a time, exiting this one.");
                        return; // Exit;
                    }
                }

                // Create Main Window
                MainWindow.Init(argsList.Contains("-debug", StringComparer.InvariantCultureIgnoreCase));
            } 
            catch (Exception err) 
            {
                // Exception handling
                logError(err);
            }
        }

        // Log any errors
        private static void logError(Exception err)
        {
            // Create Error Message
            string message = " - Start Error -" + Environment.NewLine +
                DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt") + " - Error " + err.HResult + " - " + err.Message + Environment.NewLine +
                err.InnerException + Environment.NewLine +
                err.TargetSite + Environment.NewLine +
                err.Source + Environment.NewLine +
                err.StackTrace + Environment.NewLine +
                err.HelpLink + Environment.NewLine +
                " - End Error -" + Environment.NewLine + Environment.NewLine;
            // Check if folder exists
            string AppData = AppDataFolder();
            if (!Directory.Exists(AppData))
            {
                Directory.CreateDirectory(AppData);
            }
            // Write to AppData
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Path.Combine(AppData, @"moonshot-error.log"), true))
            {
                file.WriteLine(message);
            }
            // Throw the Error anyways, useful from the user's perspective to see the actual error.
            throw err;
        }

        static bool checkforResources()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images" )))
            {
                Console.WriteLine("Error loading 'Images' folder.");
                return true; // Exit
            }
            return true;
        }

        // Function to return the AppDataFolder location. This ensures that we store files to the correct location based on the operation system.
        // https://dotnettips.wordpress.com/2017/02/20/net-framework-core-getting-the-app-data-folder/
        public static string AppDataFolder()
        {
            var userPath = Environment.GetEnvironmentVariable(
              RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
              "LOCALAPPDATA" : "Home");

            // If the userpath is null then we use an alternate method.
            // https://developers.redhat.com/blog/2018/11/07/dotnet-special-folder-api-linux/
            if (userPath == null)
            {
                userPath = Environment.GetFolderPath(SpecialFolder.UserProfile, SpecialFolderOption.DoNotVerify);
            }

            // Set foldername based on platform.
            var folder = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
              "moonshot" : ".moonshot";
            var path = System.IO.Path.Combine(userPath, folder);

            return path;
        }

        // Private Repeater function, just repeats a chracter as many times as you specify.
        private static string Repeater(char c, int n)
        {
            if (n < 0) { n = 0; }
            return new String(c, n);
        }
    }
}
