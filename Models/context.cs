using Microsoft.EntityFrameworkCore;

namespace Dashboard.Models 
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options):base(options){}
        public DbSet<User> User{get;set;}
        public DbSet<Message> Message{get;set;}
        public DbSet<Comment> Comment{get;set;}
    }
}