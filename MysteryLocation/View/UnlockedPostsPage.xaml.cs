using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnlockedPostsPage : ContentPage
    {
        public User user;
        public UnlockedPostsPage(User user)
        {
            this.user = user;
            InitializeComponent();
            currentGPS.BindingContext = new LocationViewModel(user);
        }
    }
}