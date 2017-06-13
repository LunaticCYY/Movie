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
        [Display(Name = "评论编号")]
        public int CommentId { get; set; }//评论编号
        [Required]
        [Display(Name = "用户编号")]
        public int UserId { get; set; }//评论用户编号
        [Required]
        [Display(Name = "视频编号")]
        public int VideoId { get; set; }//用户评论视频编号
        [Required]
        [Display(Name = "评论内容")]
        [StringLength(2000)]
        public string Content { get; set; }//评论内容
        [DataType(DataType.Date)]
        [Display(Name = "评论时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string CommentTime { get; set; }//评论时间
    }
}