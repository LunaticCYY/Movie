using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class User
    {
        [Key]
        public int Uid { get; set; }
        //[Required]
        //[StringLength(16,MinimumLength = 3)]
        public string NickName { get; set; }
        //[Required]
        //[StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }
        //[Required]
        public int Privilege { get; set; }
    }
}