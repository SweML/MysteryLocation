using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    public class UnlockedViewModel : PostListProperty
    {
        public ObservableCollection<PostListElement> items = new ObservableCollection<PostListElement>();
        private User user;
        public ObservableCollection<PostListElement> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public UnlockedViewModel(User user)
        {
            this.user = user;
            Items.Add(new PostListElement() {
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
            });
        }

        public void updateListElements()
        {
            List<Post> posts = user.getUnlocked();
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

    }


}
