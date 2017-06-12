using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class CommentDetail
    {
        public int VideoId { get; set; }
        public string Vname { get; set; }
        public string Content { get; set; }
        public string CommentTime { get; set; }
    }
}