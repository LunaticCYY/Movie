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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : ValidationAttribute
    {
        private static MovieContext db = new MovieContext();
        public override Boolean IsValid(Object value)
        {
            if(value!=null)
            return !db.Users.Any(c => c.Email.Contains(value.ToString()));
            return true;
        }
    }

    public class User
    {
        public int UserId { get; set; }//用户编号
        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string NickName { get; set; }//用户昵称
        [Required]
        [StringLength(16, MinimumLength = 6)]
        public string Password { get; set; }//用户密码
        [Required]
        [Unique]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$")]

        public string Email { get; set; }//用户邮箱
        [Required]
        public int Privilege { get; set; }//用户权限
    }
}