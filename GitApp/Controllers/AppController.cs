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

namespace GitApp.Controllers
{
    public class AppController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly IGitRepository _repository;
        private readonly IMapper _mapper;

        public AppController(IHostingEnvironment hosting, IGitRepository repository, IMapper mapper)
        {
            _hosting = hosting;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            RepositoriesViewModel repoViewModel = new RepositoriesViewModel();
            repoViewModel.repositories = _repository.GetAllRepositories();

            return View(repoViewModel);
        }

        public IActionResult ViewDetails(int id)
        {
            RepositoryViewModel repoModel = new RepositoryViewModel();
            Repository newRecord = _repository.GetRepositoryByID(id);
       
            return View("RepoDetails", _mapper.Map<Repository, RepositoryViewModel>(newRecord));
        }
    }
}