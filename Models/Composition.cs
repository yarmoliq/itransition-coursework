using System;
using System.Collections.Generic;
using Identity.Models;

namespace coursework_itransition.Models
{
    public class Composition
    {
        public Composition()
        {
            Chapters = new List<Chapter>();
        }
        
        public string AuthorID{ get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ApplicationUser Author { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ID { get; set; }

        public DateTime CreationDT { get; set; }

        public DateTime LastEditDT { get; set; }

        public string Genre { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
    }
}