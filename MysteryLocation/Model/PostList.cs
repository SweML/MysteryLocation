/*using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using MysteryLocation;
using MysteryLocation.Model;
using MysteryLocation.View;
using Xamarin.Forms;

namespace MysteryLocation.Model
{
    public class PostList
    {

        public int Id { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String Created { get; set; }
        public String LastUpdated { get; set; }
        public Coordinate Position { get; set; }

        public String Dist { get; set; }


        private List<Post> all = new List<Post>();

        private User user;
        
        private APIConnection conn = new APIConnection();

        public static Coordinate prevCoordinate = null;

        public CardViewTemp ctp { get; }

        private PostList()
        {
          
        }
       
        public PostList(User user, CardViewTemp ctp)
        {
            this.user = user;
            
            //Console.WriteLine(list.Count + " list contains");
           // PopulateUIWithPosts();
            this.ctp = ctp;
            user.pl = this;


        }

        public void PopulateUIWithPosts()
        {
            List<PostList> All = new List<PostList>();
            //Coordinate newCoordinate = new Coordinate(55.840281, 13.300698);
            //Coordinate newCoordinate = user.getCurrentPos();
            // newCoordinate = new Coordinate(55.840281, 13.300698);

            // Loop through the public static fields of the Color structure.

            // Convert the name to a friendly name.

            all = user.getFeed();
            Coordinate other;
            foreach(Post x in all)
            {
                Console.WriteLine("ABCDEF");
                other = x.getCoordinate();
                PostList Pl = new PostList
                {
                    Id = x.getId(),
                    Subject = x.getSubject(),
                    Body = x.getBody(),
                    Created = x.getCreated(),
                    LastUpdated = x.getLastUpdated(),
                    Position = x.getCoordinate(),
                    Dist = "Loading"

                };
               
                //Console.WriteLine("Throwing exception?");
                //Console.WriteLine(Pl.Position.getDistance(newCoordinate).ToString());
                All.Add(Pl);
                }
                //Console.WriteLine("Exception not thrown.");
            
            Console.WriteLine(all.Count + "ABCABCABC");
            Console.WriteLine(All.Count + "ETA");
            //  ListOfPosts = All;
            ELL = All;
            //ctp.updateView();
            Console.WriteLine(ELL.Count + " nbr of elements in ELL");
        }*/
      
        /**
         * This function is called when the distance between the user's new gps-coordinate 
         * and the previous calculation's coordinate is greater than 500 m
         */
    /*    public void ReCalculateDistance(User usr)
        {
            Coordinate current = usr.currentPos;
            double distance = 0;
            if (ELL.Count > 0)
            {
                prevCoordinate = current;
                
            }
            List<PostList> All = new List<PostList>();
            HashSet<PostList> a = new HashSet<PostList>();
            int i = 0;
            foreach (PostList x in ELL)
            {

                if(x.Position == null)
                {
                    a.Add(x);
                }

                distance = current.getDistance(x.Position);
                if(distance > 1000)
                {
                    distance /= 10;
                    x.Dist = distance.ToString() + " km";
                }
                else
                {
                    x.Dist = distance.ToString() + " m";
                }
                Console.WriteLine(x.Dist + "Testing x.Dist");
            }

            foreach(PostList x in a)
            {
                ELL.Remove(x);
            }
            Console.WriteLine("ReCalculated distance " + i);
            ELL = ELL;
        }

     
        public async void getPostList()
        {
            try
            {
                await conn.RefreshDataAsync();

            }
            catch (Exception e)
            {

            }
            all = conn.getCurrentPosts();

        }

        public static IList<PostList> ELL { set; get; }



    }
}
    */