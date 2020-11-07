using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation.Model
{
    public class User
    {
        private List<Post> feed { get; set; }
        private List<Post> marked { get; set; }
        private List<Post> unlocked { get; set; }
        private Coordinate lastPosition { get; set; }
        private bool newUser { get; set; }

        public Category category { get; set; }
        public User(List<Post> feed, List<Post> marked, List<Post> unlocked,  bool newUser, Category category)
        {
            this.feed = feed;
            this.marked = marked;
            this.unlocked = unlocked;
            this.newUser = newUser;
            this.category = category;
        }
        /**
         * Reads the cookie file for information about the user.
         */
        public void ReadUser()
        {

        }




    }
}
