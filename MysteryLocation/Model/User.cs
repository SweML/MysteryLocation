using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation.Model
{
    class User
    {
        private List<Post> feed { get; set; }
        private List<Post> marked { get; set; }
        private List<Post> unlocked { get; set; }
        private Coordinate lastPosition { get; set; }
        private bool newUser { get; set; }

        private Category category { get; set; }
        public User(List<Post> feed, List<Post> marked, List<Post> unlocked, Coordinate lastPosition, bool newUser, Category category)
        {
            this.feed = feed;
            this.marked = marked;
            this.unlocked = unlocked;
            this.lastPosition = lastPosition;
            this.newUser = newUser;
            this.category = category;
        }


    }
}
