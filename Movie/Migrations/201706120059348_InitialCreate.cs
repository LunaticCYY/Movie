namespace Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Comments",
                c => new
                    {
                        CommentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Content = c.String(nullable: false, maxLength: 2000),
                        CommentTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateTable(
                "SCOTT.Favorites",
                c => new
                    {
                        FavoriteId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        FavoriteTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FavoriteId);
            
            CreateTable(
                "SCOTT.Histories",
                c => new
                    {
                        HistoryId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HistoryTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryId);
            
            CreateTable(
                "SCOTT.Users",
                c => new
                    {
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NickName = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                        Email = c.String(nullable: false, maxLength: 50),
                        Privilege = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "SCOTT.Videos",
                c => new
                    {
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Vname = c.String(nullable: false, maxLength: 50),
                        Vurl = c.String(nullable: false, maxLength: 100),
                        Thumbnail = c.String(nullable: false, maxLength: 100),
                        ViewedNum = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UploadTime = c.String(nullable: false),
                        Vtype = c.String(nullable: false, maxLength: 50),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Vinfo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.VideoId);
            
        }
        
        public override void Down()
        {
            DropTable("SCOTT.Videos");
            DropTable("SCOTT.Users");
            DropTable("SCOTT.Histories");
            DropTable("SCOTT.Favorites");
            DropTable("SCOTT.Comments");
        }
    }
}
