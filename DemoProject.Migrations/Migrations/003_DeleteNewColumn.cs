using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(4,"Delete New Column")]
public class DeleteNewColumn : Migration
{
    public override void Up()
    {   
        Delete.Column("NewColumn").FromTable("Category");
    }

    public override void Down()
    {
        Alter.Table("Category").AddColumn("NewColumn").AsString(100).Nullable();   
    }
}