using System;
using System.Collections.Generic;

namespace coursework_itransition.Models
{
    public class Chapter
    {
        string ID{get;set;}
        
        string CompositionID{get;set;}
        
        DateTime CreationDT{get;set;}
        DateTime LastEditDT{get;set;}

        string Contents;

        List<string> Likes;
    }
}