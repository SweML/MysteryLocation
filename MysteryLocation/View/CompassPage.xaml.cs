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
        public CompassPage()
        {
            InitializeComponent();
            BindingContext = vm = new ViewCompass();
            vm.Start();

        }
    }
}