using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

class Program
{
    static void Main()
    {
        SelectQuery query = new SelectQuery("Select * from Win32_Process where Name='svchost.exe'");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
        string strOwner, strDomain;
        foreach (ManagementObject o in searcher.Get())
        {
            string[] gOwner = new string[2];
            o.InvokeMethod("GetOwner", gOwner);
            strOwner = gOwner[0] != null ? gOwner[0] : "";
            strDomain = gOwner[1] != null ? gOwner[1] : "";
            Console.WriteLine("{0}\t\t {1},{2}", o["Name"], strOwner, strDomain);

            // Prozess notepad.exe beenden
            if (o["Name"].ToString().ToLower() == "notepad.exe")
                o.InvokeMethod("Terminate", null);
        }
    }
}

