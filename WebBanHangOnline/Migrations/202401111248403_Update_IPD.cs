namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_IPD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ImportProductDetail", "TypeTicket", c => c.String());
            AddColumn("dbo.tb_ImportProductDetail", "ClassTicket", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_ImportProductDetail", "ClassTicket");
            DropColumn("dbo.tb_ImportProductDetail", "TypeTicket");
        }
    }
}
