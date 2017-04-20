using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class Message 
    {
        public int MessageId{get;set;}
        public string MessageConversation{get;set;}
        public DateTime Created_At{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int MessageReceiverId{get;set;}
        public User MessageReceiver{get;set;}
        
    }
}