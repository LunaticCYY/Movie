namespace Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movie1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Comments",
                c => new
                    {
                        CommentId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        VideoId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Content = c.String(nullable: false, maxLength: 2000),
                        CommentTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("SCOTT.Comments");
        }
    }
}
