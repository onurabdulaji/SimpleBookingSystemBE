using SimpleBookingSystemBE.Application;
using SimpleBookingSystemBE.Persistence;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.UseDatabaseSeeder();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("Frontend");
app.Run();
