using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PP.NORE.Builder;
using PP.NORE.Builder.CodeTemplates;
using PP.NORE.Builder.Compilers;
using PP.NORE.Builder.Contracts;
using PP.NORE.RatingEngine;
using PP.NORE.Shared.Contracts;

using PP.NORE.Shared.Models;




var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITemplateManager, TemplateManager>();
builder.Services.AddScoped<ICodeBuilder, CodeBuilder>();
builder.Services.AddScoped<ICodeCompiler, DefaultCompiler>();
builder.Services.AddScoped<IProductRater, RatingProcessor>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/CodeBuilder", (ICodeBuilder codeBuilder, IConfiguration configuration) =>
{
	var productPath = configuration["NORE:PRODUCT_PATH"];
	var project = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(Path.Combine(productPath, "Project.json")));
	project.ProductDataPath = productPath;
	var result = codeBuilder.BuildProject(project);
	if (result.Item1)
	{
		return Results.Ok();
	}
	else
	{
		return Results.BadRequest(result.Item2);
	}
})
.WithName("CodeBuilder")
.WithOpenApi();

app.MapPost("/Rate", ([FromBody] RateRequest request, IProductRater rater, IConfiguration configuration) =>
{
	request.ProductDataPath = configuration["NORE:PRODUCT_PATH"];

	return Results.Ok(rater.RateProduct(request));
})
.WithName("Rate")
.WithOpenApi();


app.Run();

