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

        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Stargazers_count { get; set; }


        public static List<Project> GetProjects()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/" + EnvironmentVariables.User + "/repos", Method.GET);
            request.AddHeader("Accept", "application / vnd.github.v3 + json");
            request.AddHeader("User-Agent", EnvironmentVariables.User);


            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JArray returnArray = JsonConvert.DeserializeObject<JArray>(response.Content);

            Console.WriteLine(returnArray);

            string jsonOutput = returnArray.ToString();
            var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonOutput);
            Console.WriteLine(projectList[0].Name);
            return projectList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

    }
}
