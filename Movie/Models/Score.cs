using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class Score
    {
        [Key]
        [Display(Name = "评分编号")]
        public int ScoreId { get; set; }
        [Required]
        [Display(Name = "用户编号")]
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "评分时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string ScoreTime { get; set; }//评分时间
        [Required]
        [Display(Name = "视频编号")]
        public int VideoId { get; set; }
        [Required]
        [Display(Name = "评分")]
        public int Mark { get; set; }
    }
}