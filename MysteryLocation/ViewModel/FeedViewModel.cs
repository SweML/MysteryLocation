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
            Console.WriteLine("Reaches here");
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
            Console.WriteLine("Elements in memory is: " + memory.Count);
            List<Post> temp = filterBasedOnCategory();
            double distance = 10;
            foreach (Post x in temp)
            {
                distance = GlobalFuncs.calcDist(currentPos, x.getCoordinate());
                if (distance <= App.user.distance && !App.user.forbiddenSet.Contains(x.getId()))
                {
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
            if (chosenCategory.Length > 1)
            {
                Console.WriteLine("Chosencategory is " + chosenCategory);
                foreach (Post x in memory)
                {
                    Console.WriteLine(chosenCategory + "*ML" + " . " + x.getSubject());
                    if (chosenCategory + "*ML" == x.getSubject())
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
/*   public void updateListElements(List<Post> posts)
         {
             Items.Clear();
             HashSet<int> forbidden = App.user.forbiddenSet;
             foreach (Post x in posts)
             {
                 if (x.getCoordinate() != null && !forbidden.Contains(x.getId()))
                 {
                     if (x.getId() == 384)   // For testing static unlock func
                         App.user.tracking = x; // For testing static unlock func
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
         }*/