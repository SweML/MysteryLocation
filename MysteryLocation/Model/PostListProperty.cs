﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MysteryLocation.Model
{
    public class PostListProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            //var handler = PropertyChanged;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}