namespace APPPJ.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor
        public MainViewModel()
        {
            instance = this;
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
