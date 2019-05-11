namespace THH.DAL.Migrations.ReportContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreReport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        StoreName = c.String(nullable: false, maxLength: 100),
                        StoreNo = c.String(nullable: false, maxLength: 100),
                        PaymentType = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefundQuantity = c.Int(nullable: false),
                        RefundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreRate = c.String(),
                        StoreFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SettlementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
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
            DropTable("dbo.StoreReport");
        }
    }
}
