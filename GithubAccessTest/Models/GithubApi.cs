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

        public List<string> GetGithubCommit(GitModel model)
        {
            var client1 = new RestSharp.RestClient("https://docs.github.com/en/enterprise-server@3.1/rest/reference/repos?"+model.Username+"&"+model.Token+"&"+model.Giturl);
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
            List<string> data = new List<string>();
            data.Add(responsedata);
            return data;
        }

    }
}