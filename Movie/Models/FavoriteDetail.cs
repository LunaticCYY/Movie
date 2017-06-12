using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Models
{
    public class FavoriteDetail
    {
        public int VideoId { get; set; }
        public string Vname { get; set; }
        public string FavoriteTime { get; set; }
    }
}