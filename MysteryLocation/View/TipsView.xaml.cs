using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionView : ContentPage
    {
        Boolean _isTapped = false;
        public TipsView()
        {
            InitializeComponent();
        }

        private async void Instruction_Clicked(object sender, EventArgs e)
        {
            if (_isTapped)
                return;

            _isTapped = true;
            await Navigation.PopModalAsync(true);
            _isTapped = false;
        }
    }
}