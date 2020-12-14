using MysteryLocation.Model;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using Xamarin.Forms;

namespace MysteryLocation.ViewModel
{
    public class UnlockedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        private User user;
        private string position;
        public List<byte[]> images = new List<byte[]>();

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
            {   if(items != value) { 
                items = value;
                OnPropertyChanged("Items");
                }
            }
        }

        public UnlockedViewModel(User user)
        {
           
        }

        public void addUnlockedPost(PostListElement x, UnlockedPosts attachment)
        {
            App.user.unlockedSet.Add(x.Id);
            images.Add(attachment.imgBytes);
            Items.Add(new PostListElement()
            {
                Id = x.Id,
                Subject = x.Subject,
                Body = x.Body,
                Created = x.Created,
                LastUpdated = x.LastUpdated,
                Position = x.Position,
                Dist = "", 
                Img = ImageSource.FromStream(() => new MemoryStream(attachment.imgBytes))
             });
            App.user.saveImage(x.Id, attachment.imgBytes);
            
        }



        public void updateListElements(List<Post> posts)
        {
            Items.Clear();
            HashSet<int> unlockedPosts = App.user.unlockedSet;
            foreach (Post x in posts)
            {
                byte[] temp = App.user.readImage(x.getId());
                if (x.getCoordinate() != null && unlockedPosts.Contains(x.getId()) && temp != null)
                {
                    Items.Add(new PostListElement()
                    {
                        Id = x.getId(),
                        Subject = x.getSubject(),
                        Body = x.getBody(),
                        Created = x.getCreated(),
                        LastUpdated = x.getLastUpdated(),
                        Position = x.getCoordinate(),
                        Dist = "",
                        Img = ImageSource.FromStream(() => new MemoryStream(temp))
                    });
                }
            }
        }



    }


}
