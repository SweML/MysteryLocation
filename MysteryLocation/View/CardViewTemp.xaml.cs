using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.Model;
using Newtonsoft.Json;

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
            //updatePosts();
            testing();
            
        }
        private async void testing()
        {
            String content = "[{ \"id\":3,\"subject\":\"New subject\",\"body\":\"New text\",\"created\":\"2020-09-14T12:59:21.7395836\",\"lastUpdated\":\"2020-09-14T12:59:32.4214212\",\"position\":{ \"longitude\":88.0,\"latitude\":9.0} },{ \"id\":4,\"subject\":\"hej på dig\",\"body\":\"mer text här\",\"created\":\"2020-09-14T13:20:10.2899228\",\"lastUpdated\":\"2020-09-14T13:20:10.2899228\",\"position\":{ \"longitude\":12.0,\"latitude\":56.0} }]";
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            PostList test = new PostList(posts);
            listview.ItemsSource = PostList.ELL;
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