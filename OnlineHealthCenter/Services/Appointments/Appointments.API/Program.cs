using Appointments.API.GrpcServices;
using Appointments.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppointmentsInfrastructureServices();

builder.Services.AddCors(options => 
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//grpc
builder.Services.AddGrpcClient<Discounts.GRPC.Protos.DiscountsProtoService.DiscountsProtoServiceClient>(
    options => options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountsUrl"]));
builder.Services.AddScoped<DiscountsGRPCService>();

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();