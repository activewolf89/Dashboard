using System;


namespace Dashboard.Models
{
    public class Comment 
    {
        public int CommentId{get;set;}
        public string CommentConversation{get;set;}
        public DateTime Created_At{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int MessageId{get;set;}
        public Message Message{get;set;}
    }
}