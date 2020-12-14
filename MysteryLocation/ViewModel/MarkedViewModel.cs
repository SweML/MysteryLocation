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
        public Position pos;
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
                if (items != value)
                {
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

        public void setTracker(int obsId)
        {
            foreach (PostListElement x in Items)
            {
                if (x.Id == obsId)
                {
                    tracked = x;
                    tracked.Color = "#1f9f64";
                }
            }
        }

        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            HashSet<int> markedPosts = App.user.markedSet;
            currentPos = GPSFetcher.currentPosition;
            double distance = 0;
            string buttonColor = "";
            string textDist;
            if (markedPosts.Count > 0)
            {
                foreach (Post x in posts)
                {
                    buttonColor = "#404040";
                    if (x.getId() == App.user.tracker)
                        buttonColor = "#1f9f64";
                    if (markedPosts.Contains(x.getId()))
                    {
                        distance = calcDist(currentPos, x.getCoordinate());
                        if (distance > 1000)
                        {
                            distance /= 1000;
                            textDist = Math.Round(distance, 1).ToString() + " km";
                        }
                        else
                        {
                            textDist = Math.Round(distance, 1).ToString() + " m";
                        }

                        Items.Add(new PostListElement()
                        {


                            Id = x.getId(),
                            Subject = x.getSubject().Substring(0, x.getSubject().Length - 3),
                            Body = x.getBody(),
                            Created = x.getCreated(),
                            LastUpdated = x.getLastUpdated(),
                            Position = x.getCoordinate(),
                            Dist = distance.ToString(),
                            Color = buttonColor
                        });
                 
                    }


                }
            }
            if (App.user.tracker > 0)
            {
                foreach (PostListElement x in Items)
                {
                    if (x.Id == App.user.tracker)
                    {
                        tracked = x;
                        GlobalFuncs.addTracker(x.Id);
                        setMyPos(currentPos);
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
                    prevCoordinate = current;
                    foreach (PostListElement x in Items)
                    {
                        if (x.Position != null)
                        {
                            distance = calcDist(current, x.Position); // Conversion to metres
                            if (distance > 1000)
                            {
                                distance /= 1000;
                                x.Dist = Math.Round(distance, 1).ToString() + " km";
                            }
                            else
                            {
                                x.Dist = distance.ToString() + " m"; // Math.round not needed here.
                            }
                        }
                    }
                }
            }
        }

        public void AddPost(PostListElement x)
        {
            Items.Add(x);
            App.user.markedSet.Add(x.Id); // To keep track of which posts to save where.

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
            {
                Items.Remove(refe);
                App.user.markedSet.Remove(refe.Id); // To keep track of which posts to save where.
            }
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

            return Math.Round(d, 1);
        }

        public void setMyPos(Position p)
        {
            pos = p;
        }
        public Position returnmypos()
        {
            return pos;
        }
    }
}