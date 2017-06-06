using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class History
    {
        public int HistoryId { get; set; }//播放历史编号
        [Required]
        public int UserId { get; set; }//用户编号
        [Required]
        public int VideoId { get; set; }//播放视频编号
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string HistoryTime { get; set; }//播放历史时间
    }
}