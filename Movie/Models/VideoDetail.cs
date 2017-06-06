using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class VideoDetail
    {
        public int VideoId { get; set; }//视频编号
        public string Vname { get; set; }//视频名称
        public int ViewedNum { get; set; }//播放次数
        public int CommentNum { get; set; }//评论个数
        public int UserId { get; set; }//用户编号
        public string Content { get; set; }//评论内容
        public string CommentTime { get; set; }//评论时间
    }
}