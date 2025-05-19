using EFCore.BulkExtensions;
using Fortunes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FortuneContext>(db =>
    db.UseNpgsql(builder.Configuration.GetConnectionString("Fortunes"))
        .UseSeeding((context, b) =>
        {
            if (context.Set<Fortune>().Any()) return;
            var fortunes = FortuneSource.Get()
                .Select(t => new Fortune { Text = t });
            context.BulkInsert(fortunes);
        }));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using (var context = scope.ServiceProvider.GetRequiredService<FortuneContext>())
    {
        context.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.MapGet("/fortune", (FortuneContext context) =>
{
    var fortune = context.Fortunes.OrderBy(f => EF.Functions.Random()).First();
    return fortune;
});

app.Run();