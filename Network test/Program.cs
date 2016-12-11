namespace Network_test
{
    using System;
    using System.Management;
    using System.Diagnostics;
    using System.Net.NetworkInformation;

    public class Program
    {
        public static string FilePath = @"F:\MATLab\bin\win64\MATLAB.exe";
        // Network helper to manage switching on and off of network addapters
        public static NetworkHelper NetworkManager = new NetworkHelper();
        public static void Main(string[] args)
        {
            // create a process start configuration for the program
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = FilePath;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // dissable all network connections befor startin program
            NetworkManager.DisableAllNetworkConnections();

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                // Reanabling all connections that had acces to internet after program exit
                NetworkManager.RestoreNetworkConnectionsToOriginalSettings();
                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }
    }
}
