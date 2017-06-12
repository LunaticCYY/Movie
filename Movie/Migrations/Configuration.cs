namespace Movie.Migrations
{
    using Movie.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Movie.Models.MovieContext";
        }

        protected override void Seed(Movie.Models.MovieContext context)
        {
            context.Users.AddOrUpdate(i => i.UserId,
                new User
                {
                    UserId = 1,
                    NickName = "test1",
                    Password = "123456",
                    Email = "test1@qq.com",
                    Privilege = 0
                },
                new User
                {
                    UserId = 2,
                    NickName = "test2",
                    Password = "123456",
                    Email = "test2@qq.com",
                    Privilege = 1
                },
                new User
                {
                    UserId = 3,
                    NickName = "test3",
                    Password = "123456",
                    Email = "test3@qq.com",
                    Privilege = 2
                },
                new User
                {
                    UserId = 4,
                    NickName = "test4",
                    Password = "123456",
                    Email = "test4@qq.com",
                    Privilege = 3
                }
            );
            context.Videos.AddOrUpdate(i => i.VideoId,
                new Video
                {
                    VideoId = 1,
                    Vname = "When Harry Met Sally",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/1.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "Comedy",
                    UserId = 3,
                    Vinfo = "this is a test.",
                    VideoScore = 0.0
                },
                new Video
                {
                    VideoId = 2,
                    Vname = "Rio Bravo",
                    Vurl = "~/Video/2.mp4",
                    Thumbnail = "~/Image/2.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "Western",
                    UserId = 3,
                    Vinfo = "this is a test.",
                    VideoScore = 0.0
                }
            );
        }
    }
}
