using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class VideoDetail
    {
        public int VideoId { get; set; }
        public string Vname { get; set; }
        public int ViewedNum { get; set; }
        public int CommentNum { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string CommentTime { get; set; }
    }
}