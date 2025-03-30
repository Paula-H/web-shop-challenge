using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repository;
using Repository.Abstract;
using Repository.Concrete;
using Service.Abstract;
using Service.Concrete;
using FluentValidation;
using Domain.Entity;
using Domain.Dto.Create;
using Domain.Helper;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();
string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
Environment.SetEnvironmentVariable("JWT_SECRET", jwtSecret);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Automapper scoped
builder.Services.AddAutoMapper(typeof(Mapper));

// Validators scoped
builder.Services.AddScoped<AbstractValidator<User>, UserValidator>();
builder.Services.AddScoped<AbstractValidator<Order>, OrderValidator>();
builder.Services.AddScoped<AbstractValidator<Product>, ProductValidator>();
builder.Services.AddScoped<AbstractValidator<Coupon>, CouponValidator>();
builder.Services.AddScoped<AbstractValidator<CreateOrderDto>, CreateOrderDtoValidator>();

// Repositories scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductMappingRepository, OrderProductMappingRepository>();
builder.Services.AddScoped<IOrderCouponMappingRepository, OrderCouponMappingRepository>();
builder.Services.AddScoped<IUserCouponMappingRepository, UserCouponMappingRepository>();

// Services scoped
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Authentication (JWT)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Only set to true in production
    options.SaveToken = true;

    // Set up the JWT Token validation parameters
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // "yourIssuer"
        ValidAudience = builder.Configuration["Jwt:Audience"], // "yourAudience"
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)) // Your secret key
    };
});

// Add Authorization
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add authentication & authorization middleware
app.UseAuthentication();  // Authentication middleware (this must come before authorization)
app.UseAuthorization();   // Authorization middleware

// Map controllers
app.MapControllers();

app.Run();
