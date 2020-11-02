using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Chapter
    {
        public string ID{get; private set;}

        public string CompositionID{get; private set;}

        public DateTime CreationDT{get; private set;}
        public DateTime LastEditDT{get;set;}

        public string Contents;

        public List<string> Likes;
    }
}