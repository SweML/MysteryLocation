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
        //private MarkedPostsPage mpp;
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

      /*  private string localArea;
        public string LocalArea // User position
        {
            get { return localArea; }
            set
            {
                if (localArea != value)
                {
                    localArea = value;
                    OnPropertyChanged("LocalArea");
                }
            }
        }*/

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

        public PostListElement tracked { get; set; }

        public MarkedViewModel(User user)
        {
            this.user = user;
           // this.mpp = mpp;
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

        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            HashSet<int> markedPosts = App.user.markedSet;
            foreach (Post x in posts)
            {
                if (x.getCoordinate() != null && markedPosts.Contains(x.getId()))
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
            if (GPSFetcher.currentPosition != null)
                RecalculateDistance();
        }

        public void RecalculateDistance()
        {
            Position current = GPSFetcher.currentPosition;

            Console.WriteLine("calling fvm.ReCalculateDistance();");
            if (Items.Count > 0 && current != null)
            {
                if (prevCoordinate == null || GlobalFuncs.calcDist(current, prevCoordinate) > 10)
                {
                    double distance = 0;
                    int relevantNbrs = 0;
                    prevCoordinate = current;
                    foreach (PostListElement x in Items)
                    {
                        if (x.Position != null)
                        {
                            distance = GlobalFuncs.calcDist(current, x.Position); // Conversion to metres
                            relevantNbrs = ((int)distance).ToString().Length + 2;
                            if (distance > 1000)
                            {
                                distance /= 1000;
                                x.Dist = distance.ToString().Substring(0, relevantNbrs - 3) + " km";
                            }
                            else
                            {
                                x.Dist = distance.ToString().Substring(0, relevantNbrs) + " m";
                            }
                        }
                        Console.WriteLine("fvm.RecalculateDistance(); is finished");
                    }
                }
            }
        }

        public PostListElement RemovePost(int temp)
        {
            PostListElement refe = null;
            foreach (PostListElement x in Items)
            {
                if (x.Id == temp)
                {
                    refe = x;
                    break;
                }
            }
            if (refe != null)
                Items.Remove(refe);
            return refe;
        }



        /*    public void RecalculateDistance()
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

            }*/

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
