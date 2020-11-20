using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryLocation.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation
{
    public partial class SettingsPage : ContentPage
    {
        User user;
        public SettingsPage(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        void SavedButtonClicked(object sender, EventArgs args)
        {
            int CategoryId = ((Category)CategoryEntry.SelectedItem).CategoryId;
            user.setCategory(CategoryId);
            Console.WriteLine();
            Console.WriteLine("Saved");
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            displayLabel.Text = String.Format("Maximum distance {0}", value, "m");
        }



        public async void spin()
        {

            defaultActivityIndicator.IsRunning = true;
            await Task.Delay(2000);
            defaultActivityIndicator.IsRunning = false;
        }



    }

}





