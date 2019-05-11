namespace THH.DAL.Migrations.ReportContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosTxn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PointTxnDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.String(nullable: false, maxLength: 50),
                        SerialNo = c.String(nullable: false, maxLength: 50),
                        MerchantName = c.String(nullable: false, maxLength: 100),
                        TxnId = c.String(),
                        HostRef = c.String(),
                        TxnType = c.String(),
                        TxnAmt = c.String(),
                        TxnDate = c.DateTime(nullable: false),
                        SettleDate = c.DateTime(nullable: false),
                        Currency = c.String(),
                        PaymentType = c.String(nullable: false, maxLength: 50),
                        Mid = c.String(),
                        Tid = c.String(),
                        Discount = c.String(),
                        CouponName = c.String(),
                        Status = c.String(nullable: false, maxLength: 10),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PosTxnDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.String(nullable: false, maxLength: 50),
                        SerialNo = c.String(nullable: false, maxLength: 50),
                        MerchantName = c.String(nullable: false, maxLength: 100),
                        TxnDate = c.DateTime(nullable: false),
                        TxnCount = c.String(),
                        SettleDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PosTxnSettle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.String(nullable: false, maxLength: 50),
                        SerialNo = c.String(nullable: false, maxLength: 50),
                        MerchantName = c.String(nullable: false, maxLength: 100),
                        PointId = c.Int(nullable: false),
                        TxnDate = c.String(nullable: false),
                        Currency = c.String(),
                        TxnAmt = c.String(),
                        CardNo = c.String(nullable: false, maxLength: 100),
                        Point = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
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
            DropTable("dbo.PosTxnSettle");
            DropTable("dbo.PosTxnDetail");
            DropTable("dbo.PointTxnDetail");
        }
    }
}
