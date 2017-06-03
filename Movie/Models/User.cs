using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Movie.Models
{
    public class User
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [Required]
        [StringLength(16,MinimumLength = 3)]
        public string NickName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]
        public string Email { get; set; }
        [Required]
        public int Privilege { get; set; }
    }
}