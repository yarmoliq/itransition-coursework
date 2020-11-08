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

        public ApplicationUser Author { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ID { get; set; }

        public DateTime CreationDT { get; set; }

        public DateTime LastEditDT { get; set; }

        // public List<Tuple<string,short>> IDAndRating = new List<Tuple<string, short>>();

        public string Genre { get; set; }

        // public double averageRating()
        // {
        //     double average = 0;
        //     IDAndRating.ForEach(w=>{average += w.Item2;});
        //     return average/IDAndRating.Count;
        // }

        public ICollection<Chapter> Chapters { get; set; }
    }
}