namespace GeneralStoreAPI_SD105.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemCount = c.Int(nullable: false),
                        DateOfTransaction = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ProductSKU = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductSKU)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductSKU);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ProductSKU", "dbo.Products");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "ProductSKU" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropTable("dbo.Transactions");
        }
    }
}
