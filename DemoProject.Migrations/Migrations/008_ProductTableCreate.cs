using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(8, "Create Product Table")]
public class ProductTableCreate : Migration
{
    public override void Up()
    {
        Create.Table("Product").WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Sku").AsString().NotNullable()
            .WithColumn("Brand").AsString().NotNullable()
            .WithColumn("ShortDescription").AsString().Nullable()
            .WithColumn("LongDescription").AsString().Nullable()
            .WithColumn("Price").AsDecimal().NotNullable()
            .WithColumn("CategoryId").AsGuid().Nullable()
            .WithColumn("GetTime").AsDateTime2().Nullable();
        
        Create.ForeignKey("FK_Product_Category")
            .FromTable("Product").ForeignColumn("CategoryId")
            .ToTable("Category").PrimaryColumn("Id").OnDeleteOrUpdate(System.Data.Rule.None);
    }

    public override void Down()
    {
        Delete.Table("Product");
    }
}