using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class User
    {
        [Key]
        public int Uid { get; set; }
        [Required]
        [StringLength(16,MinimumLength = 3)]
        public string NickName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }
        public int Privilege { get; set; }
    }
    public class OracleDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCOTT");
        }
        public DbSet<User> User { get; set; }
        //public DbSet<Video> Video { get; set; }
        //public DbSet<History> History { get; set; }
        //public DbSet<Comment> Comment { get; set; }
    }
}