using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Total
    {
        //public int UserId { get; set; }
        //public string NickName { get; set; }
        //public string Password { get; set; }
        public string RePassword { get; set; }
        public int CommentNum { get; set; }
        //public string Email { get; set; }
        //public int Privilege { get; set; }
        //public int VideoId { get; set; }
        //public string Vname { get; set; }
        //public string Vurl { get; set; }
        //public string Thumbnail { get; set; }
        //public int ViewedNum { get; set; }
        //public string UploadTime { get; set; }
        //public string Vtype { get; set; }
        //public string Vinfo { get; set; }
        //public int CommentId { get; set; }
        //public string Content { get; set; }
        //public string CommentTime { get; set; }
        //public int HistoryId { get; set; }
        //public string HistoryTime { get; set; }
        //public int FavoriteId { get; set; }
        //public string FavoriteTime { get; set; }
        public User user { get; set; }
        public Video video { get; set; }
        public Comment comment { get; set; }
        public History history { get; set; }
        public Favorite favorite { get; set; }
    }
}