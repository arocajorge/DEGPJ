namespace APPPJ.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using APPPJ.Helpers;
    using APPPJ.Models;
    using APPPJ.Services;
    using APPPJ.Views;
    using GalaSoft.MvvmLight.Command;
    using Plugin.DeviceInfo;
    using Xamarin.Forms;

    public class SincronizacionViewModel : BaseViewModel
    {
        #region Variables
        private double _Cantidad;
        private double _Total;
        private ApiService api;
        private bool _IsRunning;
        private bool _IsEnabled;
        private List<CompraSincronizacionModel> _ListaCompras;
        #endregion

        #region Propiedades
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
        public double Total
        {
            get { return this._Total; }
            set { SetValue(ref this._Total, value); }
        }
        public double Cantidad
        {
            get { return this._Cantidad; }
            set { SetValue(ref this._Cantidad, value); }
        }
        public List<CompraSincronizacionModel> ListaCompras
        {
            get { return this._ListaCompras; }
            set { SetValue(ref this._ListaCompras, value); }
        }
        #endregion

        #region Constructor
        public SincronizacionViewModel()
        {
            api = new ApiService();
            IsEnabled = true;
            CargarCompras();
        }
        #endregion

        #region Metodos
        private void CargarCompras()
        {
            ListaCompras = App.Data.GetListCompras();
            Cantidad = ListaCompras.Count;
            Total = ListaCompras.Sum(q => q.Total);
        }
        #endregion

        #region Comandos
        public ICommand SincronizarCommand
        {
            get
            {
                return new RelayCommand(Sincronizar);
            }
        }

        private async void Sincronizar()
        {
            try
            {
                this.IsEnabled = false;
                this.IsRunning = true;
                int PKI = 0;

                #region Validaciones
                Response con = await api.CheckConnection(Settings.UrlConexionInterna);
                if (!con.IsSuccess)
                {
                    con = await api.CheckConnection(Settings.UrlConexionExterna);
                    if (!con.IsSuccess)
                    {
                        this.IsEnabled = true;
                        this.IsRunning = false;
                        await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            con.Message,
                            "Aceptar");
                        return;
                    }
                    else
                        Settings.UrlConexionActual = Settings.UrlConexionExterna;
                }
                else
                    Settings.UrlConexionActual = Settings.UrlConexionInterna;
                #endregion

                var response_sinc = await api.Post<List<CompraSincronizacionModel>>(                 Settings.UrlConexionActual,                 Settings.RutaCarpeta,                 "Compra",                 this.ListaCompras);
                if (!response_sinc.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_sinc.Message,
                        "Aceptar");
                    return;
                } 

                #region Usuario
                var response_usuario = await api.GetList<UsuarioModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "Usuario", "?IdUsuario=" + Settings.IdUsuario);
                if (!response_usuario.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_usuario.Message,
                        "Aceptar");
                    return;
                }

                var list_usuario = (List<UsuarioModel>)response_usuario.Result;

                App.Data.DeleteAll<UsuarioModel>();
                PKI = 1;
                list_usuario.ForEach(q => q.PKSQLite = PKI++);
                App.Data.InsertAll<UsuarioModel>(list_usuario);
                #endregion

                #region Producto
                var response_producto = await api.GetList<ProductoModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "Producto", "?IdUsuario=" + Settings.IdUsuario);
                if (!response_producto.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_producto.Message,
                        "Aceptar");
                    return;
                }

                var list_producto = (List<ProductoModel>)response_producto.Result;

                App.Data.DeleteAll<ProductoModel>();
                PKI = 1;
                list_producto.ForEach(q => q.PKSQLite = PKI++);
                App.Data.InsertAll<ProductoModel>(list_producto);
                #endregion

                #region Producto
                var response_atributos = await api.GetList<ProductoDetalleModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "ProductoDetalle", "?IdUsuario=" + Settings.IdUsuario);
                if (!response_atributos.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_atributos.Message,
                        "Aceptar");
                    return;
                }

                var list_atributos = (List<ProductoDetalleModel>)response_atributos.Result;

                App.Data.DeleteAll<ProductoDetalleModel>();
                PKI = 1;
                list_atributos.ForEach(q => q.PKSQLite = PKI++);
                App.Data.InsertAll<ProductoDetalleModel>(list_atributos);
                #endregion

                #region Proveedor
                var response_proveedor = await api.GetList<ProveedorModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "Proveedor", "?IdUsuario=" + Settings.IdUsuario);
                if (!response_proveedor.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_proveedor.Message,
                        "Aceptar");
                    return;
                }

                var list_proveedor = (List<ProveedorModel>)response_proveedor.Result;

                App.Data.DeleteAll<ProveedorModel>();
                PKI = 1;
                list_proveedor.ForEach(q => q.PKSQLite = PKI++);
                App.Data.InsertAll<ProveedorModel>(list_proveedor);
                #endregion

                #region ProductoPorProveedor
                var response_proveedorProducto = await api.GetList<ProveedorProductoModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "ProveedorProducto", "?IdUsuario=" + Settings.IdUsuario);
                if (!response_proveedorProducto.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_proveedorProducto.Message,
                        "Aceptar");
                    return;
                }

                var list_proveedorProducto = (List<ProveedorProductoModel>)response_proveedorProducto.Result;

                App.Data.DeleteAll<ProveedorProductoModel>();
                PKI = 1;
                list_proveedorProducto.ForEach(q => q.PKSQLite = PKI++);
                App.Data.InsertAll<ProveedorProductoModel>(list_proveedorProducto);
                #endregion

                #region Ultima compra
                var response_compra = await api.GetObject<CompraModel>(Settings.UrlConexionActual, Settings.RutaCarpeta, "Compra", "IdUsuario=" + Settings.IdUsuario+"&Dispositivo="+ CrossDeviceInfo.Current.Id);
                if (!response_compra.IsSuccess)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        response_compra.Message,
                        "Aceptar");
                    return;
                }
                App.Data.DeleteAll<CompraModel>();
                App.Data.DeleteAll<CompraDetalleModel>();

                var Compra = (CompraModel)response_compra.Result;
                Settings.IdCompra = Compra == null ? "0" : Compra.IdCompra.ToString("n0");

                await Application.Current.MainPage.DisplayAlert(
                        "Alerta",
                        "Sincronización exitosa",
                        "Aceptar");
                IsRunning = false;
                IsEnabled = true;
                MainViewModel.GetInstance().Compra = new CompraViewModel();
                Application.Current.MainPage = new MasterPage();
                #endregion
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
