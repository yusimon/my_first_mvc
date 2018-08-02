namespace AuthWithDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnChanged : DbMigration
    {
        public override void Up()
        {
            RenameColumn("Files", "Filename" , "FileName");
        }
        
        public override void Down()
        {
            RenameColumn("Files", "FileName", "Filename");
        }
    }
}
