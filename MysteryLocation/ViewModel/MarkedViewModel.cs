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
        public Position currentPos;

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

        public MarkedViewModel(User user)
        {
            this.user = user;
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
            double distance = 0;
            foreach (Post x in posts)
            {
                if (markedPosts.Contains(x.getId()))
                {
                    distance = GlobalFuncs.calcDist(currentPos, x.getCoordinate());
                    Items.Add(new PostListElement()
                    {
                        Id = x.getId(),
                        Subject = x.getSubject().Substring(0, x.getSubject().Length - 3),
                        Body = x.getBody(),
                        Created = x.getCreated(),
                        LastUpdated = x.getLastUpdated(),
                        Position = x.getCoordinate(),
                        Dist = distance.ToString()
                    });
                }
            }
            
        }

        public void RecalculateDistance()
        {
            Position current = GPSFetcher.currentPosition;

            Console.WriteLine("calling fvm.ReCalculateDistance();");
            if (Items.Count > 0 && current != null)
            {
                if (prevCoordinate == null || GlobalFuncs.calcDist(current, prevCoordinate) > 500)
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
            App.user.forbiddenSet.Add(x.Id);
            Console.WriteLine("Exiting AddPost");
            Console.WriteLine("Items count is: " + Items.Count);
        }
    }


}
