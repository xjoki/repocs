using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer www = new WebServer(80, "c:\\www", "index.htm");
            Console.WriteLine("WebServer wird gestartet...");
            string ipAddress = "127.0.0.1";
            IPAddress ip = IPAddress.Parse(ipAddress);
            IPEndPoint ep = new IPEndPoint(ip, www.Port);
            TcpListener listen = null;
            // Datenpuffer
            Byte[] bytes = null;
            int i;

            try 
	        {	        
                listen = new TcpListener(ep);
                listen.Start();
	        }
	        catch (Exception)
	        {
		        throw;
	        }

            do
            {
                Console.WriteLine("Warte auf eine Verbindung...");
                TcpClient client = listen.AcceptTcpClient();
                Console.WriteLine("Verbindung hergestellt!");
                string Data = null;
                NetworkStream stream = client.GetStream();
                bytes = new Byte[client.ReceiveBufferSize];

                Console.WriteLine("Client-Anfrage...");
                // Lesen der Daten

                i = stream.Read(bytes, 0, bytes.Length);
                if(i > 0)
                {
                    Data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(Data);
                }
                // Tokens
                string[] words = Data.Split(' ');
                if (words[0] == "GET")
                {
                    if (words[1].EndsWith("/"))
                    {
                        words[1] = words[1] + www.DefaultFile;
                    }

                    // Dokument an Client senden
                    FileStream file = null;
                    try
                    {
                        file = new FileStream(www.DocumentRoot + words[1], FileMode.Open);
                        byte[] readBuffer = new byte[4096];
                        int r = 0;
                        int offset = 0;
                        while ((r = file.Read(readBuffer, offset, readBuffer.Length)) > 0)
                        {
                            stream.Write(readBuffer, 0, r);
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        string errMsg =
                            "<html><head><title>Fehler</title></head>" +
                            "<body><h2>404 Nicht gefunden</h2></body></html>";

                        ASCIIEncoding enc = new ASCIIEncoding();
                        stream.Write(enc.GetBytes(errMsg),0,errMsg.Length);

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        if( file != null)
                            file.Close();
                    }
                }
                else
                {
                    Console.WriteLine("FEHLER 400 : BAD REQUEST");
                }
                client.Close();
                i = 0;
            } while (true);
        }
    }
}
