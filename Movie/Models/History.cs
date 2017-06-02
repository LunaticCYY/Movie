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
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HistoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Vid { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public string HistoryTime { get; set; }
    }
}