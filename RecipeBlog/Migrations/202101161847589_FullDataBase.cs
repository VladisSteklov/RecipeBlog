namespace RecipeBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentBody = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        RecipeId = c.Int(nullable: false),
                        CommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.RecipeId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Ingredients = c.String(maxLength: 256),
                        Description = c.String(),
                        Minutes = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Meal = c.String(maxLength: 50),
                        CountryKitchen = c.String(maxLength: 50),
                        ImageName = c.String(maxLength: 50),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Recipes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CommentId", "dbo.Comments");
            DropIndex("dbo.Recipes", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            DropTable("dbo.Recipes");
            DropTable("dbo.Comments");
        }
    }
}
