using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres() 
        .WithGlobalConnectionString("Host=localhost;Port=5432;Username=user;Password=pass;Database=DemoProjectDb") 
        .ScanIn(Assembly.Load("DemoProject.Migrations")).For.Migrations()) 
    .BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();  
}

Console.WriteLine("Migracije su primenjene.");