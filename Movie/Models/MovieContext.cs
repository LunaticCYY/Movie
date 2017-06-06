using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Movie.Models
{
    public class MovieContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public MovieContext() : base("name=UserDbContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCOTT");
            modelBuilder.Entity<User>().Property(t => t.UserId) .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Video>().Property(t => t.VideoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<History>().Property(t => t.HistoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Comment>().Property(t => t.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }

        public System.Data.Entity.DbSet<Movie.Models.User> Users { get; set; }//Users表

        public System.Data.Entity.DbSet<Movie.Models.Video> Videos { get; set; }//Videos表

        public System.Data.Entity.DbSet<Movie.Models.History> Histories { get; set; }//Histories表

        public System.Data.Entity.DbSet<Movie.Models.Comment> Comments { get; set; }//Comments表

     
    }
}
