using System;
using System.Collections;
using System.Collections.Generic;

namespace RemoteBSOD
{
    public class ConsoleScript
    {
        public static void PrintLine(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{text}");
        }

        public static void Print(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{text}");
        }

        public static void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"E: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{text}\n");
        }

        public static void PrintWarn(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"W: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{text}\n");
        }

        public static void PrintLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{text}");
        }

        public static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"{text}");
        }

        public static string? GetInput()
        {
            return Console.ReadLine();
        }

        public static void Beep()
        {
            Console.Beep();
        }

        public static void Beep(int freq, int time)
        {
            Console.Beep(freq, time);
        }

        public static void ClearText()
        {
            Console.Clear();
        }
    }    
}