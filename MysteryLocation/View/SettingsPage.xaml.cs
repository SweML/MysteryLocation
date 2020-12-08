using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation
{
    public partial class SettingsPage : ContentPage
    {
        public String PickerErrorMessage;
        public String SliderErrorMessage;

        private double value = 0;
        private int CategoryId = 0;

        User user;

        //public object PickerError { get; private set; }

        public SettingsPage(User user)
        {
            this.user = user;
            CategoryViewModel cavm = new CategoryViewModel();
            GPSFetcher.cavm = cavm;
            this.BindingContext = cavm;
            InitializeComponent();
          //  currentGPS.BindingContext = user;
        }

        void SavedButtonClicked(object sender, EventArgs args)
        {
               if ((Category)CategoryEntry.SelectedItem == null)
               {
                   PickerError.Text = "Choose a category";
                   //if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
                   //Console.WriteLine("popup");
                   if (value == 0)
                   {
                       SliderError.Text = "Choose a distance";
                       return;
                   }
                   SliderError.Text = "";
                   return;
               }
               if (value == 0)
               {
                   PickerError.Text = "";
                   SliderError.Text = "Choose a distance";
                   return;
               }
               PickerError.Text = "";
               SliderError.Text = "";
               CategoryId = ((Category)CategoryEntry.SelectedItem).CategoryId;
               user.setCategory(CategoryId);
               user.setDistance(value * 1000);
            if (GlobalFuncs.settingsActive && GlobalFuncs.gpsOn)
                GlobalFuncs.fvm.updateListElements(null);
               GlobalFuncs.settingsActive = true;
               Console.WriteLine();
               Console.WriteLine("Saved");
            }

        //private void ClosePopupClicked(object sender, RoutedEventArgs e)
        //{
        //    // if the Popup is open, then close it 
        //    if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
        //}

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            value = args.NewValue;
            displayLabel.Text = String.Format("Maximum distance {0}km", value);
        }

        public async void spin()
        {
            defaultActivityIndicator.IsRunning = true;
            await Task.Delay(2000);
            defaultActivityIndicator.IsRunning = false;
        }
    }
}





