using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Validator;

var builder = WebApplication.CreateBuilder(args);

// validator for user
builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.ImplicitlyValidateChildProperties = true;
});

// Swagger ve di�er gerekli servisleri ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IValidator<User>, UserValidator>();

// cors policy
var MyAllowSpecificOrigins = "MyPolicy";

// data context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductSizeService, ProductSizeService>();
builder.Services.AddScoped<IProductFavoriteService, ProductFavoriteService>();
builder.Services.AddScoped<IAddToCartService, AddToCartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();

// cors builder
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      policy =>
      {
          policy.WithOrigins("*").AllowAnyHeader().WithMethods("PUT", "DELETE", "GET", "POST");
      });
});

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



var app = builder.Build();

var allowedIPs = app.Configuration.GetSection("AllowedIPs").Get<List<string>>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
