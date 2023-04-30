using BEcommerce2011.DAL;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ����
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>p.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// ������������ע��
builder.Services.AddDbContext<EcommerceDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce")));

// Dalע��
builder.Services.AddScoped<EcommerceDal>();

// Session ע��(1)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// ����NLog
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// ����Swagger��ʾע��
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

// ���þ�̬��Դ�鿴����ʾͼƬ��
app.UseStaticFiles();

// ����Session
app.UseSession();

app.UseAuthorization();
// ����
app.UseCors();

app.MapControllers();

app.Run();
