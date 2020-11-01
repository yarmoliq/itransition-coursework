using System;

namespace coursework_itransition.Models
{
    public class Comment
    {
        string ID{get;set;}
        
        string AuthorID{get;set;}
        
        DateTime CreationDT{get;set;}
        DateTime LastEditDT{get;set;}
        
        string Contents{get;set;}
    }
}
