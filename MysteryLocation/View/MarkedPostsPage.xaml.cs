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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkedPostsPage : ContentPage
    {
        User user;
        public MarkedPostsPage(User user)
        {
            this.user = user;
            InitializeComponent();
        }
    }
}