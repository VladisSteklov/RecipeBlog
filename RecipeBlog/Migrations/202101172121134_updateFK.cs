namespace RecipeBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Comments", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "ApplicationUserId");
            AddForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Comments", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "ApplicationUserId");
            AddForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
