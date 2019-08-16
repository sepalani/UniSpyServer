﻿using System;
using System.Runtime.InteropServices;
using System.IO;
using GameSpyLib.Logging;
using RetroSpyServer.Servers;
using RetroSpyServer.XMLConfig;

namespace RetroSpyServer
{
    /// <summary>
    /// This class represents a RetroSpy Server program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Indicates the version of the server
        /// </summary>
        //public static readonly Version Version = Version.Parse(Application.ProductVersion);
        public static readonly string version = "0.5";

        public static string BasePath { get; protected set; }

        public static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        private static ServerManager manager = null;

        public static bool IsRunning = false;

        /// <summary>
        /// Entry point for the RetroSpy Server program
        /// </summary>
        /// <param name="args">List of arguments passed to the application</param>
        ///<param name="bool_InitPathArg">argument for Main()</param>
        static void Main(string[] args)
        {
            if (IsWindows()) { Console.WindowWidth = 100; } // Temp fix for Linux and MacOS?

            //you can choose whether accept command input.
            bool IsConsoleInputAvailable = false;
            // Whether accept  args input.
            bool IsInitPathArgAvailable = false; 

            string logPath;
            Console.Title = "RetroSpy Server " + version;
            BasePath = AppDomain.CurrentDomain.BaseDirectory;

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                IsRunning = false;
            };

            #region Argument Setting
            // Argument switcher
            foreach (var argument in args)
            {
                if (IsInitPathArgAvailable)

                {
                    BasePath = argument;
                    IsInitPathArgAvailable = false;
                }
                else if (argument == "--help")
                {
                    Console.WriteLine("List of arguments available:\n" +
                        "--help\tPrints this screen\n" +
                        "--no-cli-input\tDisables console input, usefull for services\n" +
                        "--init-path [path]\tUse a custom base path for load the configuration and save the logs"
                    );

                    // Close the program after --help
                    return;
                }
                else if (argument == "--no-cli-input")
                    IsConsoleInputAvailable = false;
                else if (argument == "--init-path")
                    IsInitPathArgAvailable = true;
                else
                {
                    Console.WriteLine("Unknown argument {0}", argument);
                }
            }

            if (IsInitPathArgAvailable)
            {
                Console.WriteLine("The argument \"--init-path\" requires an argument!");
                return;
            }
            #endregion

            #region Path Setting
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);

            logPath = BasePath + @"/Logs/";

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            #endregion


            LogWriter.Log = new LogWriter(string.Format(Path.Combine(logPath, "retrospy_{0}.log"), DateTime.Now.ToString("yyyy-MM-dd__HH_mm_ss")));

            Console.WriteLine("\t"+  @"  ___     _           ___             ___                      ");
            Console.WriteLine("\t" + @" | _ \___| |_ _ _ ___/ __|_ __ _  _  / __| ___ _ ___ _____ _ _ ");
            Console.WriteLine("\t" + @" |   / -_)  _| '_/ _ \__ \ '_ \ || | \__ \/ -_) '_\ V / -_) '_|");
            Console.WriteLine("\t" + @" |_|_\___|\__|_| \___/___/ .__/\_, | |___/\___|_|  \_/\___|_|  ");
            Console.WriteLine("\t" + @"                         |_|   |__/                            ");
            Console.WriteLine("");

            LogWriter.Log.Write("RetroSpy Server version " + version + ".", LogLevel.Info);

            try
            {
                ConfigManager.Load();
                //set the loglevel to system
                LogWriter.Log.MiniumLogLevel = ConfigManager.xmlConfiguration.LogLevel;
                //create a instance of ServerManager class
                manager = new ServerManager();
                IsRunning = true;
            }
            catch (Exception e)
            {
                LogWriter.Log.WriteException(e);
            }

            if (IsConsoleInputAvailable)
            {
                LogWriter.Log.Write("Servers are successfully started! \nType \"help\" for a list of the available commands.", LogLevel.Info);
                //Read key from console
                while (IsRunning)
                {
                    // Process console commands
                    //if (bool_ConsoleInput)
                    //{
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "exit":
                            manager.StopAllServers();
                            IsRunning = false;
                            break;
                        case "help":
                            Console.WriteLine("--exit \t shutdown all servers and exit the RetroSpy emulator\n" +
                                                           "other features are comming soon..\n");
                            break;
                        default:
                            Console.WriteLine("Unknown command!");
                            break;
                    }
                }
            }
            else
            {
                // Pauses the screen
                LogWriter.Log.Write("Servers are successfully started! ", LogLevel.Info);
                Console.WriteLine("Press ENTER to exit...");
                Console.ReadLine();
            }
            #region Program Dispose
            LogWriter.Log.Write("Goodbye!", LogLevel.Info);
            manager?.Dispose();
            LogWriter.Log.Dispose();
            #endregion

     

        }        
    }
}
