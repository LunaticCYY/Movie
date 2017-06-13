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
        [Display(Name = "历史编号")]
        public int HistoryId { get; set; }//播放历史编号
        [Required]
        [Display(Name = "用户编号")]
        public int UserId { get; set; }//用户编号
        [Required]
        [Display(Name = "视频编号")]
        public int VideoId { get; set; }//播放视频编号
        [DataType(DataType.Date)]
        [Display(Name = "播放时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string HistoryTime { get; set; }//播放历史时间
    }
}