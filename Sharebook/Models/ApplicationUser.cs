using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Attributes;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sharebook.Models
{
    [Validator(typeof(ApplicationUserValidator))]
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
        public City City  { get; set; }
        public string Country { get; set; }
        public ICollection<Comment> Comments{get;set;}
        private ICollection<RecievedMessage> _recievedMessages;

        public virtual ICollection<RecievedMessage> RecievedMessages
        {
            get { return _recievedMessages ?? new List<RecievedMessage>(); }
            set { _recievedMessages = value; }
        }

        private ICollection<SentMessage> _sentMessages;

        public virtual ICollection<SentMessage> SentMessages
        {
            get { return _sentMessages ?? new List<SentMessage>(); }
            set { _sentMessages = value; }
        }


    }
}