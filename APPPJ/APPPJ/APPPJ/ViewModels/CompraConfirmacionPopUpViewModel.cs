using System;
namespace APPPJ.ViewModels
{
    using APPPJ.Models;
    using APPPJ.Helpers;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Rg.Plugins.Popup.Extensions;
    using APPPJ.Views;
    using System.Collections.Generic;
    using Android.Bluetooth;
    using Java.IO;
    using System.Linq;
    using Android.OS;
    using System.Text;

    public class CompraConfirmacionPopUpViewModel : BaseViewModel
    {
        #region Variables
        private CompraModel _Compra;
        private double _Precio;
        private double _Cantidad;
        private double _Total;
        private double _Calificacion;
        private bool _IsRunning;
        private bool _IsEnabled;
        private string _IdUsuario;
        private string _Proveedor;
        private List<CompraDetalleModel> Lista;
        #endregion

        #region Propiedades
        public double Cantidad
        {
            get { return this._Cantidad; }
            set { SetValue(ref this._Cantidad, value); }
        }
        public double Calificacion
        {
            get { return this._Calificacion; }
            set { SetValue(ref this._Calificacion, value); }
        }
        public double Total
        {
            get { return this._Total; }
            set { SetValue(ref this._Total, value); }
        }
        public double Precio
        {
            get { return this._Precio; }
            set { SetValue(ref this._Precio, value); }
        }
        public bool IsRunning
        {
            get { return this._IsRunning; }
            set { SetValue(ref this._IsRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public string Proveedor
        {
            get { return this._Proveedor; }
            set { SetValue(ref this._Proveedor, value); }
        }
        public string IdUsuario
        {
            get { return this._IdUsuario; }
            set { SetValue(ref this._IdUsuario, value); }
        }
        #endregion

        #region Constructor
        public CompraConfirmacionPopUpViewModel(CompraModel _Model, List<CompraDetalleModel> _Lista)
        {
            _Compra = _Model;
            Cantidad = _Model.Cantidad;
            Precio = _Model.Precio;
            Total =  Cantidad * Precio;
            Calificacion = _Model.Calificacion;
            Proveedor = _Model.ProvNombre;
            IdUsuario = Settings.IdUsuario;
            IsEnabled = true;
            Lista = _Lista;
        }
        #endregion

        #region Comandos
        public ICommand GuardarCommand
        {
            get { return new RelayCommand(Guardar); }
        }

        private async void Guardar()
        {
            try
            {
                IsEnabled = false;
                IsRunning = true;

                _Compra.Fecha = DateTime.Now.Date;
                _Compra.Cantidad = Cantidad;
                _Compra.Precio = Precio;
                _Compra.Total = Total;
                _Compra.Calificacion = Calificacion;
                _Compra.Comentario = "";
                _Compra.IdUsuario = Settings.IdUsuario;
                _Compra.Estado = "P";

                if(App.Data.Guardar(_Compra,Lista))
                {
                    await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           "Compra "+ _Compra.Codigo+" registrada exitósamente",
                           "Aceptar");
                }
                await Application.Current.MainPage.Navigation.PopAllPopupAsync();
                MainViewModel.GetInstance().Compra = new CompraViewModel();
                Application.Current.MainPage = new MasterPage();
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

        private async void Imprimir()
        {
            try
            {
                var mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

                if (mBluetoothAdapter == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                               "Bluetooth",
                               "No bluetooth adapter found",
                               "Aceptar");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                               "Bluetooth",
                               "Bluetooth found",
                               "Aceptar");
                }

                if (!mBluetoothAdapter.IsEnabled)
                {
                    mBluetoothAdapter.Enable();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Bluetooth",
                            "Bluetooth Enabled",
                            "Aceptar");
                }

                ICollection<BluetoothDevice> pairedDevices = mBluetoothAdapter.BondedDevices;
                BluetoothDevice mmDevice = null;
                if (pairedDevices.Count > 0)
                {
                    foreach (BluetoothDevice device in pairedDevices)
                    {
                        if (device.Name.Contains("TSP"))
                        {
                            mmDevice = device;
                            break;
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Bluetooth",
                            "Paired Devices not found",
                            "Aceptar");
                    return;
                }

                ParcelUuid uuid = mmDevice.GetUuids().ElementAt(0);

                if (mmDevice == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Bluetooth",
                            "No Device Found",
                            "Aceptar");
                    return;
                }

                var mmsSocket = mmDevice.CreateInsecureRfcommSocketToServiceRecord(uuid.Uuid);

                mmsSocket.Connect();

                if (mmsSocket.IsConnected)
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Bluetooth",
                            "Socket Connected Successfully",
                            "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                            "Bluetooth",
                            "Socket Not Connected Successfully",
                            "Aceptar");
                    return;
                }

                var datastream = mmsSocket.OutputStream;

                byte[] byteArray = Encoding.ASCII.GetBytes("Sample Text");

                datastream.Write(byteArray, 0, byteArray.Length);
                /*
                BluetoothSocket socket = null;
                BufferedReader inReader = null;
                BufferedWriter outReader = null;
                string bt_printer = AdminSettings.PrinterMACAddr;
                if (string.IsNullOrEmpty(bt_printer)) bt_printer = "00:13:7B:49:D1:8C";
                BluetoothDevice mmDevice = BluetoothAdapter.DefaultAdapter.GetRemoteDevice(bt_printer);
                UUID applicationUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");
                socket = mmDevice.CreateRfcommSocketToServiceRecord(applicationUUID);
                socket.Connect();
                inReader = new BufferedReader(new InputStreamReader(socket.InputStream));
                outReader = new BufferedWriter(new OutputStreamWriter(socket.OutputStream));
                byte[] printformat = { 0x1B, 0X21, FONT_TYPE };
                //outReader.Write(printformat);
                outReader.NewLine();
                outReader.Write(text);
                */
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

        public ICommand CerrarCommand
        {
            get { return new RelayCommand(Cerrar); }
        }

        private async void Cerrar()
        {
            await Application.Current.MainPage.Navigation.PopAllPopupAsync();
        }
        #endregion
    }
}
