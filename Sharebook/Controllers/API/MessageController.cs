using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using AutoMapper;
using Sharebook.Models;
using Sharebook.ViewModels;
using System.Collections.Generic;

namespace Sharebook.Controllers.API
{
    [Authorize]
    [Route("/api/Messages")]
    public class MessageController : Controller
    {
        private ISharebookRepository _repository;

        public MessageController(ISharebookRepository repository){
            _repository = repository;
        }
        
        [HttpGet("{recieverName}")]
        public JsonResult GetConversation(string recieverName)
        {
            ApplicationUser currentUser = _repository.GetUserByName(User.Identity.Name);
            var conversation = currentUser.SentMessages.Where(m=>m.Reciever.UserName == recieverName)
                            ?.Concat(currentUser.RecievedMessages.Where(m=>m.Sender.UserName == recieverName))
                            ?.OrderByDescending(m=>m.SendDate)
                            ?.ToList();
            foreach (var message in currentUser.RecievedMessages.Where(m => m.Sender.UserName == recieverName))
            {
                message.isRead =true;
            }
            _repository.SaveAll();
            
            return Json(Mapper.Map<IEnumerable<MessageViewModel>>(conversation));
        }
        
        [HttpGet("{unread}")]
        public JsonResult getCountUnread()
        {
            ApplicationUser currentUser = _repository.GetUserByName(User.Identity.Name);
            var count = currentUser.RecievedMessages.Where(m=>m.isRead == false)?.Count();
            
            return Json(count);
        }
        [HttpPost("{recieverName}")]
        public JsonResult SendMessage([FromBody]MessageViewModel message){
            var currentUser =_repository.GetUserByName(User.Identity.Name);
            Message newMessage = Mapper.Map<Message>(message);
            newMessage.Sender = currentUser;
            newMessage.Reciever = _repository.GetUserByName(message.RecieverUserName);
            newMessage.SendDate = DateTime.Now;
            
            _repository.getSentMessages(currentUser).Add(newMessage);
            
            if(_repository.SaveAll()){
                return Json(newMessage);
            }
            
            return Json(null);
        }
    }
}