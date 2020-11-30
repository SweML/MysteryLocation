using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class MarkedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items;
        private User user;
        public static Coordinate prevCoordinate;
        public ObservableCollection<PostListElement> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public MarkedViewModel(User user)
        {
            Items = new ObservableCollection<PostListElement>();
            this.user = user;
            Items.Add(new PostListElement()
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
            });
        }

        public void updateListElements()
        {
            List<Post> posts = user.getUnlocked();
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
            Coordinate current = user.currentPos;
            double distance = 0;
            if (Items.Count > 0)
            {
                prevCoordinate = current;

            }
            foreach (PostListElement x in Items)
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

        }

    }


}
