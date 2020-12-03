using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MysteryLocation.Model
{
    public class MarkedListProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
