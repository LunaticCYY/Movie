using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Comment
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int VideoId { get; set; }
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string CommentTime { get; set; }
    }
}