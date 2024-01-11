namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TCP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Type_ClassProduct", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Type_ClassProduct", "Quantity");
        }
    }
}
