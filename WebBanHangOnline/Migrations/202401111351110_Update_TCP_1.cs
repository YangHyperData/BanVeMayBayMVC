namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TCP_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tb_Type_ClassProduct", "Price");
            DropColumn("dbo.tb_Type_ClassProduct", "PriceSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Type_ClassProduct", "PriceSale", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Type_ClassProduct", "Price", c => c.Int(nullable: false));
        }
    }
}
