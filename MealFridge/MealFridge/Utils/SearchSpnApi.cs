using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using MealFridge.Models;

namespace MealFridge.Utils
{
    public class SearchSpnApi
    {
        public string Source { get; set; }
        private string Secret { get; set; }

        public SearchSpnApi(string endpoint, string key)
        {
            Source = endpoint;
            Secret = key;
        }
        public List<SpoonacularRecipe> SearchAPI(string query)
        {
            var jsonResponse = SendRequest(Source, Secret, query);
            var recipes = JObject.Parse(jsonResponse);
            if ((int)recipes["number"] == 0)
                return null;

            var output = new List<SpoonacularRecipe>();
            foreach (var recipe in recipes["results"])
                output.Add(new SpoonacularRecipe
                {
                    Id = (int)recipe["id"],
                    Title = (string)recipe["title"],
                    ImageURL = (string)recipe["image"],
                    ImageType = (string)recipe["imageType"]
                });
            return output;
        }
        private static string SendRequest(string url, string credentials, string query)
        {
            //Number selects the number of results to return from API (FOR FUTURE REFERENCE)
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?apiKey=" + credentials + "&query=" + query + "&number=10");
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
