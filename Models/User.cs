using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Models
{
    public class User
    {
            public User()
        {
            Messages = new List<Message>();
            Received_Messages = new List<Message>();
            
        }

        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public int App_Level {get;set;}
        public string Description {get;set;}
        public DateTime Created_At {get;set;}
        public List<Message> Messages{get;set;}
        
        [InverseProperty("MessageReceiver")]
        public List<Message> Received_Messages{get;set;}


    }

}