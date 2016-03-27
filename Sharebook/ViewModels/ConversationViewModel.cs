using System;

namespace Sharebook.ViewModels
{
    public class ConversationViewModel
    {
        public string Correspondant { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool isRead { get; set; }
    }
}