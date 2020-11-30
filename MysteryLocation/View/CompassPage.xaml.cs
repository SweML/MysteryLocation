using MysteryLocation.Model;
using System;
using Xamarin.Forms;


namespace MysteryLocation
{

    public partial class CompassPage : ContentPage
    {
        ViewCompass vm;       
        public CompassPage(User user)
        {
            InitializeComponent();
            BindingContext = vm = new ViewCompass();
            try
            {
                currentGPS.BindingContext = user;
            }
            catch(Exception e) {
                if (e.InnerException != null)
                    Console.WriteLine(e.InnerException.Message);
            }
           vm.Start();

        }
    }
}