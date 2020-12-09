using MysteryLocation.Model;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MysteryLocation.ViewModel
{
    public class MarkedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
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
                if (items != value) {
                    items = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        public PostListElement tracked { get; set; }

        public MarkedViewModel(User user)
        {
            prevCoordinate = null;
        }

        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            HashSet<int> markedPosts = App.user.markedSet;
            currentPos = GPSFetcher.currentPosition;
            double distance = 0;
            Console.WriteLine("MVM inparamter posts size is: " + posts.Count);
            if (markedPosts.Count > 0) { 
            foreach (Post x in posts)
            {
                if (markedPosts.Contains(x.getId()))
                {
                    distance = calcDist(currentPos, x.getCoordinate());
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
                        Console.WriteLine("Nbr of items in Items is: " + Items.Count);
                        Console.WriteLine("The id of the current elemen is:" + x.getId());
                    }

            }
            }

        }

        public void RecalculateDistance()
        {
            Position current = GPSFetcher.currentPosition;

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
                            distance = calcDist(current, x.Position); // Conversion to metres
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

        public void AddPost(PostListElement x)
        {
            Console.WriteLine("Entering AddPost");
            Items.Add(x);
            App.user.markedSet.Add(x.Id);
            Console.WriteLine("Exiting AddPost");
            Console.WriteLine("Items count is: " + Items.Count);
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

        private double calcDist(Position p1, Position p2)
        {
            Double R = 6371e3; // metres
            Double φ1 = p1.Latitude * Math.PI / 180; // φ, λ in radians
            Double φ2 = p2.Latitude * Math.PI / 180;
            Double Δφ = (p2.Latitude - p1.Latitude) * Math.PI / 180;
            Double Δλ = (p2.Longitude - p1.Longitude) * Math.PI / 180;

            Double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                     Math.Cos(φ1) * Math.Cos(φ2) *
                     Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            Double d = R * c; // in metres
                              // For testing

            return d;
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

        public void AddPost(PostListElement x)
        {
            Items.Add(x);
            App.user.markedSet.Add(x.Id);
            App.user.forbiddenSet.Add(x.Id);
        }

        
    }
}
*/