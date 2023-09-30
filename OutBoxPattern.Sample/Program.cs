using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OutBoxPattern.Sample;
using OutBoxPattern.Sample.BackgroundServices;
using OutBoxPattern.Sample.Models;
using OutBoxPattern.Sample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var emailSettings = new EmailSettings();
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.Bind(EmailSettings.SectionName, emailSettings);
builder.Services.AddSingleton(Options.Create(emailSettings));
builder.Services.AddSingleton<IMailService, EmailService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailOutbox, EmailOutboxService>();
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHostedService<EmailBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();