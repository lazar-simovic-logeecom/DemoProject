using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(1, "Create Category Table")]
public class CategoryTableCreate : Migration
{
    public override void Up()
    {
        Create.Table("Category")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("Code").AsString().NotNullable()
            .WithColumn("ParentCategory").AsGuid().Nullable();

        Create.ForeignKey("FK_Category_Category")
            .FromTable("Category").ForeignColumn("ParentCategory")
            .ToTable("Category").PrimaryColumn("Id").OnDeleteOrUpdate(System.Data.Rule.None);
    }

    public override void Down()
    {
        Delete.Table("Category");
    }
}