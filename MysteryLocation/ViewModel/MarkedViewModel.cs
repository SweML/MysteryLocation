using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class MarkedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> markeditems;
        private User user;
        public Coordinate prevCoordinate;
        public ObservableCollection<PostListElement> MarkedItems
        {
            get { return markeditems; }
            set
            {
                markeditems = value;
                OnPropertyChanged("MarkedItems");
            }
        }

        public MarkedViewModel(User user)
        {
            MarkedItems = new ObservableCollection<PostListElement>();
            this.user = user;
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
            List<Post> posts = user.getUnlocked();
            foreach (Post x in posts)
            {
                if (x.getCoordinate() != null)
                {
                    MarkedItems.Add(new PostListElement()
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
            if (MarkedItems.Count > 0)
            {
                prevCoordinate = current;

            }
            foreach (PostListElement x in MarkedItems)
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

        public void AddPost(PostListElement x)
        {
            Console.WriteLine("Entering AddPost");
            MarkedItems.Add(x);
           
            Console.WriteLine("Exiting AddPost");
            Console.WriteLine("Items count is: " + MarkedItems.Count);
        }
    }


}
