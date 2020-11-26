using MysteryLocation.Model;
using MysteryLocation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    public partial class NavigationBar : TabbedPage
    {
        public User user;
        public APIConnection conn;
        public NavigationBar(User user, APIConnection conn)
        {
            this.user = user;
            this.conn = conn;
            pageCreation();
            InitializeComponent();

        }

        private void pageCreation()
        {

            NavigationPage compass = new NavigationPage(new CompassPage(user));
            compass.IconImageSource = "compassIcon.png";
            compass.Title = "Compass";
            Children.Add(compass);

            NavigationPage navigationPage = new NavigationPage(new SettingsPage(user));
            navigationPage.IconImageSource = "settingsIcon.png";
            navigationPage.Title = "Settings";
            Children.Add(navigationPage);



            NavigationPage home = new NavigationPage(new CardViewTemp(user, conn));
            home.IconImageSource = "homeIcon.png";
            home.Title = "Home";
            Children.Add(home);

            NavigationPage marked = new NavigationPage(new MarkedPostsPage(user));
            marked.IconImageSource = "feedIcon.png";
            marked.Title = "Marked";
            Children.Add(marked);

           /* NavigationPage unlocked = new NavigationPage(new UnlockedPostsPage(user));
            unlocked.IconImageSource = "unlockedIcon.png";
            unlocked.Title = "Unlocked";
            Children.Add(unlocked);
           */
            NavigationPage publish = new NavigationPage(new PublishPage(user, conn));
            publish.IconImageSource = "unlockedIcon.png";
            publish.Title = "Publish";
            Children.Add(publish);


        }
    }
}