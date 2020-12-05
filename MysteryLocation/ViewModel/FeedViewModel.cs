using MysteryLocation.Model;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MysteryLocation.ViewModel
{
    public class FeedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        //private User user;
        private string position;
        public Position prevCoordinate;

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
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public FeedViewModel(User user)
        {
            //this.user = user;
            prevCoordinate = null;
            Console.WriteLine("Reaches here");
            Task.Run(async() =>
            {
               // List<Post> posts = await App.conn.getDataAsync();
               // updateListElements(posts);
            });
        }

      /*  public void updateListElements()
        {
            Console.WriteLine("Calling fvm.updateListElements();");
            List<Post> posts = user.getFeed();
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
                        Dist = "Loading"
                    });
                }
            }
            Console.WriteLine("fvm.updateListElements(); is finished");
        }*/

        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            HashSet<int> forbidden = App.user.forbiddenSet;
            foreach (Post x in posts)
            {
                if (x.getCoordinate() != null && !forbidden.Contains(x.getId()))
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
