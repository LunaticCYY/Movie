namespace Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Videos",
                c => new
                    {
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
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
        }
    }
}
