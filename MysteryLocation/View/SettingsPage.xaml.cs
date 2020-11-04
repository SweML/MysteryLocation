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
        public SettingsPage()
        {
            InitializeComponent();
        }

        void SavedButtonClicked(object sender, EventArgs args)
        {
            int CategoryId = ((Category)CategoryEntry.SelectedItem).CategoryId;
            Console.WriteLine("Saved");
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            double value = args.NewValue;
            displayLabel.Text = String.Format("Maximum distance {0}", value, "m");
        }

    }

}





