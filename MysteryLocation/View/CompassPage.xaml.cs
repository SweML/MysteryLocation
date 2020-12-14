using MysteryLocation.Model;
using Xamarin.Forms;


namespace MysteryLocation
{

    public partial class CompassPage : ContentPage
    {
        ViewCompass vm;
        User user;
        public CompassPage(User user)
        {
            this.user = user;
            vm = new ViewCompass(user);
            user.vc = vm;
            GPSFetcher.vc = vm;
            InitializeComponent();
            this.BindingContext = vm;

            vm.Start();
        }
    }
}