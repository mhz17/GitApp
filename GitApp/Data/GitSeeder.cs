using GitApp.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Data
{
    public class GitSeeder
    {
        private readonly GitContext _ctx;
        private readonly IHostingEnvironment _hosting;

        public GitSeeder(GitContext ctx,
            IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;

        }

        public void Seed()
        {

            if (!_ctx.Repositories.Any())
            {

                //  Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/git.json");
                var json = File.ReadAllText(filepath);
                var repos = JsonConvert.DeserializeObject<IEnumerable<Repository>>(json);

                _ctx.Repositories.AddRange(repos);

                var repository = new Repository()
                {
                    id = repos.First().id,
                    name = repos.First().name,
                    created_at = repos.First().created_at,
                    url = repos.First().url
                };

                _ctx.Repositories.Add(repository);

                _ctx.SaveChanges();

            }

        }
    }
}
