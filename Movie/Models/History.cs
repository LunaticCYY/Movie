using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class History
    {
        [Key]
        public int Hid { get; set; }
        [Required]
        public int Uid { get; set; }
        [Required]
        public int Vid { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string HistoryTime { get; set; }
    }
    public class HistoryDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCOTT");
        }
        public DbSet<History> History { get; set; }
    }
}