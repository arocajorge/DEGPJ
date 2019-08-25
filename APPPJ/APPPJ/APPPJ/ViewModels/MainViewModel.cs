using System.Collections.ObjectModel;

namespace APPPJ.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region ViewModels
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public LoginViewModel Login { get; set; }
        public ConfiguracionViewModel Configuracion { get; set; }
        public SincronizacionViewModel Sincronizacion { get; set; }
        public CompraViewModel Compra { get; set; }
        public ComboViewModel Combo { get; set; }
        public CompraConfirmacionPopUpViewModel CompraConfirmacionPopUp { get; set; }
        public CompraConsultaViewModel CompraConsulta { get; set; }
        public CompraModificarViewModel CompraModificar { get; set; }
        #region Metodos

        private void loadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_add_circle_outline",
                PageName = "CompraPage",
                Title = "Nuevo"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_history",
                PageName = "ComprasPage",
                Title = "Histórico"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_cloud_upload",
                PageName = "SincronizacionPage",
                Title = "Sincronizar"
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = "Cerrar sesión"
            });
        }

        #endregion

        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.loadMenu();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            else
                return instance;
        }
        #endregion
    }
}
