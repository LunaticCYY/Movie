namespace Movie.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Movie.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Movie.Models.MovieContext";
        }

        protected override void Seed(Movie.Models.MovieContext context)
        {
            context.Videos.AddOrUpdate(i => i.Vname, new Video
            {
                VideoId = 1,
                Vname = "When Harry Met Sally",
                Vurl = "video/1.mp4",
                Thumbnail = "img/1.jpg",
                ViewedNum = 0,
                UploadTime = "2017-6-5",
                Vtype="Commedy",
                UserId=1,
                Vinfo="this is a test"
            });
        }
    }
}
