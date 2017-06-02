using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class History
    {
        [Key]
        public int Hid { get; set; }
        //[Required]
        public int Uid { get; set; }
        //[Required]
        public int Vid { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required]
        public string HistoryTime { get; set; }
    }
}