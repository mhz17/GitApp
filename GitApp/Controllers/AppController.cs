using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitApp.Data.Entities;
using GitApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GitApp.Controllers
{
    public class AppController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        public AppController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        public IActionResult Index()
        {
            var filepath = Path.Combine(_hosting.ContentRootPath, "Data/git.json");
            var json = System.IO.File.ReadAllText(filepath);
            IEnumerable<Repository> gitRepo = JsonConvert.DeserializeObject<IEnumerable<Repository>>(json);
            RepositoriesViewModel repoViewModel = new RepositoriesViewModel();
            repoViewModel.repositories = gitRepo;

            return View(repoViewModel);
        }

        public IActionResult Repose()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return View("RepoDetails");
        }

        public IActionResult RepoDetails()
        {
            return View();
        }

    }
}