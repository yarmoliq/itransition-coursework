using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Composition
    {
        Composition()
        {
            EditTime = DateTime.Now;
        }

        string ID{get;set;}
        
        string UserID{get;set;}
        
        DateTime EditTime{get;set;}
        
        List<string> Chapters;
        
        List<Tuple<string,short>> IDAndRating = new List<Tuple<string, short>>();

        public double averageRating()
        {
            double average = 0;
            IDAndRating.ForEach(w=>{average += w.Item2;});
            return average/IDAndRating.Count;
        }
    }
}