using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class FeedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        private User user;
        private string position;
        public Coordinate prevCoordinate;

        public string Position // User position
        {
            get { return position; }
            set
            {
                if(position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
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
            double distance = 0, copyDistance = 0;
            int relevantNbrs = 0;
            if (Items.Count > 0)
            {
                Console.WriteLine("calling fvm.ReCalculateDistance();");
                prevCoordinate = current;

            }
            foreach(PostListElement x in Items)
            {
                distance = current.getDistance(x.Position);
                relevantNbrs = ((int)distance).ToString().Length + 1;
                if (distance > 1000)
                {
                    distance /= 10;
                    x.Dist = distance.ToString().Substring(0, relevantNbrs) + " km";
                }
                else
                {
                    x.Dist = distance.ToString().Substring(0, relevantNbrs) + " m";
                }
            }
            Console.WriteLine("fvm.RecalculateDistance(); is finished");

        }

        public PostListElement RemovePost(Post temp)
        {
            PostListElement refe = null;
            foreach(PostListElement x in Items)
            {
                if(x.Id == temp.getId())
                {
                    refe = x;
                    break;
                }
            }
            if(refe != null)
                Items.Remove(refe);
            return refe;
        }
    }

    
}
