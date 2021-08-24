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
using Newtonsoft.Json.Linq;
using static GithubAccessTest.AVL;

namespace GithubAccessTest.Controllers
{
    public class GitCommitAccessController : Controller
    {
        // GET: GitCommitAccess
        private Dictionary<string, object> errordata, dictionaryObj;
        private static Dictionary<string, object> MainArr;
        private static JavaScriptSerializer _jsObj = new JavaScriptSerializer();
        private static DataTable dTable;
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
        public ActionResult GetGitCommits(GitModel model)
        {
            Dictionary<string, string> dictionaryObj1 = new Dictionary<string, string>();
            GithubApi githubApi = new GithubApi();
            //ArrayList listdata = new ArrayList();
            List<string> listdata = new List<string>();
            string response = githubApi.GetGithubCommit(model);
            List<GitResponse> gitResponses = JsonConvert.DeserializeObject<List<GitResponse>>(response);

            List<string> messages = new List<string>();
            List<string> words = new List<string>();
            List<string> asciicode = new List<string>();
            AVL tree = new AVL();
            int ascii = 0;
            foreach (GitResponse item in gitResponses)
            {
                item.username = model.Username;
                item.token = model.Token;
                item.reponame = model.Giturl;

                if (!string.IsNullOrEmpty(item.commit.message))
                {
                    messages.Add(item.commit.message);
                    var charArrayList = messages.Select(str => str.ToArray()).ToList();

                    for (int i = 0; i <= charArrayList.Count; i++)
                    {
                        ascii = charArrayList[0][i];
                        tree.Add(ascii);
                    }
                    model.Avl = tree;
                    Node node = new Node(ascii);
                    AvlModel avlModel = new AvlModel();
                    
                }
            }
            
            string message = string.Empty;

           
            return View(gitResponses);
        }
        public ActionResult WordsCount(string sha, string message)
        {
            
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in message.Split(' ', ','))
            {
                KeyValuePair<string, int> wordCountItem = wordCount.FirstOrDefault(i => i.Key.ToLower() == word.ToLower());
                if (wordCountItem.Value == 0) wordCount.Add(word.ToLower(), 1);
                else wordCount[word.ToLower()] = wordCountItem.Value + 1;
                ViewBag.Message = message;            
                
                string path = @"E:\csv\data.csv";
                String csv = String.Join(
                   Environment.NewLine,
                 wordCount.Select(d => $"{d.Key} {d.Value}")
);
                
                System.IO.File.WriteAllText(path, csv);
            }
            
            return View(wordCount);
        }




    }
}