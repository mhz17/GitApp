using System.Collections.Generic;
using GitApp.Data.Entities;

namespace GitApp.Data
{
    public interface IGitRepository
    {
        IEnumerable<Repository> GetAllRepositories();
        Repository GetRepositoryByID(int id);
        bool SaveAll();
    }
}