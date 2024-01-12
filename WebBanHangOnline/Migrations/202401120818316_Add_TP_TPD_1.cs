namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TP_TPD_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Time_Promotion_Detail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimePromotionId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.tb_Time_Promotion", t => t.TimePromotionId, cascadeDelete: true)
                .Index(t => t.TimePromotionId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_Time_Promotion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PercentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        IsBan = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Time_Promotion_Detail", "TimePromotionId", "dbo.tb_Time_Promotion");
            DropForeignKey("dbo.tb_Time_Promotion_Detail", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Time_Promotion_Detail", new[] { "ProductId" });
            DropIndex("dbo.tb_Time_Promotion_Detail", new[] { "TimePromotionId" });
            DropTable("dbo.tb_Time_Promotion");
            DropTable("dbo.tb_Time_Promotion_Detail");
        }
    }
}
