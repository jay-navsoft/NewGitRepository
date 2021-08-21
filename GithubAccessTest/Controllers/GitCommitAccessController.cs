using GithubAccessTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GithubAccessTest.Controllers
{
    public class GitCommitAccessController : Controller
    {
        // GET: GitCommitAccess

        public ActionResult CreateGitUI()
        {
            return View();
        }
        public List<string> GetGitCommits(GitModel model)
        {
            GithubApi githubApi = new GithubApi();
            List<string> response = githubApi.GetGithubCommit(model);
            return response;
        }
    }
}