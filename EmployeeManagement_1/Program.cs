using EmployeeManagement_1.Common;
using EmployeeManagement_1.Cosmos;
using EmployeeManagement_1.Interface;
using EmployeeManagement_1.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
builder.Services.AddSingleton<ILeadService, LeadService>();
builder.Services.AddSingleton<IMemberService, MemberService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add services to the container.
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
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();



