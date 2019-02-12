namespace TagRef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.References",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        TagsValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagReferences",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Reference_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Reference_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.References", t => t.Reference_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Reference_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagReferences", "Reference_Id", "dbo.References");
            DropForeignKey("dbo.TagReferences", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagReferences", new[] { "Reference_Id" });
            DropIndex("dbo.TagReferences", new[] { "Tag_Id" });
            DropTable("dbo.TagReferences");
            DropTable("dbo.Tags");
            DropTable("dbo.References");
        }
    }
}
