using BEcommerce2011.DAL;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 跨域
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>p.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// 数据上下文类注入
builder.Services.AddDbContext<EcommerceDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce")));

// Dal注入
builder.Services.AddScoped<EcommerceDal>();

// Session 注入(1)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// 启用NLog
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 配置Swagger显示注释
builder.Services.AddSwaggerGen(o =>
{
    string path = AppContext.BaseDirectory + "BEcommerce2011.WebAPI.xml";
    o.IncludeXmlComments(path, true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 启用静态资源查看（显示图片）
app.UseStaticFiles();

// 启用Session
app.UseSession();

app.UseAuthorization();
// 跨域
app.UseCors();

app.MapControllers();

app.Run();
