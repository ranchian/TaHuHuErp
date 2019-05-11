namespace THH.DAL.Migrations.ErpContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initErp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(nullable: false, maxLength: 255),
                        Picture = c.String(maxLength: 255),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VipPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IfShelves = c.Boolean(nullable: false),
                        IfPromotion = c.Boolean(nullable: false),
                        Introduction = c.String(maxLength: 255),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogType = c.String(nullable: false, maxLength: 100),
                        LogContent = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 100),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(nullable: false),
                        State = c.String(),
                        Country = c.String(),
                        StudentId = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.SysButton",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ButtonName = c.String(nullable: false, maxLength: 100),
                        ButtonIocn = c.String(maxLength: 100),
                        ButtonCode = c.String(nullable: false, maxLength: 100),
                        InputType = c.String(maxLength: 200),
                        Status = c.String(maxLength: 10),
                        ButtonStyle = c.String(maxLength: 100),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SysMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuCode = c.String(nullable: false, maxLength: 100),
                        Url = c.String(nullable: false, maxLength: 100),
                        MenuName = c.String(nullable: false, maxLength: 100),
                        MenuLevel = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        SortNumber = c.Int(nullable: false),
                        Icon = c.String(),
                        Status = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        LogingName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Status = c.String(maxLength: 10),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SysMenuId = c.Int(nullable: false),
                        SysButtonId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SysButton", t => t.SysButtonId, cascadeDelete: true)
                .ForeignKey("dbo.SysMenus", t => t.SysMenuId, cascadeDelete: true)
                .Index(t => t.SysMenuId)
                .Index(t => t.SysButtonId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CredateTime = c.DateTime(nullable: false),
                        UpdateUser = c.String(),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRights", "SysMenuId", "dbo.SysMenus");
            DropForeignKey("dbo.UserRights", "SysButtonId", "dbo.SysButton");
            DropForeignKey("dbo.StudentAddress", "StudentId", "dbo.Student");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRights", new[] { "SysButtonId" });
            DropIndex("dbo.UserRights", new[] { "SysMenuId" });
            DropIndex("dbo.StudentAddress", new[] { "StudentId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.UserRights");
            DropTable("dbo.User");
            DropTable("dbo.TestTable");
            DropTable("dbo.SysMenus");
            DropTable("dbo.SysButton");
            DropTable("dbo.StudentAddress");
            DropTable("dbo.Student");
            DropTable("dbo.Role");
            DropTable("dbo.Log");
            DropTable("dbo.Goods");
        }
    }
}
