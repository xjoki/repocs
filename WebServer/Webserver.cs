using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer
{
    class WebServer
    {
        // TCP-Port
        private int port;
        // Document-Root
        private string docRoot;
        // STD-File
        private string defaultFile;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public string DocumentRoot
        {
            get { return docRoot; }
            set { docRoot = value; }
        }

        public string DefaultFile
        {
            get { return defaultFile; }
            set { defaultFile = value; }
        }

        public WebServer(int port, string docRoot, string defaultFile)
        {
            Port = port;
            DocumentRoot = docRoot;
            DefaultFile = defaultFile;
        }
    }
}
