using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModel;
using StudentAdminPortal.API.Profiles;
using StudentAdminPortal.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminPortalDb")));

builder.Services.AddScoped<IStudentRepository, SQLStudentRepositories>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("angularApplication");

app.UseAuthorization();

app.MapControllers();

app.Run();
