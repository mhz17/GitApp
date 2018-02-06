using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitApp.Data.Entities;
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
            ViewBag.Repo = gitRepo;

            return View();
        }

        public IActionResult Repose()
        {
            return View();
        }

    }
}