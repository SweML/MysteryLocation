using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MysteryLocation;
using MysteryLocation.Model;

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
        
        
        static PostList()
        {

        }

        private PostList()
        {
          
        }
       
        public PostList(List<Post> list, User user)
        {
            this.user = user;
            all = list;
            Console.WriteLine(list.Count + " list contains");
            method();
            
        }

        public void method()
        {
            List<PostList> All = new List<PostList>();
           // Coordinate newCoordinate = new Coordinate(55.840281, 13.300698);
            Coordinate newCoordinate = user.getCurrentPos();
            //newCoordinate = new Coordinate(55.840281, 13.300698);

            // Loop through the public static fields of the Color structure.

            // Convert the name to a friendly name.
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
                    Dist = newCoordinate.getDistance(other).ToString()

                };
                //Console.WriteLine("Throwing exception?");
                //Console.WriteLine(Pl.Position.getDistance(newCoordinate).ToString());
                    All.Add(Pl);
                //Console.WriteLine("Exception not thrown.");
            }
            Console.WriteLine(all.Count + "ABCABCABC");
            Console.WriteLine(All.Count + "ETA");
            ELL = All;
            Console.WriteLine(ELL.Count + " nbr of elements in ELL");
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
