using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;

namespace MealFridge.Models
{
    public class SpoonacularAPI
    {
        public string Source { get; set; }
        private string Secret { get; set; }

        public SpoonacularAPI(string endpoint, string key)
        {
            Source = endpoint;
            Secret = key;
        }
        public IEnumerable<SpoonacularRecipe> SearchAPI(string query) 
        {
            string jsonResponse = SendRequest(Source, Secret, query);
            Debug.WriteLine(jsonResponse);

            JObject recipe = JObject.Parse(jsonResponse);
            int count = 0;
            if((int)recipe["totalResults"] >= (int)recipe["number"])
            {
                count = (int)recipe["number"];
            }
            else
            {
                count = (int)recipe["totalResults"];
            }
            if (count == 0)
            {
                return null;
            }
            List<SpoonacularRecipe> output = new List<SpoonacularRecipe>();
            for (int i = 0; i < count; i++)
            {
                int id = (int)recipe["results"][i]["id"];
                string title = (string)recipe["results"][i]["title"];
                string imageURL = (string)recipe["results"][i]["image"];
                string imageType = (string)recipe["results"][i]["imageType"];
                output.Add( new SpoonacularRecipe { ID = id, Title = title, ImageURL = imageURL, ImageType = imageType });
            }
            return output;
        }
        private static string SendRequest(string url, string credentials, string query)
        {
            //Number selects the number of results to return from API (FOR FUTURE REFERENCE)
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url +"?apiKey="+credentials + "&query=" + query + "&number=5");
            request.Accept = "application/json";
            string jsonString = null;
            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }
    }
}
