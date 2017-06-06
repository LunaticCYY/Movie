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
        public int CommentId { get; set; }//评论编号
        [Required]
        public int UserId { get; set; }//评论用户编号
        [Required]
        public int VideoId { get; set; }//用户评论视频编号
        [Required]
        [StringLength(2000)]
        public string Content { get; set; }//评论内容
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string CommentTime { get; set; }//评论时间
    }
}