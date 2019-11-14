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
    using Plugin.DeviceInfo;

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

                //_Compra.Fecha = DateTime.Now.Date;
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

                Imprimir();

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

                var Detalle = App.Data.GetCompraDetalle(_Compra.IdCompra);

                string Reporte = string.Empty;
                Reporte += "\n";
                Reporte += "====NEXPIRION====" + "\n";
                Reporte += "Compra # " + _Compra.Codigo + "\n";
                Reporte += "Fecha # " + _Compra.Fecha.Date.ToString("dd/MM/yyyy") + "\n";
                Reporte += "Recolector: " + Settings.IdUsuario + "\n";
                Reporte += "Equipo: " + CrossDeviceInfo.Current.Id + "\n";
                Reporte += "Proveedor: " + _Compra.ProvNombre + "\n";
                Reporte += "Producto: " + _Compra.prDescripcion + "\n";
                foreach (var item in Detalle)
                {
                    Reporte += item.Descripcion + ": " + item.Valor.ToString("n2") + "\n";
                }
                Reporte += "Calificacion: " + _Compra.Calificacion.ToString("n2") + "\n";
                Reporte += "\n";
                Reporte += "Cantidad: " + _Compra.Cantidad.ToString("n2") + "\n";
                Reporte += "Precio: " + _Compra.Precio.ToString("n2") + "\n";
                Reporte += "Total: " + _Compra.Total.ToString("n2") + "\n";
                Reporte += "\n";
                Reporte += "\n";


                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Reporte);
                mmsSocket.OutputStream.Write(buffer, 0, buffer.Length);
                //mmsSocket.OutputStream.Write(buffer, 0, buffer.Length);
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
