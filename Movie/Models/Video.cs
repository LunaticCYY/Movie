using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Video
    {
        [Display(Name = "视频编号")]
        public int VideoId { get; set; }//视频编号
        [Required]
        [Display(Name = "视频名称")]
        [StringLength(50)]
        public string Vname { get; set; }//视频名称
        [Required]
        [Display(Name = "视频存放地址")]
        [StringLength(100)]
        public string Vurl { get; set; }//视频存放地址
        [Required]
        [Display(Name = "视频缩略图地址")]
        [StringLength(100)]
        public string Thumbnail { get; set; }//视频缩略图地址
        [Display(Name = "播放人数")]
        public int ViewedNum { get; set; }//播放数
        [DataType(DataType.Date)]
        [Display(Name = "视频上传日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string UploadTime { get; set; }//视频上传日期
        public enum Types
        {
            动作, 喜剧, 科幻, 爱情, 纪录, 动画, 恐怖, 悬疑, 青春, 文艺, 励志, 战争, 犯罪,剧情, 音乐, 历史
        }
        [Required]
        [Display(Name = "视频类型")]
        public Types? Vtype { get; set; }//视频类型
        [Display(Name = "上传视频用户编号")]
        public int UserId { get; set; }//上传视频用户编号
        [Display(Name = "视频简介")]
        [StringLength(1000)]
        public string Vinfo { get; set; }//视频简介

    }
}