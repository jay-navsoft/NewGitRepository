using GithubAccessTest.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections;
using System.Data;

namespace GithubAccessTest.Controllers
{
    public class GitCommitAccessController : Controller
    {
        // GET: GitCommitAccess
        private Dictionary<string, object> errordata, dictionaryObj;
        private static JavaScriptSerializer _jsObj = new JavaScriptSerializer();
        public ActionResult CreateGitUI()
        {
            return View();
        }
        public static dynamic Deserialize(string content, Type convertType = null)
        {
            if (content != null)
            {
                if (convertType != null)
                {
                    return _jsObj.Deserialize(content, convertType);
                }
                else
                {
                    return _jsObj.Deserialize(content, content.GetType());
                }
            }
            else
            {
                return "Data not available.";
            }
        }
        public object GetGitCommits(GitModel model)
        {
            Dictionary<string, string> dictionaryObj1 = new Dictionary<string, string>();
            GithubApi githubApi = new GithubApi();
            //ArrayList listdata = new ArrayList();
            List<string> listdata = new List<string>();
            string response = githubApi.GetGithubCommit(model);
            List<object> lc = JsonConvert.DeserializeObject<List<object>>(response);
            DataTable dt = Utility.ToDataTable<object>(lc);
            //string rs = response[0].ToString();
            //string json = JsonConvert.SerializeObject(response, Formatting.None);            
            //json = JsonConvert.SerializeObject(response, Formatting.Indented);
            //var table = JsonConvert.DeserializeAnonymousType(json, new { Makes = default(DataTable) }).Makes;
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            //listdata.Add(response);
            string message = string.Empty;
            foreach (var data in lc)
            {
                if(data!=null)
                {

                }
            }
            
            
            return response;
        }
        

      

    }
}