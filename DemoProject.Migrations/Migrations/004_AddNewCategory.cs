using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(4, "Add New Category")]
public class AddNewCategory : ForwardOnlyMigration
{
    public override void Up()
    {
        Insert.IntoTable("Category").Row(new
            { Id = Guid.NewGuid(), Title = "Ass", Description = "Category Description", Code = "123" });
    }
}