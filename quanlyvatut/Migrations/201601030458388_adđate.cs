namespace quanlyvatut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adđate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.phieunhaps", "ngaytaophieu", c => c.DateTime(nullable: false));
            AddColumn("dbo.phieuxuats", "ngaytaophieu", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.phieuxuats", "ngaytaophieu");
            DropColumn("dbo.phieunhaps", "ngaytaophieu");
        }
    }
}
