using System;

namespace coursework_itransition.Models
{
    public class Chapter
    {
        public string ID { get; set; }

        public string CompositionID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Composition Composition { get; set; }

        public DateTime CreationDT { get; set; }

        public DateTime LastEditDT { get; set; }

        public string Title { get; set; }
        
        public string Contents { get; set; }

        public int Order { get; set; }

        // public List<string> Likes;
    }
}