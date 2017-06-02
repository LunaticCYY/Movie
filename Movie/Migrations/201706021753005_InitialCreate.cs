namespace Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Users",
                c => new
                    {
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        NickName = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                        Email = c.String(nullable: false, maxLength: 50),
                        Privilege = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("SCOTT.Users");
        }
    }
}
