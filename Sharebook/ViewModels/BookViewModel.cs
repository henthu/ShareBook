using System.Collections.Generic;

namespace Sharebook.ViewModels
{
    public class BookViewModel
    {
        public string Id{get;set;}
        public string Name {get;set;}
        public string Author {get;set;}
        public string Genre{get;set;}
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}