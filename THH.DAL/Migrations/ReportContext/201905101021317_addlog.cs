namespace THH.DAL.Migrations.ReportContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogType = c.String(),
                        ReportId = c.Int(nullable: false),
                        ReportName = c.String(),
                        Content = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReportLog");
        }
    }
}
