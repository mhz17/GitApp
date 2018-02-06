using GitApp.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Data
{
    public class GitRepository : IGitRepository
    {
        GitContext _ctx;
        ILogger<GitRepository> _logger;

        public GitRepository(GitContext ctx, ILogger<GitRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Repository> GetAllRepositories()
        {
            try
            {
                _logger.LogInformation("GetAllRepositories was called");

                return _ctx.Repositories
                    .OrderBy(p => p.name)
                    .ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all repositories: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
