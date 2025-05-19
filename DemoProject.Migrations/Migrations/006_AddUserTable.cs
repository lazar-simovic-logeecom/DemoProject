using FluentMigrator;

namespace DemoProject.Migrations.Migrations;

[Migration(6, "Add User Table")]
public class AddAdminTable : Migration
{
    public override void Up()
    {
        Create.Table("User")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Username").AsString().NotNullable()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("Role").AsString().Nullable()
            .WithColumn("Token").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("User");
    }
}