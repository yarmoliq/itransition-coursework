using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Composition
    {
        Composition()
        {
            CreationDT = DateTime.UtcNow;
            LastEditDT = DateTime.Now;
        }

        public string ID{get; private set;}
        
        public string AuthorID{ get; private set; }

        public DateTime CreationDT{get; private set;}
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