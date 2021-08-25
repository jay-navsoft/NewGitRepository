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
        public ActionResult GetGitCommits(GitModel model)
        {
            Dictionary<string, string> dictionaryObj1 = new Dictionary<string, string>();
            GithubApi githubApi = new GithubApi();
            //ArrayList listdata = new ArrayList();
            List<string> listdata = new List<string>();
            string response = githubApi.GetGithubCommit(model);
            List<GitResponse> gitResponses = JsonConvert.DeserializeObject<List<GitResponse>>(response);
            return View(gitResponses);
        }
        public ActionResult WordsCount(string sha, string message)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            wordCount = Utility.WordCount(sha, message);
            var count_query =
            from string word in message.Split(' ')
            orderby word.ToCharArray().Distinct().Count()
            select word.ToCharArray().Distinct().Count() + ", " + word;
            ViewBag.Message = message;
            return View(wordCount);
        }
        public void DownloadCsv(string sha, string message)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            wordCount = Utility.WordCount(sha, message);
            foreach (string word in message.Split(' ', ','))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Commit Word", typeof(string));
                dt.Columns.Add("Word Count", typeof(string));

                foreach (KeyValuePair<string, int> dict in wordCount)
                {
                    dt.Rows.Add(dict.Key, dict.Value);
                }
                string filename = @"E:\csv\data.csv";
                dt.ToCSV(filename);
            }

        }

        public void AvlTreeSort(string sha, string message)
        {

            List<string> messages = new List<string>();
            List<string> words = new List<string>();
            List<string> asciicode = new List<string>();

            //Values acquired by AVL tree but not in use
            AVL tree = new AVL();
            //AVLTree tree = new AVLTree();
            int ascii = 0;
            messages.Add(message);
            var charArrayList = messages.Select(str => str.ToArray()).ToList();
            
                for (int i = 0; i <= charArrayList.Count; i++)
                {
                    ascii = charArrayList[0][i];// This will access all the ASCII code within their character 

                    tree.Add(ascii); //This will add the ASCII code into AVL tree 
                    //tree.root = tree.insert(tree.root, ascii);
                }

        }
    }
}
