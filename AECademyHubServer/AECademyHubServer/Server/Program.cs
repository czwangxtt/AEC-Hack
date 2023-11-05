global using AECademyHubServer.Server.Services.ObjectService;
global using AECademyHubServer.Server.Services.SuggestionService;
global using AECademyHubServer.Server.Services.QueueService;
global using AECademyHubServer.Server.Data;
global using AECademyHubServer.Shared;
global using AECademyHubServer.Shared.Suggestion;
global using AECademyHubServer.Shared.Object;
global using AECademyHubServer.Shared.Queue;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IObjectService, ObjectService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();
builder.Services.AddScoped<IQueueService, QueueService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
