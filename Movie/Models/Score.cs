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
        public int ScoreId { get; set; }
        [Required]
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string ScoreTime { get; set; }//评分时间
        [Required]
        public int VideoId { get; set; }
        [Required]
        public int mark { get; set; }
    }
}