using System;
namespace APPPJ.ViewModels
{
    using APPPJ.Helpers;
    using APPPJ.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel
    {
        #region Propiedades
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Comandos
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
            App.Master.IsPresented = false;
            App.Master.MasterBehavior = MasterBehavior.Popover;
            switch (this.PageName)
            {
                case "LoginPage":
                    #region Limpio los settings
                    Settings.IdUsuario = string.Empty;
                    #endregion
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
                case "CompraPage":
                    MainViewModel.GetInstance().Compra = new CompraViewModel();
                    Application.Current.MainPage = new MasterPage();
                    break;
                case "ComprasPage":
                    MainViewModel.GetInstance().CompraConsulta = new CompraConsultaViewModel();
                    App.Navigator.PushAsync(new CompraConsultaPage());
                    break;
                case "SincronizacionPage":
                    MainViewModel.GetInstance().Sincronizacion = new SincronizacionViewModel();
                    App.Navigator.PushAsync(new SincronizacionPage());
                    break;
            }
        }
        #endregion
    }
}
