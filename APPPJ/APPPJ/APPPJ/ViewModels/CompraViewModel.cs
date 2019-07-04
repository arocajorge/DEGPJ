using System;


namespace APPPJ.ViewModels
{
    using APPPJ.Models;
    using APPPJ.Views;
    using APPPJ.Helpers;
    using APPPJ.Services;
    using System.Collections.ObjectModel;

    public class CompraViewModel : BaseViewModel
    {
        #region Variables
        private bool _IsEnabled;
        private bool _IsVisible;
        private DataAccess data;
        private ApiService api;
        #endregion

        #region Propiedades
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set { SetValue(ref this._IsEnabled, value); }
        }
        public bool IsVisible
        {
            get { return this._IsVisible; }
            set { SetValue(ref this._IsVisible, value); }
        }
        #endregion

        #region MyRegion
        public CompraViewModel()
        {
            data = new DataAccess();
            api = new ApiService();
        }
        #endregion

    }
}
