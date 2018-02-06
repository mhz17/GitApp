using System.Collections.Generic;
using GitApp.Data.Entities;

namespace GitApp.Data
{
    public interface IGitRepository
    {
        IEnumerable<Repository> GetAllRepositories();
        bool SaveAll();
    }
}