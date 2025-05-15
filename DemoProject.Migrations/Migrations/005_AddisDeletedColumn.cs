using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(5, "Add isDeleted Column")]
public class AddisDeletedColumn : Migration
{
    public override void Up()
    {
        Alter.Table("Category").AddColumn("DeletedAt").AsDateTime2().Nullable();
    }

    public override void Down()
    {
        Delete.Column("DeletedAt").FromTable("Category");
    }
}