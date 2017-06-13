using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Favorite
    {
        [Display(Name = "收藏编号")]
        public int FavoriteId { get; set; }//收藏编号
        [Required]
        [Display(Name = "用户编号")]
        public int UserId { get; set; }//用户编号
        [Required]
        [Display(Name = "视频编号")]
        public int VideoId { get; set; }//视频编号
        [DataType(DataType.Date)]
        [Display(Name = "收藏时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string FavoriteTime { get; set; }//收藏时间
    }
}