namespace APPPJ.ViewModels
{
    using System;
    using System.Windows.Input;
    using APPPJ.Models;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    using Android.Bluetooth;
    using Java.IO;
    using System.Linq;
    using Android.OS;
    using System.Text;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Java.Util;
    using APPPJ.Helpers;

    public class CompraModificarViewModel : BaseViewModel
    {
        #region Variables
        private CompraModel _Compra;
        private bool _IsEnabled;
        private bool _IsRunning;
        private string _Comentario;
        #endregion

        #region Propiedades
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public bool IsRunning
        {
            get { return this._IsRunning; }
            set { SetValue(ref this._IsRunning, value); }
        }
        public CompraModel Compra
        {
            get { return this._Compra; }
            set { SetValue(ref this._Compra, value); }
        }
        public string Comentario
        {
            get { return this._Comentario; }
            set { SetValue(ref this._Comentario, value); }
        }
        #endregion

        #region Constructor
        public CompraModificarViewModel(CompraModel _Model)
        {
            Compra = _Model;
            Comentario = Compra.Comentario;
            IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand GuardarCommand
        {
            get { return new RelayCommand(Guardar); }
        }

        public ICommand ImprimirCommand
        {
            get { return new RelayCommand(Imprimir); }
        }
        private async void Imprimir()
        {
            try
            {
                IsEnabled = false;
                IsRunning = true;
                var mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

                if (mBluetoothAdapter == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                               "Bluetooth",
                               "No se encuentra adaptador bluetooth",
                               "Aceptar");
                    return;
                }

                if (!mBluetoothAdapter.IsEnabled)
                {
                    mBluetoothAdapter.Enable();
                }

                BluetoothDevice device = (from bd in mBluetoothAdapter.BondedDevices
                                          where bd.Name.StartsWith("AL")
                                          select bd).FirstOrDefault();
                if (device == null)
                    throw new Exception("Dispositivo no encontrado");

                ParcelUuid uuid = device.GetUuids().ElementAt(0);
                BluetoothSocket mmsSocket = device.CreateRfcommSocketToServiceRecord(uuid.Uuid);
                await mmsSocket.ConnectAsync();

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Xamarin bluetooth\nPrinting text test\nSample Text...");
                //await Task.Delay(3000);
                byte[] format = {29, 33, 35};
                // Write data to the device
                mmsSocket.OutputStream.Write(format, 0, format.Length);
                mmsSocket.OutputStream.Write(buffer, 0, buffer.Length);

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

                mmsSocket.OutputStream.Write(UnicodeEncoding.UTF8.GetBytes(cmds),0, UnicodeEncoding.UTF8.GetBytes(cmds).Length);
                mmsSocket.Close();

                
                App.Impresion.Print("Xamarin bluetooth\nPrinting text test\nSample Text...");
                IsEnabled = false;
                IsRunning = true;
            }
            catch (Exception ex)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           ex.Message,
                           "Aceptar");
                return;
            }
        }
        private async void Guardar()
        {
            try
            {
                if (string.IsNullOrEmpty(Comentario))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo comentario es obligatorio",
                            "Aceptar");
                    return;
                }

                Compra.Comentario = Comentario;

                if (App.Data.Guardar(Compra, new System.Collections.Generic.List<CompraDetalleModel>()))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           "Compra " + Compra.Codigo + " modificada exitósamente",
                           "Aceptar");
                }
                MainViewModel.GetInstance().CompraConsulta.CargarCompras();
                await App.Navigator.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                IsRunning = false;
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           ex.Message,
                           "Aceptar");
                return;
            }
        }
        #endregion
    }
}
