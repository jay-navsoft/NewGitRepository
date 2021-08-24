using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubAccessTest.Models
{
    public class GitModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Giturl { get; set; }
        public dynamic Avl{ get; set; }

    }
    public class AvlModel
    {
        public AvlModel  right { get; set; }
        public AvlModel left { get; set; }
        public AvlModel root { get; set; }

    }

    public class GitResponse
    {
        public string sha { get; set; }

        public string username { get; set; }
        public string token { get; set; }
        public string reponame { get; set; }
        public GitCommit commit { get; set; }        
        public class GitCommit
        {
            public string message { get; set; }            
            public GitCommitDate committer { get; set; }
        }
        public class GitCommitDate
        {
            public string date { get; set; }
        }
    }
}