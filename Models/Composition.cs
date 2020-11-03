using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Composition
    {
        public Composition()
        {
        }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ID{get; set;}
        
        public string AuthorID{ get; set; }

        public DateTime CreationDT{get; set;}
        public DateTime LastEditDT{get;set;}

        public List<string> Chapters;

        public List<Tuple<string,short>> IDAndRating = new List<Tuple<string, short>>();

        public string Genre{get; set;}

        public double averageRating()
        {
            double average = 0;
            IDAndRating.ForEach(w=>{average += w.Item2;});
            return average/IDAndRating.Count;
        }
    }
}