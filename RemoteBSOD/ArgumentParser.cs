using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace RemoteBSOD
{
    public class ArgumentParser : ConsoleScript
    {
        public static void ParseArguments(string[] Args)
        {
            if(Args.Length == 0) { PrintHelp(); }
            else
            {
                if(Args[0] == "-s") { Program.ProgramExecutionMode = ProgramMode.ServerMode; Program.Execute(); }
                else if (Args[0] == "-c") 
                { 
                    if(Args.Length == 2)
                    {
                        if(string.IsNullOrEmpty(Args[1]))
                        {
                            PrintError("Invalid argument (IP ADDRESS)");
                        }
                        else
                        {
                            IPAddress address;
                            if (IPAddress.TryParse(Args[1], out address))
                            {
                                switch (address.AddressFamily)
                                {
                                    case System.Net.Sockets.AddressFamily.InterNetwork:
                                        PrintLine($"Set ip address to: {Args[1]}", ConsoleColor.Yellow);
                                        Program.ConnectionAddress = address;
                                        Program.ProgramExecutionMode = ProgramMode.ClientMode;
                                        Program.Execute();
                                        break;
                                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                                        PrintError($"IPv6 is not allowed");
                                        break;
                                    default:
                                        PrintError($"{Args[1]} is not a valid IPv4 Address!");
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        PrintError("No argument given (IP ADDRESS)");
                    }
                }
            }
        }

        static void PrintHelp()
        {
            PrintLine("- HELP -");
            PrintLine($"Run as server: -s (usage: RemoteBSOD.exe -s)\nRun as client: -c (usage: RemoteBSOD.exe -c [IP:ADDRESS])");
            PrintLine("- END OF HELP -");
        }
    }    
}