namespace APPPJ.ViewModels
{
    using System;
    using System.Windows.Input;
    using APPPJ.Models;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

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

                if (App.Data.Guardar(Compra))
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
