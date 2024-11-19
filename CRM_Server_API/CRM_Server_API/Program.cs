using CRM_DAL.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CRM_Business_Layer.Interfaces;
using CRM_DAL.Interfaces;
using CRM_Server_API.Mapping;
using CRM_Business_Layer.Services;
using CRM_DAL.Repositories;
using CRM_DAL.Entitys.Auth;
using System.Text.Json;
using Azure.Storage.Blobs;
using CRM_Server_API.Blobs;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

   

// For Entity Framework
builder.Services.AddDbContext<AzureDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AzureConnectionStr")));

// For Identity
builder.Services.AddIdentity<EmployeeRegisterModel, IdentityRole>()
    .AddEntityFrameworkStores<AzureDbContext>()
    .AddDefaultTokenProviders();

//Remove field $id
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; 
    });



// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//For blobs
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("BlobConnection")));
builder.Services.AddSingleton<BlobModul>(); // Регистрация BlobModul
builder.Services.AddScoped<PhotoBlobToBase64Resolver>();
builder.Services.AddScoped<PhotoBlobUploadResolver>(); // Регистрация Resolver для Upload

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
builder.Services.AddScoped<IClientService, ClientService>();
//add deal
builder.Services.AddScoped<IDealService, DealService>();
//add product
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IDealProductService, DealProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


//Documentation
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = "CRMDocumentation.xml"; 
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AzureDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCors");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
