namespace quanlyvatut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.nhaps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        maphieu = c.String(),
                        name = c.String(),
                        day = c.DateTime(nullable: false),
                        creator = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.phieunhaps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nhapID = c.Int(nullable: false),
                        vattuID = c.Int(nullable: false),
                        soluong = c.Int(nullable: false),
                        dongia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.nhaps", t => t.nhapID, cascadeDelete: true)
                .ForeignKey("dbo.vattus", t => t.vattuID, cascadeDelete: true)
                .Index(t => t.nhapID)
                .Index(t => t.vattuID);
            
            CreateTable(
                "dbo.vattus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        mavattu = c.String(),
                        name = c.String(),
                        soluong = c.Int(nullable: false),
                        dongia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ngaytao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.phieuxuats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        xuatID = c.Int(nullable: false),
                        vattuID = c.Int(nullable: false),
                        soluong = c.Int(nullable: false),
                        dongia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.vattus", t => t.vattuID, cascadeDelete: true)
                .ForeignKey("dbo.xuats", t => t.xuatID, cascadeDelete: true)
                .Index(t => t.xuatID)
                .Index(t => t.vattuID);
            
            CreateTable(
                "dbo.xuats",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        maphieu = c.String(),
                        name = c.String(),
                        day = c.DateTime(nullable: false),
                        creator = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.phieuxuats", "xuatID", "dbo.xuats");
            DropForeignKey("dbo.phieuxuats", "vattuID", "dbo.vattus");
            DropForeignKey("dbo.phieunhaps", "vattuID", "dbo.vattus");
            DropForeignKey("dbo.phieunhaps", "nhapID", "dbo.nhaps");
            DropIndex("dbo.phieuxuats", new[] { "vattuID" });
            DropIndex("dbo.phieuxuats", new[] { "xuatID" });
            DropIndex("dbo.phieunhaps", new[] { "vattuID" });
            DropIndex("dbo.phieunhaps", new[] { "nhapID" });
            DropTable("dbo.xuats");
            DropTable("dbo.phieuxuats");
            DropTable("dbo.vattus");
            DropTable("dbo.phieunhaps");
            DropTable("dbo.nhaps");
        }
    }
}
