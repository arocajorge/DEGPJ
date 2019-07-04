using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPPJ.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            this.MasterBehavior = MasterBehavior.Popover;
            App.Navigator = Navigator;
            App.Master = this;
        }
    }
}
