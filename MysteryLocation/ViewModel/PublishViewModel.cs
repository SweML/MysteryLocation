using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;

namespace MysteryLocation.ViewModel
{
    public class PublishViewModel : PostListProperty
    {
        public string positionLocation;

        public string longitude;

        public string latitude;

        private Timer timer;

        public IList<Category> CatList { get; set; }
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

        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // if (PropertyChanged != null)
            Console.WriteLine("Ticking");
                OnPropertyChanged("Now");
               // PropertyChanged(this, new PropertyChangedEventArgs("Now"));
        }

        public PublishViewModel()
        {
            timer = new Timer();
            try
            {
                CatList = new ObservableCollection<Category>();
                CatList.Add(new Category { CategoryId = 1, CategoryName = "Historisk" });
                CatList.Add(new Category { CategoryId = 2, CategoryName = "Natur" });
                CatList.Add(new Category { CategoryId = 3, CategoryName = "Arkitektur" });
                CatList.Add(new Category { CategoryId = 4, CategoryName = "Modern arkitektur" });
                CatList.Add(new Category { CategoryId = 5, CategoryName = "Historisk arkitektur" });
            }
            catch (Exception ex)
            {

            }

        }
        public void startTimer()
        {
            timer.Interval = 1000; // 1 second updates
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public void stopTimer()
        {
            timer.Stop();
        }
    }
}
