using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using ECommerce.Business.Security.JWT;
using ECommerce.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using ECommerce.Business.ValidationRules.FluentValidation;
using Microsoft.AspNetCore.Identity;
using ECommerce.Entities.Concrete;
using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete;
using Microsoft.OpenApi.Models;
using ECommerce.Business.Payment;
using ECommerce.Business.Cargo;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlConnection"));
});
// Add services to the container.
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<ICampaignService, CampaignManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICartDal, EfCartDal>();
builder.Services.AddScoped<IOrderDal, EfOrderDal>();
builder.Services.AddScoped<ICampaignDal, EfCampaignDal>();
builder.Services.AddScoped<
    IPasswordHasher<User>,
    PasswordHasher<User>>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<IPaymentService,
    CreditCardPaymentManager>();

builder.Services.AddScoped<PaymentAdapter>();
var tokenOptions = builder.Configuration.GetSection("TokenOptions");

builder.Services.AddScoped<CargoFactory>();
string securityKey = tokenOptions["SecurityKey"]!;

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = tokenOptions["Issuer"],

                ValidAudience = tokenOptions["Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(securityKey))
            };
    });
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ECommerce API",
            Version = "v1"
        });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description = "JWT Token giriniz."
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,

                        Id = "Bearer"
                    }
                },

                Array.Empty<string>()
            }
        });
});
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

app.Run();
