
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public APIConnection()
        {
            client = new HttpClient();    
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

        public async Task<int> testPublishPosts(createPost x)
        {
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
              if (response.IsSuccessStatusCode)
              {      
                string temp = response.Headers.Location.ToString().Substring(54);
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

        public async Task<UnlockedPosts> getPostAttachmentAsync(int obsId)
        {
            Stopwatch stop = new Stopwatch();
            Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation/" + obsId + "/attachment", string.Empty));
            HttpResponseMessage response = await client.GetAsync(uri);
            UnlockedPosts temp = null;
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                temp = MakeUnlockedPost(content);
                temp.obsID = obsId;
            }
            return temp;
        }

        private UnlockedPosts MakeUnlockedPost(string content)
        {
            string id = "";
            int counter = 7;
            while (content[counter] != ',')
            {
                id += content[counter];
                counter++;
            }
            counter = 23 + id.Length;
            content = content.Substring(counter);
            content = content.Remove(content.Length - 3, 3);
            UnlockedPosts temp = new UnlockedPosts(int.Parse(id), content);
            return temp;
        }

      

        /*
        private List<Post> ConvertJsonToPostsUser(string JsonString)
        {
            List<Post> posts;
            posts = JsonConvert.DeserializeObject<List<Post>>(JsonString);
            return posts;
        }
        */



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

        /*    public async Task RefreshDataAsync() // Get 
            {
                Uri uri = new Uri(string.Format("https://saabstudent2020.azurewebsites.net/observation", string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    currentPosts = ConvertJsonToPostsUser(content);
                }
            }
        */


    }
}
