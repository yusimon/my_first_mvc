namespace AuthWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivityTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 255),
                    ModifiedDate = c.DateTime(),
                    Description = c.String(maxLength: 100),
                    Image = c.Binary()
                })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.Activities");
        }
    }
}
