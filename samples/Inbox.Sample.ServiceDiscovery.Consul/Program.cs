using Inbox.ServiceDiscovery.Consul;
using Inbox.ServiceDiscovery.LoadBalancers;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加服务
var consulOptions = builder.Configuration.GetSection("Consul").Get<ConsulOptions>();
builder.Services.AddConsul(consulOptions.ConsulUrl, loadBalancer: TypeLoadBalancer.RoundRobin);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//注册到Consul
app.RegisterToConsul(consulOptions);

app.Run();
