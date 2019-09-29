namespace APPPJ.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using APPPJ.Models;
    using APPPJ.Helpers;
    using System.Linq;
    using System;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ComboViewModel : BaseViewModel
    {
        #region Variables
        private ObservableCollection<CatalogoItemViewModel> _lstDet;
        private string _filter;
        private bool _IsRefreshing;
        private string CodigoProveedor;
        private Enumeradores.eTipoCombo TipoCombo;

        #endregion

        #region Propiedades
        public bool IsRefreshing
        {
            get { return this._IsRefreshing; }
            set
            {
                SetValue(ref this._IsRefreshing, value);
            }
        }
        public ObservableCollection<CatalogoItemViewModel> LstCatalogo
        {
            get { return this._lstDet; }
            set
            {
                SetValue(ref this._lstDet, value);
            }
        }

        public string filter
        {
            get { return this._filter; }
            set
            {
                SetValue(ref this._filter, value);
                Buscar();
            }
        }
        #endregion

        #region Constructor
        public ComboViewModel(Enumeradores.eTipoCombo _TipoCombo, string _CodigoProveedor)
        {
            CodigoProveedor = _CodigoProveedor;
            TipoCombo = _TipoCombo;
            LoadLista();
        }
        #endregion

        #region Metodos
        private IEnumerable<CatalogoItemViewModel> ToCatalogoItemModel()
        {
            var temp = App.Data.GetListCatalogo(TipoCombo,CodigoProveedor).Select(l => new CatalogoItemViewModel
            {
                Codigo = l.Codigo,
                Descripcion = l.Descripcion,
                TipoCombo = l.TipoCombo,
                CedulaRuc = l.CedulaRuc,
                Id = l.Id
            });
            return temp;
        }
        public async void LoadLista()
        {
            IsRefreshing = true;
            try
            {
                this.LstCatalogo = new ObservableCollection<CatalogoItemViewModel>(ToCatalogoItemModel());

                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                           "Alerta",
                           ex.Message,
                           "Aceptar");
                return;
            }
        }
        #endregion

        #region Comandos
        public ICommand BuscarCommand
        {
            get { return new RelayCommand(Buscar); }
        }
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadLista); }
        }

        private async void Buscar()
        {
            IsRefreshing = true;
            try
            {
                if (string.IsNullOrEmpty(filter))
                    this.LstCatalogo = new ObservableCollection<CatalogoItemViewModel>(ToCatalogoItemModel());
                else
                    this.LstCatalogo = new ObservableCollection<CatalogoItemViewModel>(
                        ToCatalogoItemModel().Where(q => q.Descripcion.ToLower().Contains(filter.ToLower())
                        ).OrderBy(q => q.Descripcion));
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
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
