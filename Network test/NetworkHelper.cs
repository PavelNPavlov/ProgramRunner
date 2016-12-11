using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;

namespace Network_test
{
    public class NetworkHelper
    {
        public Dictionary<string, bool> OriginalSettings { get; set; }

        public NetworkHelper()
        {
            this.OriginalSettings = new Dictionary<string, bool>();
        }

        public void DisableAllNetworkConnections()
        {
            // query for object selecting
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            // get objects from windows system
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            // go through every adapter and recored initial state. Disable all adapters
            foreach (ManagementObject item in searchProcedure.Get())
            {
                var id = (string)item["NetConnectionId"];
                var enalbed = (bool)item["NetEnabled"];
                this.OriginalSettings.Add(id, enalbed);

                item.InvokeMethod("Disable", null);
            }
        }

        public void RestoreNetworkConnectionsToOriginalSettings()
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject item in searchProcedure.Get())
            {
                var id = (string)item["NetConnectionId"];
                // reenable adpaters that were one before program start
                if (this.OriginalSettings[id])
                {
                    item.InvokeMethod("Enable", null);
                }
            }
        }
    }
}
