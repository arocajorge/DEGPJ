using System;
namespace APPPJ.ViewModels
{
    using APPPJ.Models;
    using APPPJ.Helpers;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;

    public class CompraConsultaViewModel : BaseViewModel
    {
        #region variables
        ObservableCollection<CompraModel> _ListaCompras;
        private bool _IsRefreshing;
        #endregion

        #region Propiedades
        public ObservableCollection<CompraModel> ListaCompras
        {
            get { return this._ListaCompras; }
            set
            {
                SetValue(ref this._ListaCompras, value);
            }
        }
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }
            set
            {
                SetValue(ref this._IsRefreshing, value);
            }
        }
        #endregion
        #region Singleton
        private static CompraConsultaViewModel instance;
        public static CompraConsultaViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CompraConsultaViewModel();
                return new CompraConsultaViewModel();
            }
            else
                return instance;
        }
        #endregion
        #region Constructor
        public CompraConsultaViewModel()
        {
            CargarCompras();
        }
        #endregion

        #region Metodos
        private IEnumerable<CompraItemViewModel> ToEgresoItemViewModel(List<CompraModel> lst)
        {
            return lst.Select(l => new CompraItemViewModel
            {
                PKSQLite = l.PKSQLite,
                IdCompra = l.IdCompra,
                ProvCedulaRuc = l.ProvCedulaRuc,
                ProvNombre = l.ProvNombre,
                ProvCodigo = l.ProvCodigo,
                IdUsuario = l.IdUsuario,
                Codigo = l.Codigo,
                IdProducto = l.IdProducto,
                Calificacion = l.Calificacion,
                Fecha = l.Fecha,
                Precio = l.Precio,
                Cantidad = l.Cantidad,
                Total = l.Total,
                Comentario = l.Comentario,
                Estado = l.Estado,
                prDescripcion = l.prDescripcion
            });
        }

        public void CargarCompras()
        {
            IsRefreshing = true;
            ListaCompras = new ObservableCollection<CompraModel>(ToEgresoItemViewModel(App.Data.GetListModificar()));
            IsRefreshing = false;
        }
        #endregion
    }
}
