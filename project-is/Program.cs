using BusLine.Data;
using BusLine.Data.Models;
using BusLine.Infrastructure;
using BusLine.Infrastructure.Interfaces;
using BusLine.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_is.Mapping;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(@"Server=EDINA;Database=Buslines;Trusted_Connection=True"));
builder.Services.AddIdentity<User, AppRole>()
    .AddRoles<AppRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IBuslineRepository, BuslineRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITravelRepository, TravelRepository>();  
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleUserRepository, ScheduleUserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddAuthentication(options =>

{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}
)
.AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:5001",
        ValidIssuer = "https://localhost:5001",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTK"))
    };

});


var app = builder.Build();




using (var serviceScope = app.Services.CreateScope())
{
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    
    if(!await roleManager.RoleExistsAsync("Admin"))
    {
        var adminRole = new AppRole("Admin");
        await roleManager.CreateAsync(adminRole);
    }
    if(!await roleManager.RoleExistsAsync("Driver"))
    {
        var driverRole = new AppRole("Driver");
        await roleManager.CreateAsync(driverRole);
    }
    if(!await roleManager.RoleExistsAsync("Conductor"))
    {
        var adminRole = new AppRole("Conductor");
        await roleManager.CreateAsync(adminRole);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();



app.Run();
