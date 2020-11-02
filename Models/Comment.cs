using System;

namespace coursework_itransition.Models
{
    public class Comment
    {
        public string ID{get; private set;}

        public string AuthorID{get; private set;}

        public DateTime CreationDT{get; private set;}
        public DateTime LastEditDT{get;set;}

        public string Contents{get;set;}
    }
}
