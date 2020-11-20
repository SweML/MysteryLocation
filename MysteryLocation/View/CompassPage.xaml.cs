using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{

    public partial class CompassPage : ContentPage
    {
        ViewCompass vm;
        User user;
        public CompassPage(User user)
        {
            this.user = user;
            InitializeComponent();
            BindingContext = vm = new ViewCompass();
            vm.Start();

        }
    }
}