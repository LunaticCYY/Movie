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
        public int VideoId { get; set; }//视频编号
        [Required]
        [StringLength(50)]
        public string Vname { get; set; }//视频名称
        [Required]
        [StringLength(100)]
        public string Vurl { get; set; }//视频存放地址
        [Required]
        [StringLength(100)]
        public string Thumbnail { get; set; }//视频缩略图地址
        public int ViewedNum { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string UploadTime { get; set; }//视频上传日期
        [Required]
        [StringLength(50)]
        public string Vtype { get; set; }//视频类型
        public int UserId { get; set; }//上传视频用户编号
        [StringLength(200)]
        public string Vinfo { get; set; }//视频简介
    }
}