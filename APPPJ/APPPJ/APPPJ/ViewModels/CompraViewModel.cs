using System;


namespace APPPJ.ViewModels
{
    using APPPJ.Models;
    using APPPJ.Views;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using System.Linq;
    using System.Collections.Generic;
    using Rg.Plugins.Popup.Services;

    public class CompraViewModel : BaseViewModel
    {
        #region Variables
        private bool _IsEnabled;
        private bool _IsVisible;
        private bool _IsRunning;
        private bool _IsVisibleBuscar;
        private CompraModel Compra;
        private string _NomProveedor;
        private string _NomProducto;
        private int _Height;
        private double _Cantidad;
        private DateTime _Fecha;
        private ObservableCollection<ProductoDetalleModel> _ListaDetalle;
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
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); }
        }
        public bool IsVisibleBuscar
        {
            get { return this._IsVisibleBuscar; }
            set { SetValue(ref this._IsVisibleBuscar, value); }
        }
        public string NomProducto
        {
            get { return this._NomProducto; }
            set { SetValue(ref this._NomProducto, value); }
        }
        public string NomProveedor
        {
            get { return this._NomProveedor; }
            set { SetValue(ref this._NomProveedor, value); }
        }
        public double Cantidad
        {
            get { return this._Cantidad; }
            set { SetValue(ref this._Cantidad, value); }
        }
        public ObservableCollection<ProductoDetalleModel> ListaDetalle
        {
            get { return this._ListaDetalle; }
            set { SetValue(ref this._ListaDetalle, value); }
        }
        public int Height
        {
            get { return this._Height; }
            set { SetValue(ref this._Height, value); }
        }
        public DateTime Fecha
        {
            get { return this._Fecha; }
            set { SetValue(ref this._Fecha, value); }
        }
        #endregion

        #region MyRegion
        public CompraViewModel()
        {
            IsEnabled = true;
            Compra = new CompraModel();
            Height = 0;
            Cantidad = 0;
            Fecha = DateTime.Now.Date;
        }
        #endregion

        #region Metodos
        public void SetCombo(string Codigo, string CedulaRuc,int Id, string Descripcion, Enumeradores.eTipoCombo _TipoCombo)
        {
            switch (_TipoCombo)
            {
                case Enumeradores.eTipoCombo.Producto:
                    NomProducto = Descripcion;
                    Compra.IdProducto = Id;
                    Compra.prDescripcion = Descripcion;
                    CargarDetalleProducto();
                    break;
                case Enumeradores.eTipoCombo.Proveedor:
                    NomProveedor = Descripcion;
                    Compra.ProvCodigo = Codigo;
                    Compra.ProvNombre = Descripcion;
                    Compra.ProvCedulaRuc = CedulaRuc;
                    ValidarProveedor();
                    break;
            }
        }

        private async void CargarDetalleProducto()
        {
            try
            {
                ListaDetalle = new ObservableCollection<ProductoDetalleModel>(App.Data.GetListProducto(Compra.IdProducto));
                Height = ListaDetalle.Count * 50;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           ex.Message,
                           "Aceptar");
                return;
            }
        }

        private async void ValidarProveedor()
        {
            try
            {
               var Lista = App.Data.GetListProveedorProducto(Compra.ProvCodigo);
                if (Lista.Count > 1)
                {
                    IsVisibleBuscar = true;
                    NomProducto = string.Empty;
                    Compra.IdProducto = 0;
                    Compra.prDescripcion = string.Empty;
                }
                else
                {
                    IsVisibleBuscar = false;
                    Compra.IdProducto = Lista.FirstOrDefault().IdProducto;
                    NomProducto = Lista.FirstOrDefault().Descripcion;
                    Compra.prDescripcion = NomProducto;
                }
                CargarDetalleProducto();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           ex.Message,
                           "Aceptar");
                return;
            }
        }
        #endregion

        #region Comandos
        public ICommand BuscarProductoCommand
        {
            get { return new RelayCommand(BuscarProducto); }
        }

        private async void BuscarProducto()
        {
            MainViewModel.GetInstance().Combo = new ComboViewModel(Enumeradores.eTipoCombo.Producto, Compra.ProvCodigo);
            await App.Navigator.PushAsync(new ComboPage());
        }

        public ICommand BuscarProveedorCommand
        {
            get { return new RelayCommand(BuscarProveedor); }
        }

        private async void BuscarProveedor()
        {
            MainViewModel.GetInstance().Combo = new ComboViewModel(Enumeradores.eTipoCombo.Proveedor,null);
            await App.Navigator.PushAsync(new ComboPage());
        }

        public ICommand CalificarCommand
        {
            get { return new RelayCommand(Calificar); }
        }

        private async void Calificar()
        {
            try
            {
                IsRunning = true;
                IsEnabled = false;

                if (Fecha == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo fecha es obligatorio",
                            "Aceptar");
                    return;
                }

                if (Fecha == DateTime.MinValue)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo fecha es obligatorio",
                            "Aceptar");
                    return;
                }

                if (string.IsNullOrEmpty(Compra.ProvCodigo))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo proveedor es obligatorio",
                            "Aceptar");
                    return;
                }

                if (Compra.IdProducto == 0)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo producto es obligatorio",
                            "Aceptar");
                    return;
                }

                if (Cantidad == 0 | Cantidad < 0)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo cantidad es obligatorio",
                            "Aceptar");
                    return;
                }

                if(ListaDetalle.Where(q => q.EsObligatorio && (q.Minimo > q.Calificacion || q.Calificacion > q.Maximo)).Count() > 0)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "La calificación ingresada no cumple con uno o varios parámetros obligatorios, no se puede proceder con la compra",
                            "Aceptar");
                    return;
                }

                var Producto = App.Data.GetProducto(Compra.IdProducto);
                if (Producto == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo producto es obligatorio",
                            "Aceptar");
                    return;
                }

                Compra.Fecha = Fecha;
                foreach (var q in ListaDetalle)
                {
                    q.PonderacionFinal = 0;
                    if (q.Minimo <= q.Calificacion && q.Calificacion <= q.Maximo)
                    {
                        if (Math.Round(q.PorcentajeMinimo,2) == 100)
                        {
                            q.PonderacionFinal = q.Ponderacion;
                        }
                        else
                        {
                            double RangoCalificacion = q.Maximo - q.Minimo;
                            double RangoSegunPreferencia = q.ValorOptimo == "MAXIMO" ? q.Calificacion - q.Minimo : q.Maximo - q.Calificacion;
                            double RangoPorcentual = (RangoSegunPreferencia / RangoCalificacion) * 100;
                            double PonderacionPorAsignar = 100 - q.PorcentajeMinimo;
                            double PonderacionRangoPorcentual = PonderacionPorAsignar * (RangoPorcentual / 100);
                            double PorcentajePonderacion = PonderacionRangoPorcentual + q.PorcentajeMinimo;
                            q.PonderacionFinal = q.Ponderacion * (PorcentajePonderacion / 100);
                        }
                    }
                }

                double Ponderacion = ListaDetalle.Sum(q => q.PonderacionFinal);
                double Min = 0;
                double Max = 0;
                double PonderacionMin = 0;
                double PonderacionMax = 0;

                if (Ponderacion > Producto.CalificacionD)
                {
                    if (Ponderacion > Producto.CalificacionC)
                    {
                        if (Ponderacion > Producto.CalificacionB)
                        {
                            if (Ponderacion > Producto.CalificacionA)
                            {
                                Min = Producto.PrecioA;
                                Max = Producto.PrecioA;
                                PonderacionMin = Producto.CalificacionA;
                                PonderacionMax = Producto.CalificacionA;
                            }
                            else
                            {
                                Min = Producto.PrecioB;
                                Max = Producto.PrecioA;
                                PonderacionMin = Producto.CalificacionB;
                                PonderacionMax = Producto.CalificacionA;
                            }
                        }
                        else
                        {
                            Min = Producto.PrecioC;
                            Max = Producto.PrecioB;
                            PonderacionMin = Producto.CalificacionC;
                            PonderacionMax = Producto.CalificacionB;
                        }
                    }
                    else
                    {
                        Min = Producto.PrecioD;
                        Max = Producto.PrecioC;
                        PonderacionMin = Producto.CalificacionD;
                        PonderacionMax = Producto.CalificacionC;
                    }
                }
                else
                {
                    Min = 0;
                    Max = 0;
                }

                if (Min == 0)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "La calificación del producto no es aceptable, no se puede proceder con la compra",
                            "Aceptar");
                    return;
                }

                switch(Producto.EscogerPrecioPor)
                {
                    case "MASALTO":
                        Compra.Precio = Max;
                        break;
                    case "MASBAJO":
                        Compra.Precio = Min;
                        break;
                    case "CERCANO":
                        var RangoMin = Ponderacion - PonderacionMax;
                        var RangoMax = PonderacionMax - Ponderacion;
                        if (RangoMin < RangoMax)
                            Compra.Precio = Min;
                        else
                            Compra.Precio = Max;
                        break;
                }
                Compra.Calificacion = Ponderacion;
                Compra.Cantidad = Cantidad;
                var lstDetalle = new List<ProductoDetalleModel>(ListaDetalle);
                var lst = lstDetalle.Select(q => new CompraDetalleModel
                {
                    Secuencia = (int)q.Secuencia,
                    Descripcion = q.Descripcion,
                    Minimo = q.Minimo,
                    Maximo = q.Maximo,
                    Ponderacion = q.Ponderacion,
                    EsObligatorio = q.EsObligatorio,
                    PorcentajeMinimo = q.PorcentajeMinimo,
                    ValorOptimo = q.ValorOptimo,
                    Valor = q.Calificacion
                }).ToList();
                MainViewModel.GetInstance().CompraConfirmacionPopUp = new CompraConfirmacionPopUpViewModel(Compra, lst);
                await PopupNavigation.PushAsync(new CompraConfirmacionPopUpPage());

                IsRunning = false;
                IsEnabled = true;
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
