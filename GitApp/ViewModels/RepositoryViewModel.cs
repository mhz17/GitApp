using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.ViewModels
{
    public class RepositoryViewModel
    {
        [Required]
        public int RepositoryID { get; set; }
        [Required]
        public string RepositoryName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
    }
}
