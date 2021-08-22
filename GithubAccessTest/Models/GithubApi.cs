using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace GithubAccessTest.Models
{
    public class GithubApi
    {

        public string GetGithubCommit(GitModel model)
        {
            //https://api.github.com/repos/jay-navsoft/NewGitRepository/git/commits/2592568d6c21459eeeb670b666a2a2e3fc549a79
            var client1 = new RestSharp.RestClient("https://api.github.com/repos"+"/"+ model.Username +"/"+ model.Giturl +"/"+"commits");
            var request1 = new RestRequest(Method.GET);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            request1.RequestFormat = DataFormat.Json;
            //request1.AddHeader("Content-Type", "application/json");
            request1.AddHeader("Content-Type","application/vnd.github.v3+json");
            //request1.AddParameter("authorization", "Bearer " + model.Token, ParameterType.HttpHeader);
            //request1.AddHeader("content-type", "application/json");
            //request1.AddParameter("application/json", jsondata, ParameterType.RequestBody);
            IRestResponse response = client1.Execute(request1);
            string responsedata = response.Content.ToString();           
            return responsedata;  
        }

    }
}