namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_P : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "TypeTicket", c => c.String());
            AddColumn("dbo.tb_Product", "ClassTicket", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ClassTicket");
            DropColumn("dbo.tb_Product", "TypeTicket");
        }
    }
}
