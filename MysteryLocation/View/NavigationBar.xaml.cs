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
    public partial class NavigationBar : TabbedPage
    {
        public User user;
        public NavigationBar()
        {
            pageCreation();
            InitializeComponent();

        }

        private void pageCreation()
        {

            NavigationPage compass = new NavigationPage(new CompassPage());
            compass.IconImageSource = "compassIcon.png";
            compass.Title = "Compass";
            Children.Add(compass);

            NavigationPage navigationPage = new NavigationPage(new SettingsPage());
            navigationPage.IconImageSource = "settingsIcon.png";
            navigationPage.Title = "Settings";
            Children.Add(navigationPage);



            NavigationPage home = new NavigationPage(new MainPage());
            home.IconImageSource = "homeIcon.png";
            home.Title = "Home";
            Children.Add(home);

            NavigationPage marked = new NavigationPage(new MarkedPostsPage());
            marked.IconImageSource = "feedIcon.png";
            marked.Title = "Marked";
            Children.Add(marked);

            NavigationPage unlocked = new NavigationPage(new UnlockedPostsPage());
            unlocked.IconImageSource = "unlockedIcon.png";
            unlocked.Title = "Unlocked";
            Children.Add(unlocked);


        }
    }
}