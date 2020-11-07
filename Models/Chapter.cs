using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Chapter
    {
        public string ID { get; set; }

        public string CompositionID { get; set; }

        public Composition Composition { get; set; }

        public DateTime CreationDT { get; set; }

        public DateTime LastEditDT { get; set; }

        public string Title;
        
        public string Contents;

        // public List<string> Likes;
    }
}