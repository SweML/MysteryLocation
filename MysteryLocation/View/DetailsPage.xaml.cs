using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.Model;
using System.IO;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        User user;
        int id;
        public PostListElement temp;
        public ImageSource Img;
        public DetailsPage(User user, int id)
        {
            this.id = id;
            this.user = user;
            //uvm = new UnlockedViewModel(user);


            /**/

            InitializeComponent();
            foreach (PostListElement x in GlobalFuncs.uvm.Items)
            {
                if (x.Id == id)
                {
                    temp = x;
                    image.Source = x.Img;
                    imge.Source = x.Img;
                    Title.Text = x.Subject;
                    Body.Text = x.Body;
                    //More to be added. 
                }
            }
        }
    }
}