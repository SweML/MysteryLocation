using System;
using MysteryLocation.Model;
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
            DependencyService.Get<SnackInterface>().SnackbarShow("User settings has been set.");
            if (GlobalFuncs.settingsActive && GlobalFuncs.gpsOn)
            { // Check to see if user has chosen a new category. 
                GlobalFuncs.fvm.updateListElements(null);
                Console.WriteLine("Does this execute?");
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










/* public async void spin()
        {
            defaultActivityIndicator.IsRunning = true;
            await Task.Delay(2000);
            defaultActivityIndicator.IsRunning = false;
        }*/
//if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
//Console.WriteLine("popup");


//private void ClosePopupClicked(object sender, RoutedEventArgs e)
//{
//    // if the Popup is open, then close it 
//    if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
//}





