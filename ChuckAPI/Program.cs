using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nancy.Json;

namespace ChuckAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowCategories();
            Console.WriteLine("Pick a category");
            string userCategory = Console.ReadLine();
            ShowJokeFromGivenCategory(userCategory);

            Console.ReadLine();
        }

        public static void ShowCategories()
        {
            string categoryUrl = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                //Console.WriteLine(response);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var categories = ser.Deserialize<List<string>>(response);

                foreach (string category in categories)
                {
                    Console.WriteLine(category);
                }
            }
        }

        public static void ShowRandomJoke()
        {
            string randomJokeUrl = "https://api.chucknorris.io/jokes/random";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(randomJokeUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }

        public static void ShowJokeFromGivenCategory(string category)
        {
            string userCategoryUrl = "https://api.chucknorris.io/jokes/random?category=" + category;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userCategoryUrl);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);
            }
        }
    }
}
