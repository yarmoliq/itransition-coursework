using Microsoft.AspNetCore.Identity;
using coursework_itransition.Models;
using System.Collections.Generic;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Compositions = new List<Composition>();   
        }
        
        public string Name { get; set; }

        public string Region { get; set;  }

        public ICollection<Composition> Compositions { get; set; }
    }
}