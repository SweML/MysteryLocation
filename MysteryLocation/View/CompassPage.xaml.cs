using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            try
            {
                Console.WriteLine(user.getCoordinate() + "1337");
                currentGPS.BindingContext = user;
            }
            catch(Exception e) {
                if(e.InnerException != null)
                Console.WriteLine(e.InnerException.Message + "        testing1234");
            }
           vm.Start();

        }
    }
}