using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

class Program
    {
    static void Main(string[] args)
    {
        SelectQuery query = new SelectQuery("Select * from Win32_QuickFixEngineering");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            
        foreach (ManagementObject o in searcher.Get())
        {
            if( o["Description"].ToString().Length > 0)
                Console.WriteLine(o["Description"]);
        }
    }
}
