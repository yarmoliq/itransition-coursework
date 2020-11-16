using System;

namespace coursework_itransition.Models
{
    public class Comment
    {
        public string ID{get; set;}

        public string AuthorID{get; set;}
        public string CompositionID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]

        public Composition Composition{get; set;}

        public DateTime CreationDT{get; set;}
        public DateTime LastEditDT{get;set;}

        public string Contents{get;set;}
    }
}
