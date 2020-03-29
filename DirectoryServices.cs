using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;

namespace DirectoryServices
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DirectoryEntry e = new DirectoryEntry("LDAP://DC=mydomain,DC=local");
                DirectorySearcher searcher =
                    new DirectorySearcher(e);
                searcher.Filter = "(objectClass=computer)";
                SearchResultCollection results = searcher.FindAll();
                Console.WriteLine("{0} Computer gefunden.", results.Count.ToString());

                foreach (SearchResult r in results)
                {
                    Console.WriteLine(r.GetDirectoryEntry().Name.ToString());
                    Console.WriteLine(r.GetDirectoryEntry().GetType().Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
