namespace RecipeBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NickName", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NickName");
        }
    }
}
