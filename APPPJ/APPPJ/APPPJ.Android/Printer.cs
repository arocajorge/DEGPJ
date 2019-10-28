using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using APPPJ.Droid;
using APPPJ.Helpers;
using Java.IO;
using Java.Util;

[assembly: Xamarin.Forms.Dependency(typeof(Printer))]
namespace APPPJ.Droid
{
    
    public class Printer : IPrinter
    {
        public Printer()
        {
        }


        public void Print(string printString)
        {


            try
            {
                var mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

                if (mBluetoothAdapter == null)
                {
                    throw new Exception("No se encuentra adaptador bluetooth");
                }

                if (!mBluetoothAdapter.IsEnabled)
                {
                    mBluetoothAdapter.Enable();
                }

                BluetoothSocket socket = null;
                BufferedReader inReader = null;
                BufferedWriter outReader = null;

                BluetoothDevice device = (from bd in mBluetoothAdapter.BondedDevices
                                          where bd.Name.StartsWith("PT200")
                                          select bd).FirstOrDefault();
                if (device == null)
                    throw new Exception("Dispositivo no encontrado");

                UUID applicationUUID = UUID.FromString("00001101-0000-1000-8000-00805f9b34fb");

                socket = device.CreateRfcommSocketToServiceRecord(applicationUUID);
                socket.Connect();
                inReader = new BufferedReader(new InputStreamReader(socket.InputStream));
                outReader = new BufferedWriter(new OutputStreamWriter(socket.OutputStream));


                //byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Xamarin bluetooth\nPrinting text test\nSample Text...");                

                //Create ESC/POS commands for sample receipt
                string ESC = "0x1B"; //ESC byte in hex notation
                string NewLine = "0x0A"; //LF byte in hex notation

                string cmds = ESC + "@"; //Initializes the printer (ESC @)
                cmds += ESC + "!" + "0x38"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "BEST DEAL STORES"; //text to print
                cmds += NewLine + NewLine;
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0)
                cmds += "COOKIES                   5.00";
                cmds += NewLine;
                cmds += "MILK 65 Fl oz             3.78";
                cmds += NewLine + NewLine;
                cmds += "SUBTOTAL                  8.78";
                cmds += NewLine;
                cmds += "TAX 5%                    0.44";
                cmds += NewLine;
                cmds += "TOTAL                     9.22";
                cmds += NewLine;
                cmds += "CASH TEND                10.00";
                cmds += NewLine;
                cmds += "CASH DUE                  0.78";
                cmds += NewLine + NewLine;
                cmds += ESC + "!" + "0x18"; //Emphasized + Double-height mode selected (ESC ! (16 + 8)) 24 dec => 18 hex
                cmds += "# ITEMS SOLD 2";
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0)
                cmds += NewLine + NewLine;
                cmds += "11/03/13  19:53:17";

                char[] buffer = cmds.ToCharArray();
                outReader.Write(cmds);
                outReader.Write(buffer);


                outReader.Flush();
                var s = inReader.Ready();
                inReader.Skip(0);
                //close all
                inReader.Close();
                socket.Close();
                outReader.Close();

            }
            catch(Exception ex)
            {

            }
        }

    }
}