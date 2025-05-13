using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(2, "Add New Column")]
public class AddNewColumn : Migration
{
    public override void Up()
    {
        Alter.Table("Category").AddColumn("NewColumn").AsString(100).Nullable();   
    }

    public override void Down()
    {
        Delete.Column("NewColumn").FromTable("Category");
    }
}