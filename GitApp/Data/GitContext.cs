using GitApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Data
{
    public class GitContext: DbContext
    {
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Owner> Owners { get; set; }

    }
}
