using FluentMigrator;
using FluentMigrator.Runner.Conventions;

namespace DemoProject.Migrations.Migrations;

[Migration(7, "Add New Users")]
public class AddNewUsers : ForwardOnlyMigration
{
    public override void Up()
    {
        var lazarPassword = BCrypt.Net.BCrypt.HashPassword("lazar123"); 
        var mladenPassword = BCrypt.Net.BCrypt.HashPassword("mladen123"); 

        Insert.IntoTable("User")
            .Row(new { Id = Guid.NewGuid(), Username = "lazar", Password = lazarPassword, Role = "Admin" })
            .Row(new { Id = Guid.NewGuid(), Username = "mladen", Password = mladenPassword });
    }
}