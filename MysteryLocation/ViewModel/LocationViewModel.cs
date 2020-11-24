using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        public User user;
        private String currentPosition;
        public LocationViewModel(User user)
        {
            this.user = user;
            currentPosition = string.Empty;
        }

        public String Position
        {
            /*get { return user.currPos; }
            set { }*/
            get => user.currentPosition;
            set
            {

                Position = value;
                OnPropertyChanged(user.currentPosition);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string Coord)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Coord));
        }
    }

    
}
