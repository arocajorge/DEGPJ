namespace APPPJ.ViewModels
{
    using System.Windows.Input;
    using APPPJ.Models;
    using APPPJ.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    public class CompraItemViewModel : CompraModel
    {
        #region Comandos
        public ICommand SelectCommand
        {
            get { return new RelayCommand(Select); }
        }

        private async void Select()
        {
            try
            {
                App.Master.IsPresented = false;
                MainViewModel.GetInstance().CompraModificar = new CompraModificarViewModel(this);
                await App.Navigator.Navigation.PushAsync(new CompraModificarPage());
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alerta",
                    ex.Message,
                    "Aceptar");
            }

        }
        #endregion
    }
}
