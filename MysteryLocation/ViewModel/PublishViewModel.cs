using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class PublishViewModel : PostListProperty
    {
        private string positionLocation;
        public string PositionLocation // User position
        {
            get { return positionLocation; }
            set
            {
                if (positionLocation != value)
                {
                    positionLocation = value;
                    OnPropertyChanged("PositionLocation");
                }
            }
        }
    }
}
