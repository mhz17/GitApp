using GitApp.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Data
{
    public class GitRepository : IGitRepository
    {
        GitContext _ctx;
        ILogger<GitRepository> _logger;
        IHostingEnvironment _hosting;

        public GitRepository(GitContext ctx, 
            ILogger<GitRepository> logger,
            IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
            _logger = logger;
        }

        public IEnumerable<Repository> GetAllRepositories()
        {
            try
            {
                _logger.LogInformation("GetAllRepositories was called");

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/git.json");
                var json = System.IO.File.ReadAllText(filepath);
                IEnumerable<Repository> gitRepo = JsonConvert.DeserializeObject<IEnumerable<Repository>>(json);
                return gitRepo.OrderByDescending(p => p.pushed_at);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all repositories: {ex}");
                return null;
            }
        }

        public Repository GetRepositoryByID(int id)
        {
            try
            {
                _logger.LogInformation("GetRepositoryByID was called");

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/git.json");
                var json = System.IO.File.ReadAllText(filepath);
                IEnumerable<Repository> gitRepo = JsonConvert.DeserializeObject<IEnumerable<Repository>>(json);

                return gitRepo.Where(i => i.id == id).First();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Repository by ID: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
