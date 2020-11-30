using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class FeedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items;
        private User user;
        public Coordinate prevCoordinate;
        public ObservableCollection<PostListElement> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public FeedViewModel(User user)
        {
            Items = new ObservableCollection<PostListElement>();
            this.user = user;
            prevCoordinate = null;
            Console.WriteLine("Reaches here");
        }

        public void updateListElements()
        {
            Console.WriteLine("Calling fvm.updateListElements();");
            List<Post> posts = user.getFeed();
            foreach(Post x in posts)
            {
                if (x.getCoordinate() != null)
                {
                    Items.Add(new PostListElement()
                    {
                        Id = x.getId(),
                        Subject = x.getSubject(),
                        Body = x.getBody(),
                        Created = x.getCreated(),
                        LastUpdated = x.getLastUpdated(),
                        Position = x.getCoordinate(),
                        Dist = "Loading"
                    });
                }
            }
            Console.WriteLine("fvm.updateListElements(); is finished");
        }

        public void RecalculateDistance()
        {
            Coordinate current = user.currentPos;
            double distance = 0;
            if (Items.Count > 0)
            {
                Console.WriteLine("calling fvm.ReCalculateDistance();");
                prevCoordinate = current;

            }
            foreach(PostListElement x in Items)
            {
                distance = current.getDistance(x.Position);
                if (distance > 1000)
                {
                    distance /= 10;
                    x.Dist = distance.ToString() + " km";
                }
                else
                {
                    x.Dist = distance.ToString() + " m";
                }
            }
            Console.WriteLine("fvm.RecalculateDistance(); is finished");

        }
    }

    
}
