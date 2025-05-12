using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(3, "Add New Category")]
public class AddNewCategory : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Category").Row(new { Id = Guid.NewGuid(), Title = "Ass", Description = "Category Description", Code = "123", ParentCategory = (object)null });
    }

    public override void Down()
    {
    }
}