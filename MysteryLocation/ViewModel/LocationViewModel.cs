using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation.ViewModel
{
    class LocationViewModel
    {
        public User user;
        
        public LocationViewModel(User user)
        {
            this.user = user;
        }

        public String Position
        {
            get { return user.currPos; }
            set { }
        }
    }

    
}
