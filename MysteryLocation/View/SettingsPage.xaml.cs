using System;
using MysteryLocation.Model;
using Plugin.Toast;
using Xamarin.Forms;

namespace MysteryLocation
{
    public partial class SettingsPage : ContentPage
    {
        public String PickerErrorMessage;
        public String SliderErrorMessage;
        private double value = 0;
        private int CategoryId = 0;
        public SettingsPage(User user)
        {
            GPSFetcher.cavm = GlobalFuncs.svm;
            this.BindingContext = GlobalFuncs.svm;

            InitializeComponent();

            value = App.user.getDistance();
            value = value / 1000;
            CategoryId = App.user.getCategory();

            displayLabel.Text = String.Format("Maximum distance {0}km", value);
            CategoryEntry.SelectedIndex = CategoryId;
        }

        

        void SavedButtonClicked(object sender, EventArgs args)
        {

            


            if ((Category)CategoryEntry.SelectedItem == null)
            {
                PickerError.Text = "Choose a category";

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
            App.user.setCategory(CategoryId);
            App.user.setDistance(value * 1000);
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    DependencyService.Get<SnackInterface>().SnackbarShow("User settings has been set");
                    break;
                case Device.iOS:
                    CrossToastPopUp.Current.ShowToastMessage("User settings has been set");
                    break;
                default:
                    break;
            }
            if (GlobalFuncs.settingsActive && GlobalFuncs.gpsOn)
            { 
                GlobalFuncs.fvm.updateListElements(null);
                
            }
            GlobalFuncs.settingsActive = true;

        }


        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            value = args.NewValue;
            displayLabel.Text = String.Format("Maximum distance {0}km", value);
        }
    }
}



