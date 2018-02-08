using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitApp.Data.Entities;
using GitApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using GitApp.Data;
using AutoMapper;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace GitApp.Controllers
{
    public class AppController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly IGitRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppController> _logger;
        public AppController(IHostingEnvironment hosting, IGitRepository repository, IMapper mapper, ILogger<AppController> logger)
        {
            _hosting = hosting;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                RepositoriesViewModel repoViewModel = GetRepoViewModel("mhz17");
                if (repoViewModel != null)
                {
                    return View(repoViewModel);
                }
                else
                {
                    return View("Error");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting gitData from github: {ex.Message}");
            }

        }


        [HttpPost]
        public IActionResult Index(string username)
        {
            try
            {
                RepositoriesViewModel repoViewModel = GetRepoViewModel(username);
                if (repoViewModel  != null)
                {
                    return View(GetRepoViewModel(username));
                }
                else
                {
                    return View("Error");
                }
              
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting gitData from github: {ex.Message}");
            }
        }

        public IActionResult ViewDetails(int id, string username)
        {

            using (var client = new HttpClient())
            {
                Repository repo = new Repository();
                RepositoryViewModel repoViewModel = new RepositoryViewModel();
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
                var task = client.GetAsync($"https://api.github.com/users/{username}/repos")
                    .ContinueWith((taskwithresponse) =>
                    {
                       var response = taskwithresponse.Result.Content.ReadAsStringAsync();
                        response.Wait();
                        IEnumerable<Repository> gitData = JsonConvert.DeserializeObject<IEnumerable<Repository>>(response.Result);
                        repo = gitData.Where(i => i.id == id).First();
                    });

                task.Wait();

                return View("RepoDetails", _mapper.Map<Repository, RepositoryViewModel>(repo));

            }

        }

        public RepositoriesViewModel GetRepoViewModel(string username)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    RepositoriesViewModel repoViewModel = new RepositoriesViewModel();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://developer.github.com/v3/#user-agent-required");
                    var task = client.GetAsync($"https://api.github.com/users/{username}/repos")
                        .ContinueWith((taskwithresponse) =>
                        {
                            var response = taskwithresponse.Result.Content.ReadAsStringAsync();
                            response.Wait();
                            IEnumerable<Repository> gitData = JsonConvert.DeserializeObject<IEnumerable<Repository>>(response.Result);
                            repoViewModel.repositories = gitData;
                            repoViewModel.UserName = username;
                        });

                    task.Wait();
                    return repoViewModel;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error: " + ex);
                    return null;
                }

            }
        }
     
    }
}