namespace Movie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movie2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SCOTT.Histories",
                c => new
                    {
                        HistoryId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Vid = c.Decimal(nullable: false, precision: 10, scale: 0),
                        HistoryTime = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("SCOTT.Histories");
        }
    }
}
