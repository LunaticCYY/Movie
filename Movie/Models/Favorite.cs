using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }//收藏编号
        [Required]
        public int UserId { get; set; }//用户编号
        [Required]
        public int VideoId { get; set; }//视频编号
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string FavoriteTime { get; set; }//收藏时间
    }
}