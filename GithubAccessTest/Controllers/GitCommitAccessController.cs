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
            List<string>asciicode=new List<string>();
            AVL tree = new AVL();
            int ascii=0;
            foreach (GitResponse item in gitResponses)
            {
                item.username = model.Username;
                item.token = model.Token;
                item.reponame = model.Giturl;

                if (!string.IsNullOrEmpty(item.commit.message))
                {
                    messages.Add(item.commit.message);
                    var charArrayList = messages.Select(str => str.ToArray()).ToList();

                    for(int i=0;i<=charArrayList.Count;i++)
                    {
                         ascii = charArrayList[0][i];
                        tree.Add(ascii);
                    }
                    model.Avl = tree;
                    Node node = new Node(ascii);
                    AvlModel avlModel = new AvlModel();
                    //avlModel.right = node.right;
                    
                    //tree.DisplayTree();

                    //char character = char.Parse(message);
                    //foreach (char c in charArrayList)
                    //{
                    //    int unicode = Convert.ToChar(c.);
                    //    asciicode.Add(unicode);
                    //    //Console.WriteLine(unicode < 128 ? "ASCII: {0}" : "Non-ASCII: {0}", unicode);
                    //}

                    //foreach (string word in item.commit.message.Split(' ', ','))
                    //{
                    //    if (!words.Contains(word)) words.Add(word);
                    //}

                }
            }
            //ViewBag.AVLTree = tree.ri;



            //Using github api= 3654678
            //Including github api=56465479665
            //    Using
            //    github
            //    api
            //    Including
            //string resources = (string)JsonConvert.DeserializeObject(response);

            //JArray jsonArray = JArray.Parse(response);
            //dynamic data = JObject.Parse(Convert.ToString(jsonArray[0]));
            //var obj = JObject.Parse(response);

            //string msgs = data.SelectToken(Convert.ToString("sha")).Value<dynamic>();

            //var names = resources["tree"]
            //    .Select(item => (string)item["sha"])
            //    .ToList();

            //var jss = new JavaScriptSerializer();
            //dynamic table = jss.Deserialize<dynamic>(response);

            //for (int i = 0; i <= table.Length; i++)
            //{
            //    string m = table[i].ToString();
            //}
            //var itemStores = resources.Select(resource => resource
            //         {

            //});
            //DataTable datalist = JsonToDatatable(response, "ListTable");

            //List<dynamic> lc = JsonConvert.DeserializeObject<List<dynamic>>(response);
            //ArrayList list = new ArrayList();
            //list.Add(lc);
            // DataTable dt = Utility.ToDataTable<dynamic>(lc);
            //string rs = response[0].ToString();
            //string json = JsonConvert.SerializeObject(response, Formatting.None);            
            //json = JsonConvert.SerializeObject(response, Formatting.Indented);
            //var table = JsonConvert.DeserializeAnonymousType(json, new { Makes = default(DataTable) }).Makes;
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            //listdata.Add(response);
            string message = string.Empty;

            ////oreach (var data in list)
            //{

            //    if(data!=null)
            //    {
            //        for(int i=0;i<=list.Count;i++)
            //        {
            //            message = list[0].ToString();
            //            listdata.Add(message);
            //        }
            //    }
            //}
            return View(gitResponses);
        }
        public static DataTable JsonToDatatable(string content, string tablename = "")
        {
            dTable = new DataTable();
            dTable = tablename.Trim() == string.Empty ? new DataTable() : new DataTable(tablename);
            MainArr = new Dictionary<string, object>();
            ArrayList mainData;
            MainArr = (Dictionary<string, object>)_jsObj.Deserialize(content, (typeof(Dictionary<string, object>)));
            if (MainArr.ContainsKey("status"))
            {
                if (Convert.ToBoolean(MainArr["status"]))
                {
                    if (MainArr.ContainsKey("response"))
                    {
                        var objType = MainArr["response"].GetType();
                        if (objType.Name.ToLower() == "string")
                        {
                            //dTable.Columns.Add("status");
                            dTable.Columns.Add("response");
                            //object[] row = new object[] { MainArr["status"], MainArr["response"] };
                            object[] row = new object[] { MainArr["status"], MainArr["response"] };
                            dTable.Rows.Add(row);
                        }
                        else if (objType.Name.ToLower() == "arraylist")
                        {
                            mainData = new ArrayList();
                            mainData = (ArrayList)MainArr["response"];
                            foreach (Dictionary<string, object> data in mainData)
                            {
                                int count = data.Keys.Count;
                                if (count > 0)
                                {
                                    List<string> keys = data.Keys.ToList();
                                    List<object> values = data.Values.ToList();
                                    object[] array = new object[values.Count];
                                    if (dTable.Columns.Count == 0)
                                    {
                                        for (int i = 0; i < keys.Count; i++)
                                        {
                                            dTable.Columns.Add(keys[i]);
                                        }
                                        //dTable.Columns.Add("status");
                                    }

                                    for (int i = 0; i < values.Count; i++)
                                    {
                                        array[i] = values[i];
                                    }
                                    //array[values.Count] = MainArr["status"];
                                    dTable.Rows.Add(array);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("'Response' key does not contain in json format.");
                    }
                }
            }
            else
            {
                throw new Exception("'Status' key does not contain in json format.");
            }
            return dTable;
        }

        public ActionResult WordsCount(string sha,string message )
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in message.Split(' ', ','))
            {
                KeyValuePair<string, int> wordCountItem = wordCount.FirstOrDefault(i => i.Key.ToLower() == word.ToLower());
                if (wordCountItem.Value == 0) wordCount.Add(word, 1);
                else wordCount[word] = wordCountItem.Value + 1;
            }
            return View(wordCount);
        }




    }
}