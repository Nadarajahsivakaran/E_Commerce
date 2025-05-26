using E_Commerce_API.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.DataAccess.Repository;
using Product.Extension;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region  Authorize
builder.AddSwaggerAuthorize();  //Swagger Authorize
builder.AddAppAuthentication(); //Authorize confirmation
#endregion

#region Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        );
});
#endregion

#region Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:2001")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowSpecificOrigin");

app.Run();
