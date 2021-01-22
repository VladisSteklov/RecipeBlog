namespace RecipeBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recipes", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            DropIndex("dbo.Recipes", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Comments", "ApplicationUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Recipes", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "ApplicationUserId");
            CreateIndex("dbo.Recipes", "ApplicationUserId");
            AddForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Recipes", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Recipes", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Recipes", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Recipes", "ApplicationUserId");
            CreateIndex("dbo.Comments", "ApplicationUserId");
            AddForeignKey("dbo.Recipes", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}