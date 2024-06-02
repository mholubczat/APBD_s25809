using Microsoft.EntityFrameworkCore;
using Prescription.Context;
using Prescription.Repositories;
using Prescription.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDoctorService>();
builder.Services.AddScoped<IDoctorRepository>();
builder.Services.AddScoped<IMedicamentService>();
builder.Services.AddScoped<IMedicamentService>();
builder.Services.AddScoped<IPatientService>();
builder.Services.AddScoped<IPatientRepository>();
builder.Services.AddScoped<IPrescriptionService>();
builder.Services.AddScoped<IPrescriptionRepository>();
builder.Services.AddDbContext<PrescriptionAppContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();