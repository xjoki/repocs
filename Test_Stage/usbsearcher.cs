// USB SEARCHER
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFGetDirectory
{
    class UsbSearcher
    {
        static Collection<UsbDataModel> sc = new Collection<UsbDataModel>();

        
        private static void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            isWork = false;
            header.Background = b;
            foreach(UsbDataModel s in sc)
            {
                listView.Items.Add(s);
            }
        }

        public static void GetUsbDevices(object sender, DoWorkEventArgs e)
        {
            const string getCurrentControlSet = "System\\Select";
            const string strCCSet = "001";
            string getUSBKeys = "System\\ControlSet" + strCCSet + "\\Enum\\USBSTOR";
            int iLfd = 1;   // laufende Nummer zur Ausgabe
            var output = String.Empty;

            // Auslesen des aktuellen ControlSet
            RegistryKey key = Registry.LocalMachine.OpenSubKey(getCurrentControlSet);
            Object o = null;
            // wenn das aktuelle ControlSet ausgelesen werden konnte, dann verwenden
            // ansonsten wird für CurrentControlSet die Vorgabe 001 benutzt

            if (key != null)
            {
                o = key.GetValue("Current"); // Wert auslesen
                String cSet = o.ToString();
                // und das aktuelle ControlSet setzen
                getUSBKeys = getUSBKeys.Replace(strCCSet.Substring(3 - cSet.Length, cSet.Length), cSet);

                // Ausgabe Header
                //var output = String.Format("{0,-4}{1,-28}{2,-37}{3}", "Nr.", "Seriennummer", "Name", "Install");
                //listBox.Items.Add(output);
                //output = String.Format("{0,-4}{1,-28}{2,-37}{3}", "---", "------------", "----", "-------");
                //listBox.Items.Add(output);

                key = Registry.LocalMachine.OpenSubKey(getUSBKeys);
                var theKeys = key.GetSubKeyNames();
                foreach (string s in theKeys)
                {
                    var iKey = Registry.LocalMachine.OpenSubKey(getUSBKeys + "\\" + s);
                    var theKeys2 = iKey.GetSubKeyNames();
                    foreach (string serial in theKeys2)
                    {
                        var iKey2 = Registry.LocalMachine.OpenSubKey(getUSBKeys + "\\" + s + "\\" + serial);
                        var theKeys3 = iKey2.GetValueNames();
                        var srcString = s + "\\" + serial;
                        // Ausgabe von FriendlyName und Seriennummer
                        var friendlyNameKey = Registry.LocalMachine.GetValue(getUSBKeys + "\\" + s + "\\" + serial, "FriendlyName");

                        // Im StreamReader nach der Zeichfolge srcString suchen...

                        string fUsage = searchUsageTime(serial);
                        if (fUsage != "N/A")
                            fUsage = fUsage.Substring(0, 10);

                        var endSerial = serial.IndexOf("&");


                        if (endSerial > 0)
                        {
                            UsbDataModel model = new UsbDataModel();
                            model.Serial = serial.Substring(0, endSerial);
                            model.Description = iKey2.GetValue("FriendlyName").ToString();
                            model.Activate = fUsage;
                            sc.Add(model);
                            //output = String.Format("{0,-10}{1,-40}{2,-50}{3}", iLfd, serial.Substring(0, endSerial), iKey2.GetValue("FriendlyName"), fUsage);
                        }
                        else
                        {
                            UsbDataModel model = new UsbDataModel();
                            model.Serial = serial.Substring(0, endSerial);
                            model.Description = iKey2.GetValue("FriendlyName").ToString();
                            model.Activate = fUsage;
                            sc.Add(model);
                           // output = String.Format("{0,-10}{1,-40}{2,-50}{3}", iLfd, serial, iKey2.GetValue("FriendlyName"), fUsage);
                        }
                        
                        //sc.Add(output);
                        //listBox.Items.Add(output);
                        iLfd++;
                    }
                }
            }
        }

        

    }
}
