﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Video
    {
        [Key]
        public int Vid { get; set; }
        [Required]
        [StringLength(30)]
        public string Vname { get; set; }
        [Required]
        public string Vurl { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        public int ViewedNum { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string UploadTime { get; set; }
        [Required]
        public string Vtype { get; set; }
        public int Uid { get; set; }
        [StringLength(200)]
        public string Vinfo { get; set; }
    }
    public class VideoDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SCOTT");
        }
        public DbSet<Video> Video { get; set; }
    }
}