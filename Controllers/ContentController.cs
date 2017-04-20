using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Controllers
{
    public class ContentController : Controller
    {
    private DashboardContext _context;
 
    public ContentController(DashboardContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("users/show/{Uid}")]
        public IActionResult show(int Uid)
        {
            User aUser = _context.User.SingleOrDefault(user => user.UserId == Uid);

            List<Message>listOfMessages = _context.Message
            .Include(user => user.User)
            
            .ToList();
            List<Comment>listOfComments = _context.Comment 
            .Include(message => message.Message)
            .ToList();
            ViewBag.myCommentsList = listOfComments;
            ViewBag.myMessageList = listOfMessages;
            ViewData["error"] = TempData["error"];
            ViewBag.aUser = aUser;
            ViewData["commentError"] =  TempData["commentError"];
            return View();
        }
        [HttpPost]
        [Route("message/{who_received_message_id}")]

        public IActionResult message(int who_received_message_id, string myMessage)
        {
            int who_wrote_message_id = (int)HttpContext.Session.GetInt32("user_id");
            if(myMessage.Length < 1)
            {
                TempData["error"] = "Message has to contain a value";
                return RedirectToAction("show", new{Uid = who_received_message_id});
            } 
            else 
            {
                Message messageObject = new Message{
                    MessageConversation = myMessage,
                    Created_At = DateTime.Now,
                    UserId = who_wrote_message_id,
                    MessageReceiverId = who_received_message_id
                };
                _context.Add(messageObject);
                _context.SaveChanges();
            
            
                return RedirectToAction("show", new{Uid = who_received_message_id});
            }
        }
        [HttpPost] 
        [Route("comment/{message_id}")]

        public IActionResult comment(int message_id, string comment_area, int MessageWallUserId)
        {
            if(comment_area.Length > 1)
            {
                int who_wrote_comment_id = (int)HttpContext.Session.GetInt32("user_id");
                Comment myCommentOnMessage = new Comment
                {
                    CommentConversation = comment_area,
                    Created_At = DateTime.Now,
                    UserId = who_wrote_comment_id,
                    MessageId = message_id 
                };
                _context.Add(myCommentOnMessage);
                _context.SaveChanges();
                return RedirectToAction("show", new{Uid = MessageWallUserId});
            }
            else 
            {
                TempData["commentError"] = "Length of TextArea has to contain a value";
                return RedirectToAction("show", new{Uid = MessageWallUserId});
            }

        }
    }
}