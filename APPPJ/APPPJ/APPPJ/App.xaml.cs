using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace APPPJ
{
    using APPPJ.Helpers;
    using APPPJ.ViewModels;
    using APPPJ.Views;
   

    public partial class App : Application
    {
        #region Propiedades
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        static DataAccess data;
        #endregion

        public static DataAccess Data
        {
            get
            {
                if (data == null)
                    data = new DataAccess(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("DBREC.db3"));
                return data;
            }
        }

        public App()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.IdUsuario))
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                MainPage = new NavigationPage(new LoginPage());
            }else
            {
                MainViewModel.GetInstance().Compra = new CompraViewModel();
                MainPage = new MasterPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
