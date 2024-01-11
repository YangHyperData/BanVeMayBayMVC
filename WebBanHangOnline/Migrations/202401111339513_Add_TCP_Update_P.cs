namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TCP_Update_P : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Type_ClassProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        TypeTicket = c.String(),
                        ClassTicket = c.String(),
                        Price = c.Int(nullable: false),
                        PriceSale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.tb_Product", "TypeTicket");
            DropColumn("dbo.tb_Product", "ClassTicket");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "ClassTicket", c => c.String());
            AddColumn("dbo.tb_Product", "TypeTicket", c => c.String());
            DropForeignKey("dbo.tb_Type_ClassProduct", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_Type_ClassProduct", new[] { "ProductId" });
            DropTable("dbo.tb_Type_ClassProduct");
        }
    }
}
