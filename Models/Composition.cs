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

        string ID{get;set;}
        
        string AuthorID{get;set;}
        
        DateTime CreationDT{get;set;}
        DateTime LastEditDT{get;set;}
        
        List<string> Chapters;
        
        List<Tuple<string,short>> IDAndRating = new List<Tuple<string, short>>();

        string Genre{get; set;}

        public double averageRating()
        {
            double average = 0;
            IDAndRating.ForEach(w=>{average += w.Item2;});
            return average/IDAndRating.Count;
        }
    }
}