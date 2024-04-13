using MaxiShop.Infrastructure;
using MaxiShop.Application;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using MaxiShop.Infrastructure.Common;
using MaxiShop.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using MaxiShop.Application.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region service for Application

builder.Services.AddApplicationService();
#endregion

#region service for Infrastructure

builder.Services.AddInfrastructureServices();
#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy",x=> x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion

#region SSMS DbProvider
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

}).AddEntityFrameworkStores<ApplicationDbContext>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Config for Seeding Data to Database
static async void UpdateDataBaseAsync(IHost host)
{
    using(var scope = host.Services.CreateScope())
    {
        var service = scope.ServiceProvider;

        try
        {
            var context = service.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            await SeedData.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error while seeding the database");
        }
    }
}
#endregion

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

UpdateDataBaseAsync(app);

await SeedData.SeedRoles(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
