namespace APPPJ.ViewModels
{
    using System.Windows.Input;
    using APPPJ.Helpers;
    using APPPJ.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Variables
        private string _usuario;
        private string _contrasenia;
        private bool _IsEnabled;
        private bool _IsRunning;
        private DataAccess data;
        #endregion

        #region Propiedades
        public string usuario
        {
            get { return this._usuario; }
            set { SetValue(ref this._usuario, value); }
        }
        public string contrasenia
        {
            get { return this._contrasenia; }
            set { SetValue(ref this._contrasenia, value); }
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
        public LoginViewModel()
        {
            data = new DataAccess();
            IsEnabled = true;
        }
        #endregion

        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public async void Login()
        {
            try
            {
                IsEnabled = false;
                IsRunning = true;

                if (string.IsNullOrEmpty(usuario))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo usuario es obligatorio",
                            "Aceptar");
                    return;
                }

                if (string.IsNullOrEmpty(contrasenia))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "El campo contraseña es obligatorio",
                            "Aceptar");
                    return;
                }

                if (usuario.ToLower() == "admin" && contrasenia.ToLower() == "admin")
                {
                    IsRunning = false;
                    IsEnabled = true;
                    this.contrasenia = string.Empty;
                    Settings.IdUsuario = string.Empty;
                    MainViewModel.GetInstance().Configuracion = new ConfiguracionViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new ConfiguracionPage());
                    return;
                }

                var ModelUsuario = data.GetUsuario(usuario, contrasenia);
                if (ModelUsuario == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert(
                            "Alerta",
                            "Credenciales incorrectas",
                            "Aceptar");
                    return;
                }

                MainViewModel.GetInstance().Compra = new CompraViewModel();
                Application.Current.MainPage = new MasterPage();

                Settings.IdUsuario = ModelUsuario.IdUsuario;
                IsRunning = false;
                IsEnabled = true;
            }
            catch (System.Exception ex)
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
