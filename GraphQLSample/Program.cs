using GraphQLSample.Data;
using GraphQLSample.GraphQL.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmployeeContext>(options =>
        options.UseSqlServer(connectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);


builder.Services
    .AddGraphQLServer()
    .AddQueryType<EmployeeQuery>()
    .RegisterDbContextFactory<EmployeeContext>()
    .AddProjections()
    .AddFiltering()
    .ModifyCostOptions(o => o.EnforceCostLimits = false)
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


InitDatabase(app);
RunSeeder(app, connectionString);
RunSeeder(app, connectionString);
RunSeeder(app, connectionString);
RunSeeder(app, connectionString);
RunSeeder(app, connectionString);

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();

app.MapGraphQL();

app.Run();

void InitDatabase(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

void RunSeeder(WebApplication webApplication, string connectionString)
{
    using var scope = webApplication.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
    // tested with manually create EmployeeContext
    //var optionsBuilder = new DbContextOptionsBuilder<EmployeeContext>();
    //optionsBuilder.UseSqlServer(connectionString);
    //using var context = new EmployeeContext(optionsBuilder.Options);
    var seeder = new EmployeeSeeder();
    seeder.Seed(context);
}