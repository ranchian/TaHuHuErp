namespace THH.DAL.Migrations.ReportContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifCompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Merchant", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Store", "MerchantId", "dbo.Merchant");
            DropForeignKey("dbo.Machine", "StoreId", "dbo.Store");
            DropIndex("dbo.Machine", new[] { "StoreId" });
            DropIndex("dbo.Store", new[] { "MerchantId" });
            DropIndex("dbo.Merchant", new[] { "CompanyId" });
            AddColumn("dbo.Machine", "MerchantId", c => c.Int(nullable: false));
            AddColumn("dbo.Store", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Merchant", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Machine", "MerchantId");
            CreateIndex("dbo.Merchant", "StoreId");
            CreateIndex("dbo.Store", "CompanyId");
            AddForeignKey("dbo.Store", "CompanyId", "dbo.Company", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Merchant", "StoreId", "dbo.Store", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Machine", "MerchantId", "dbo.Merchant", "Id", cascadeDelete: true);
            DropColumn("dbo.Machine", "StoreId");
            DropColumn("dbo.Store", "MerchantId");
            DropColumn("dbo.Merchant", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Merchant", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Store", "MerchantId", c => c.Int(nullable: false));
            AddColumn("dbo.Machine", "StoreId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Machine", "MerchantId", "dbo.Merchant");
            DropForeignKey("dbo.Merchant", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Store", "CompanyId", "dbo.Company");
            DropIndex("dbo.Store", new[] { "CompanyId" });
            DropIndex("dbo.Merchant", new[] { "StoreId" });
            DropIndex("dbo.Machine", new[] { "MerchantId" });
            DropColumn("dbo.Merchant", "StoreId");
            DropColumn("dbo.Store", "CompanyId");
            DropColumn("dbo.Machine", "MerchantId");
            CreateIndex("dbo.Merchant", "CompanyId");
            CreateIndex("dbo.Store", "MerchantId");
            CreateIndex("dbo.Machine", "StoreId");
            AddForeignKey("dbo.Machine", "StoreId", "dbo.Store", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Store", "MerchantId", "dbo.Merchant", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Merchant", "CompanyId", "dbo.Company", "Id", cascadeDelete: true);
        }
    }
}
