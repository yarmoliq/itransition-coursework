using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Region { get; set;  }

        // public List<uint> Compositions { get; set; }
    }
}