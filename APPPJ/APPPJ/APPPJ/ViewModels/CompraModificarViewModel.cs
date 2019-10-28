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
    using Plugin.DeviceInfo;

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
                                          where bd.Name.StartsWith("PT200")
                                          select bd).FirstOrDefault();
                if (device == null)
                    throw new Exception("Dispositivo no encontrado");

                ParcelUuid uuid = device.GetUuids().ElementAt(0);
                BluetoothSocket mmsSocket = device.CreateRfcommSocketToServiceRecord(uuid.Uuid);
                await mmsSocket.ConnectAsync();

                string Reporte = string.Empty;
                Reporte += "\n";
                Reporte += "====NEXPIRION===="+"\n";
                Reporte += "Compra # "+Compra.Codigo+"\n";
                Reporte += "Recolector: " + Compra.IdUsuario + "\n";
                Reporte += "Equipo: " + CrossDeviceInfo.Current.Id + "\n";
                Reporte += "Proveedor: " + Compra.ProvNombre+ "\n";
                Reporte += "Producto: " + Compra.prDescripcion + "\n";
                Reporte += "Cantidad: " + Compra.Cantidad.ToString("n2") + "\n";
                Reporte += "Calificacion: " + Compra.Calificacion.ToString("n2")+ "\n";
                Reporte += "Precio: " + Compra.Precio.ToString("n2") + "\n";
                Reporte += "Total: " + Compra.Total.ToString("n2") + "\n";
                Reporte += "\n";
                Reporte += "\n";
                Reporte += "\n";

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Reporte);
                mmsSocket.OutputStream.Write(buffer, 0, buffer.Length);
                mmsSocket.Close();

                IsEnabled = true;
                IsRunning = false;
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
