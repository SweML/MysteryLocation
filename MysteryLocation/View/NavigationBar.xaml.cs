using MysteryLocation.Model;
using MysteryLocation.View;
using Xamarin.Forms;

namespace MysteryLocation
{
    public partial class NavigationBar : TabbedPage
    {
        public User user;
        public APIConnection conn;
        public int flag;
        public static int flago = 0;
        public NavigationBar(User user, APIConnection conn)
        {
            this.user = user;
            this.conn = conn;
            if (App.user.termsFlag == 0)
            {

                pageCreation();
                pushTerms();
            }
            else
            {
                pageCreation();
            }

            InitializeComponent();
            if (!App.user.newUser)
                CurrentPage = Children[2]; // If previous user open feed.
            else
                CurrentPage = Children[0]; // For newUser open settingspage.
        }

        private void pageCreation()
        {
            this.Children.Add(new SettingsPage(user) { Title = "Settings", IconImageSource = "settingsIcon.png" });
            this.Children.Add(new CompassPage(user) { Title = "Compass", IconImageSource = "compassIcon.png" });
            this.Children.Add(new CardViewTemp(user) { });
            this.Children.Add(new MarkedPostsPage(user) { Title = "Marked", IconImageSource = "feedIcon.png" });
            this.Children.Add(new UnlockedPostsPage(user) { Title = "Unlocked", IconImageSource = "unlockedIcon.png" });
        }

        private async void pushTerms()
        {
            await Navigation.PushModalAsync(new TermsView());
        }
    }
}