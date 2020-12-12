using MysteryLocation.Model;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MysteryLocation.ViewModel
{
    public class FeedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        private string position;
        public Position currentPos;
        public Position prevCoordinate;
        private List<Post> memory;

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
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public FeedViewModel(User user)
        {
            prevCoordinate = null;
            memory = null;
        }


      

        /*
         *  This method will only ever be called when the GPSFetcher has a current Position.
         */
        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            currentPos = GPSFetcher.currentPosition; // So that all methods use the same Position.
            if (posts != null)
                memory = posts; // Save the list for later. Ha en metod som tar bort icke valid inlägg
            List<Post> temp = filterBasedOnCategory();
            double distance = 10;
            foreach (Post x in temp)
            {
                distance = calcDist(currentPos, x.getCoordinate());
                if (distance <= App.user.distance && !App.user.markedSet.Contains(x.getId()) && !App.user.unlockedSet.Contains(x.getId()))
                {
                    Items.Add(new PostListElement()
                    {
                        Id = x.getId(),
                        Subject = x.getSubject().Substring(0, x.getSubject().Length - 3),
                        Body = x.getBody(),
                        Created = x.getCreated(),
                        LastUpdated = x.getLastUpdated(),
                        Position = x.getCoordinate(),
                        Dist = distance.ToString(),
                        Color = "#404040"
                    });

                }
            }

            Items = OrderThoseGroups(Items);
        }

        public static ObservableCollection<PostListElement> OrderThoseGroups(ObservableCollection<PostListElement> orderThoseGroups)
        {
            ObservableCollection<PostListElement> temp;
            temp = new ObservableCollection<PostListElement>(orderThoseGroups.OrderBy(PostListElement => PostListElement.Created)); // double.Parse(PostListElement.Dist)
            temp = new ObservableCollection<PostListElement>(temp.Reverse());  //                    Lägg in komparator här om tid.
            orderThoseGroups.Clear();
            foreach (PostListElement j in temp)
            {
                orderThoseGroups.Add(j);
                Console.WriteLine(j.Created);
            }
            
            return orderThoseGroups;
        }

        private List<Post> filterBasedOnCategory()
        {
            List<Post> temp = new List<Post>();
            string chosenCategory = "";
            int nbr = App.user.category;
            switch (nbr)
            {
                case 1:
                    chosenCategory = "Historisk";
                    break;
                case 2:
                    chosenCategory = "Natur";
                    break;
                case 3:
                    chosenCategory = "Arkitektur";
                    break;
                case 4:
                    chosenCategory = "Modern arkitektur";
                    break;
                case 5:
                    chosenCategory = "Historisk arkitektur";
                    break;
                default:
                    break;
            }
            if (chosenCategory.Length > 1 && memory != null)
            {
                foreach (Post x in memory)
                {
                    if (chosenCategory + GlobalFuncs.marker == x.getSubject())
                    {
                        temp.Add(x);

                    }
                }
            }
            return temp;
        }


        public void RecalculateDistance()
        {
            Position current = GPSFetcher.currentPosition;
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
                                x.Dist = Math.Round(distance, 1).ToString() + " km";
                            }
                            else
                            {
                                x.Dist = Math.Round(distance, 1).ToString() + " m";
                            }
                        }
                    }
                }
            }
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

            return Math.Round(d, 1);
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
    }
}