namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IP_IPD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_ImportProductDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImportProductId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Title = c.String(),
                        Quantity = c.Int(nullable: false),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_ImportProduct", t => t.ImportProductId, cascadeDelete: true)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ImportProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_ImportProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Note = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_ImportProductDetail", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_ImportProductDetail", "ImportProductId", "dbo.tb_ImportProduct");
            DropIndex("dbo.tb_ImportProductDetail", new[] { "ProductId" });
            DropIndex("dbo.tb_ImportProductDetail", new[] { "ImportProductId" });
            DropTable("dbo.tb_ImportProduct");
            DropTable("dbo.tb_ImportProductDetail");
        }
    }
}
