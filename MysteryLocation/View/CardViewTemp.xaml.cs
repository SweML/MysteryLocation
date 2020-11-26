using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.Model;


namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardViewTemp : ContentPage
    {
        private List<Post> liist = new List<Post>();
        Model.User user;
        public CardViewTemp(Model.User user, APIConnection conn)
        {
            this.user = user;
            
            InitializeComponent();
            updatePosts();


        }

        
        private async void updatePosts()
        {
            
            try
            {
                await user.updatePosts();
                List<Post> posts = user.getFeed();
                if (posts.Count >0)
                {
                    String postData = "";
                    Console.WriteLine("posts size is " + posts.Count);
                    
                    foreach (Post x in posts)
                    {
                        postData += x.ToString();
                        liist.Add(x);
                    }
                    Console.WriteLine(postData + "ABCGHJ");
                    PostList test = new PostList(liist);
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }


        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          
            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }

       
    }
}