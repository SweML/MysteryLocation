using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MysteryLocation.Model
{
    public class User : IUser, INotifyPropertyChanged
    {
        private List<Post> feed { get; set; }
        private List<Post> marked { get; set; }
        private List<Post> unlocked { get; set; }
        private Coordinate lastPosition { get; set; }
        private bool newUser { get; set; }
        private HashSet<int> unlockedSet;
        private HashSet<int> markedSet;


        public int category;

        public Coordinate currentPos;

        public String currentPosition;

        public String CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                if (currentPosition != value)
                {
                    currentPosition = value;
                    this.OnPropertyChanged("CurrentPosition");
                }
            }
        }
    

    public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }



        public double distance;


        private Post tracking { get; set; }
        private int tracker;
        private APIConnection conn;
        private int counter;

        

        public User(bool newUser, int category, APIConnection conn)
        {
            this.feed = new List<Post>();
            this.marked = new List<Post>();
            this.unlocked = new List<Post>();
            this.newUser = newUser;
            this.category = category;
            this.conn = conn;
            currentPosition = "Loading";
            unlockedSet = new HashSet<int>();
            markedSet = new HashSet<int>();
            updatePosts();
            counter = 0;
        }

       
        public bool isNewUser()
        {
            return newUser;
        }

        public String getCoordinate()
        {
            if (currentPos != null)
                return currentPos.toString();
            return "Loading";
        }

        public void setPosition(Coordinate newCoord)
        {
           // counter++;
           // Console.WriteLine("New coordinate");
            currentPos = newCoord; // Sätter rätt koordinat

            //currentPosition = currentPos.toString() + counter;
            CurrentPosition = newCoord.toString(); //+ counter; // Uppdaterar labels i views

          //  Console.WriteLine(CurrentPosition + "Testingdfdgdg");
        }

        public void setCategory(int cat)
        {
            category = cat;
            Console.WriteLine("The user category has been changed to " + category.ToString());
        }
        public void setDistance(double dist)
        {
            distance = dist;
            Console.WriteLine("The user distance has been changed to " + distance.ToString());
        }
        /**
         * Reads the cookie file for information about the user.
         */
        public void ReadUser()
        {
            var filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "myFile.txt");
            if (File.Exists(filename))
            {
                newUser = false;
                String fileInfo = System.IO.File.ReadAllText(filename);
                using (StreamReader file = new StreamReader(filename))
                {
                    int lineCounter = 0;
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        lineCounter++;
                        if (ln.Contains("*"))
                            counter++;
                        else if (counter == 0)
                        {
                            if (lineCounter == 1)
                            {
                                category = Int16.Parse(ln);
                            }
                            else
                            {
                                // Sätt önskat avstånd
                            }
                        }
                        else if (counter == 1)
                            unlockedSet.Add(Int16.Parse(ln));
                        else if (counter == 2)
                            markedSet.Add(Int16.Parse(ln));
                        else
                        {
                            tracker = Int16.Parse(ln);
                        }
                    }
                    file.Close();
                }
            }
        }

        public void SaveUser()
        {
            String infoToSave = "";
            infoToSave = category + "\n";
            foreach (Post x in unlocked)
            { // Saving info from unlocked
                infoToSave += x.getId() + "\n";
            }
            infoToSave += "*\n"; // To indicate the end of previous
            foreach (Post x in marked)
            { // Saving info from unlocked
                infoToSave += x.getId() + "\n";
            }
        }

        // Method to set which post is being tracked.
        public void addTracker(int observationId)
        {
            foreach (Post x in marked)
            {
                if (x.getId() == observationId)
                {
                    tracking = x;
                    marked.Remove(x);
                }
            }
        }

        public async Task updatePosts()
        {
            List<Post> fromAPI;
            fromAPI = await conn.getDataAsync();
            if (!newUser)
            {
                if (markedSet.Count > 0 || unlockedSet.Count > 0)
                {
                    foreach (Post x in fromAPI)
                    {
                        if (markedSet.Contains(x.getId()))
                        {
                            marked.Add(x);
                            fromAPI.Remove(x);
                        }
                        else if (unlockedSet.Contains(x.getId()))
                        {
                            unlocked.Add(x);
                            fromAPI.Remove(x);
                        }
                        else if (tracker == x.getId())
                        {
                            tracking = x;
                            fromAPI.Remove(x);
                        }
                        else
                        {
                            feed.Add(x);
                        }
                    }
                }
            }
            else
            {
                feed = fromAPI;
            }

            Console.WriteLine("User now holds " + feed.Count + " posts");

        }

        public List<Post> getFeed()
        {
            return feed;
        }

        public List<Post> getUnlocked()
        {
            return unlocked;
        }

        public List<Post> getMarked()
        {
            return marked;
        }
    }
}
