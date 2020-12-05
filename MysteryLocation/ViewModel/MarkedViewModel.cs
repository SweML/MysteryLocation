using MysteryLocation.Model;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class MarkedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        private User user;
        private MarkedPostsPage mpp;
        public Position prevCoordinate;
        private string position;

        public string Position // User position
        {
            get { return position; }
            set
            {
                if (position != value)
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
                if(items != value) { 
                items = value;
                OnPropertyChanged("Items");
                }
            }
        }

        public MarkedViewModel(User user, MarkedPostsPage mpp)
        {
            this.user = user;
            this.mpp = mpp;
          /*  Items.Add(new PostListElement()
            {
                Id = 9999,
                Subject = "PlaceHolder 1",
                Body = "PlaceHolder Text",
                Created = "2020-09-14T12:59:21.7395836",
                LastUpdated = "2020-09-14T12:59:21.7395836",
                Position = new Coordinate(55.907875776284456, 14.067913798214885),
                Dist = ""
            });
            Items.Add(new PostListElement()
            {
                Id = 10000,
                Subject = "PlaceHolder 2",
                Body = "PlaceHolder Text",
                Created = "2020-09-14T12:59:21.7395836",
                LastUpdated = "2020-09-14T12:59:21.7395836",
                Position = new Coordinate(55.907875776284456, 14.067913798214885),
                Dist = ""
            });*/
            prevCoordinate = null;
        }

        public void updateListElements()
        {
            List<Post> posts = user.getMarked();
            foreach (Post x in posts)
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
                        Dist = ""
                    });
                }
            }
        }

        public void RecalculateDistance()
        {
            Position current = user.currentPos;
            double distance = 0;
            if (Items.Count > 0)
            {
                prevCoordinate = current;

            }
            foreach (PostListElement x in Items)
            {
                distance = current.CalculateDistance(x.Position);
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

        }

        public void AddPost(PostListElement x)
        {
            Console.WriteLine("Entering AddPost");
            Items.Add(x);
            App.user.markedSet.Add(x.Id);
            Console.WriteLine("Exiting AddPost");
            Console.WriteLine("Items count is: " + Items.Count);
        }
    }


}
