namespace AuthWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Contents",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Description = c.String(),
            //            Contents = c.String(),
            //            Image = c.Binary(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Filename = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        EnrollmentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.Lists",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Details = c.String(nullable: false),
            //            Date_Posted = c.String(),
            //            Time_Posted = c.String(),
            //            Date_Edited = c.String(),
            //            Time_Edited = c.String(),
            //            Public = c.String(),
            //            User_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.NewUsers",
            //    c => new
            //        {
            //            NewUserID = c.Int(nullable: false, identity: true),
            //            LastName = c.String(nullable: false, maxLength: 50),
            //            FirstName = c.String(nullable: false, maxLength: 50),
            //            Email = c.String(nullable: false),
            //            Password = c.String(nullable: false),
            //            DateEdited = c.String(),
            //        })
            //    .PrimaryKey(t => t.NewUserID);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Email = c.String(nullable: false),
            //            Password = c.String(nullable: false),
            //            Name = c.String(),
            //            Country = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "PersonId", "dbo.People");
            DropIndex("dbo.Files", new[] { "PersonId" });
            //DropTable("dbo.Users");
            //DropTable("dbo.NewUsers");
            //DropTable("dbo.Lists");
            DropTable("dbo.People");
            DropTable("dbo.Files");
            //DropTable("dbo.Contents");
        }
    }
}
