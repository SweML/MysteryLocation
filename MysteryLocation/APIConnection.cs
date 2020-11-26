
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            currentPosts = new List<Post>();
          
        }

        public async Task RefreshDataAsync() // Get 
        {
            Console.WriteLine("Trying to connect to API");
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            Console.WriteLine("Trying to connect to API2");
            // ActivityIndicator activityIndicator = new ActivityIndicator { Color = Color.Orange };
            HttpResponseMessage response = await client.GetAsync(uri);
            Console.WriteLine("Trying to connect to API3");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Trying to connect to API4");
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Trying to connect to API5");
                Console.WriteLine(content);
                Console.WriteLine("In if statement in RefreshDataAsync");
                currentPosts = ConvertJsonToPostsUser(content);
                //Console.WriteLine(posts);
                //UserHolder.user.populateLists(ConvertJsonToPostsUser(content));

            }
        }

        public async Task<List<Post>> getDataAsync()
        {
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            List<Post> posts = new List<Post>();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                posts = JsonConvert.DeserializeObject<List<Post>>(content);
            }
            return posts;
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

       
        public async Task<int> testPublishPosts(createPost x)
        {
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(content + "Test2");
            Console.WriteLine(json + "Test2");
              HttpResponseMessage response = await client.PostAsync(uri, content);
              if (response.IsSuccessStatusCode)
              {
                  Console.WriteLine("Test2");
                  Console.WriteLine(response.ReasonPhrase + " response.ReasonPhrase");
                Console.WriteLine(response.Headers.Location + "response.Headers.ToString()");
                Console.WriteLine(response.Headers + "response.Headers");
                Console.WriteLine(response.Content.ReadAsStringAsync() + "response.Content");
                  Console.WriteLine(response.StatusCode.ToString() + "response.StatusCode");
                string temp = response.Headers.Location.ToString().Substring(54);
                Console.WriteLine(temp + " is it correct?");
                return Int32.Parse(temp);
              }
            return -1;
        }

        public async void publishAttachment(PostAttachment x)
        {
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation/" + x.obsID + "/attachment", string.Empty));
            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Hurra!");
            }
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
                Console.Write(content);

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
