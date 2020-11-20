
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MysteryLocation.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MysteryLocation
{
    public class APIConnection
    {
        private HttpClient client;
     
        private List<Post> currentPosts;
        

        public APIConnection()
        {
            client = new HttpClient();
          
        }

        public async void RefreshDataAsync() // Get 
        {
            Console.WriteLine("Trying to connect to API");
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            // ActivityIndicator activityIndicator = new ActivityIndicator { Color = Color.Orange };
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                Console.WriteLine("In if statement in RefreshDataAsync");
                currentPosts = ConvertJsonToPostsUser(content);
                //Console.WriteLine(posts);
                //UserHolder.user.populateLists(ConvertJsonToPostsUser(content));

            }


        }


        /** Method to delete an observation.
         *  Currently not supported by the API.
         *  The response returns an errorcode 405
         * */
        /*public async void deleteItem(int observationID)
        {
           // Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation/", observationID));
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(@"\tTodoItem successfully deleted.");
            }
            else
            {
                Console.WriteLine("Delete failed " + response);
            }
        }*/

        public async void CreateNewPost()
        {
            Console.WriteLine("Trying to connect to API");
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            createPost post = new createPost("myfirstpusheverandeverandever...", "very happy days", new Coordinate(100, 150));
            string json = JsonConvert.SerializeObject(post, Formatting.Indented);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
        }




        private String ConvertJsonToPosts(string JsonString) // This should return the whole list instead of a String. Todo
        {
            List<Post> posts;
            posts = JsonConvert.DeserializeObject<List<Post>>(JsonString); // Converts the Json to Posts
            String everything = "";
            foreach (Post p in posts)
            {
               // Console.WriteLine(p.toString());
               // everything += p.toString();
            }
            return everything;

        }

        private List<Post> ConvertJsonToPostsUser(string JsonString)
        {
            List<Post> posts;
            posts = JsonConvert.DeserializeObject<List<Post>>(JsonString);
            return posts;
        }

        /**
         * Use this method to populate the list variable in this class.
         */
        public async void GetPostsAsync() 
        {
            List<Post> posts = null;
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                posts = JsonConvert.DeserializeObject<List<Post>>(content);
                currentPosts = posts;
            }
           /* foreach (Post p in currentPosts)
            {
                if (!user.category.Equals(p.subject))
                {
                    currentPosts.Remove(p);
                }
            }*/ //Code to filter the list
        }

        public List<Post> getCurrentPosts()
        {
            return currentPosts;
        }

    }
}
