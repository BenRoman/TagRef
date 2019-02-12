namespace TagRef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagsValue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.References", "TagsValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.References", "TagsValue", c => c.String());
        }
    }
}
