namespace APPPJ.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using APPPJ.Helpers;
    using APPPJ.Models;
    using APPPJ.Services;
    using APPPJ.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class ConfiguracionViewModel : BaseViewModel
    {
        #region Variables
        private string _urlServidorExterno;
        private string _urlServidorInterno;
        private string _RutaCarpeta;
        private bool _IsRunning;
        private bool _IsEnabled;
        private ApiService api;
        private string _usuario;
        #endregion

        #region Propiedades
        public string UrlServidorExterno
        {
            get { return this._urlServidorExterno; }
            set { SetValue(ref this._urlServidorExterno, value); }
        }
        public string UrlServidorInterno
        {
            get { return this._urlServidorInterno; }
            set { SetValue(ref this._urlServidorInterno, value); }
        }
        public string RutaCarpeta
        {
            get { return this._RutaCarpeta; }
            set { SetValue(ref this._RutaCarpeta, value); }
        }
        public string Usuario
        {
            get { return this._usuario; }
            set { SetValue(ref this._usuario, value); }
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
        #endregion

        #region Constructor
        public ConfiguracionViewModel()
        {
            api = new ApiService();
            this.IsEnabled = true;
            this.Usuario = "admin";
            this.UrlServidorExterno = string.IsNullOrEmpty(Settings.UrlConexionExterna) ? "http://192.168.1.131:20000" : Settings.UrlConexionExterna;
            this.UrlServidorInterno = string.IsNullOrEmpty(Settings.UrlConexionInterna) ? "http://192.168.1.131:20000" : Settings.UrlConexionInterna;
            this.RutaCarpeta = string.IsNullOrEmpty(Settings.RutaCarpeta) ? "/Api" : Settings.RutaCarpeta;
        }
        #endregion

        #region Metodos
        public ICommand SincronizarCommand
        {
            get
            {
                return new RelayCommand(Sincronizar);
            }
        }

        private async void Sincronizar()
        {
            this.IsEnabled = false;
            this.IsRunning = true;
            int PKI = 0;

            #region Validaciones
            if (string.IsNullOrEmpty(Usuario))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar el usuario",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(UrlServidorExterno))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar la url del servidor externo",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(UrlServidorInterno))
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Debe ingresar la url del servidor interno",
                    "Aceptar");
                return;
            }

            Settings.RutaCarpeta = this.RutaCarpeta;
            Response con = await api.CheckConnection(this.UrlServidorExterno);
            if (!con.IsSuccess)
            {
                con = await api.CheckConnection(this.UrlServidorInterno);
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
                    Settings.UrlConexionActual = this.UrlServidorInterno;
            }
            else
                Settings.UrlConexionActual = this.UrlServidorExterno;
            #endregion

            Settings.UrlConexionExterna = this.UrlServidorExterno;
            Settings.UrlConexionInterna = this.UrlServidorInterno;

            #region Usuario
            var response_usuario = await api.GetList<UsuarioModel>(Settings.UrlConexionActual, RutaCarpeta, "Usuario", "?IdUsuario="+this.Usuario);
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
            var response_producto = await api.GetList<ProductoModel>(Settings.UrlConexionActual, RutaCarpeta, "Producto", "?IdUsuario=" + this.Usuario);
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
            var response_atributos = await api.GetList<ProductoDetalleModel>(Settings.UrlConexionActual, RutaCarpeta, "ProductoDetalle", "?IdUsuario=" + this.Usuario);
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
            var response_proveedor = await api.GetList<ProveedorModel>(Settings.UrlConexionActual, RutaCarpeta, "Proveedor", "?IdUsuario=" + this.Usuario);
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
            var response_proveedorProducto = await api.GetList<ProveedorProductoModel>(Settings.UrlConexionActual, RutaCarpeta, "ProveedorProducto", "?IdUsuario=" + this.Usuario);
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
            var response_compra = await api.GetObject<CompraModel>(Settings.UrlConexionActual, RutaCarpeta, "Compra", "IdUsuario=" + this.Usuario);
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

            var Compra = (CompraModel)response_compra.Result;
            Settings.IdCompra = Compra == null ? "0" : Compra.IdCompra.ToString("n0");
            #endregion

            #region Limpio los settings
            Settings.IdUsuario = Usuario;
            App.Data.DeleteAll<CompraModel>();
            #endregion

            this.IsEnabled = true;
            this.IsRunning = false;
            await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    "Configuración OK",
                    "Aceptar");

            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
