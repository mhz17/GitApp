using GitApp.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.ViewModels
{
    public class RepositoriesViewModel
    {
       public IEnumerable<Repository> repositories { get; set; }
       public IEnumerable<Owner> Owner { get; set; }
       public string UserName { get; set; }
    }
}
