using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    [Table("Projects")]
        public class Project
    {
        //id
        //stargazers_count
        //description
        //name
        //full_name

        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Stargazers_count { get; set; }


        //public static List<Project> GetProjects()
        //{
        //    //GET / users /:username / repos
        //    //    / https://api.github.com/users/keelyzglenn/repos

        //    //stargazers_count": 0
        //}

        public static List<Project> GetProjects()
        {
            var client = new RestClient("https://api.github.com/users/keelyzglenn/repos");
            var request = new RestRequest("", Method.GET);
            //request.AddParameter("term", business);
            //request.AddParameter("location", zipcode);
            //request.AddParameter("Authorization", "Bearer -Da4abtiOYeUJlX7M5lM58akmknszNhKyaVXWBGIS3-798urLmCGsonAX-ong3puatOKeHIr4K9YauuC6UhsQGekZ_1JmbZs96wOR_t0TGTaQxLUoTXA1oYbRYwUWXYx", ParameterType.HttpHeader);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            Console.WriteLine(response);
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            string jsonOutput = jsonResponse[""].ToString();
            Console.WriteLine(jsonOutput);
            var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonOutput);
            Console.WriteLine(projectList[0].Name);
            return projectList;

            //return jsonResponse.GetValue("businesses").ToString();
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }
}
