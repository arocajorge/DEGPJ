namespace APPPJ.ViewModels
{
    using System.Windows.Input;
    using APPPJ.Models;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class CatalogoItemViewModel : CatalogoModel
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

                MainViewModel.GetInstance().Compra.SetCombo(this.Codigo, this.CedulaRuc, this.Id, this.Descripcion, this.TipoCombo);

                await App.Navigator.Navigation.PopAsync();
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
