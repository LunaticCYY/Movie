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
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VideoId { get; set; }
        [Required]
        [StringLength(50)]
        public string Vname { get; set; }
        [Required]
        [StringLength(100)]
        public string Vurl { get; set; }
        [Required]
        [StringLength(100)]
        public string Thumbnail { get; set; }
        public int ViewedNum { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string UploadTime { get; set; }
        [Required]
        [StringLength(50)]
        public string Vtype { get; set; }
        public int UserId { get; set; }
        [StringLength(200)]
        public string Vinfo { get; set; }
    }
}