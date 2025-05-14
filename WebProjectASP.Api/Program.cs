using WebProjectASP.Api.Endpoints;
using WebProjectASP.Application.AIServicesRealization.Images;
using WebProjectASP.Application.ServiceBuilders.AIServicesBuilder;
using WebProjectASP.Application.AIServicesRealization.Text;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        b => b
            .WithOrigins("http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddGoogleImageServices(builder.Configuration);
builder.Services.AddOpenAITextServices(builder.Configuration);
builder.Services.AddOpenAIImageServices(builder.Configuration);
builder.Services.AddTogetherAIImageServices(builder.Configuration);
builder.Services.AddTogetherAITextServices(builder.Configuration);

builder.Services.AddSingleton<IImageGenerationMultimodal>(
    provider => new ImageGeneratorMultimodal(provider));

builder.Services.AddSingleton<ITextGenerationMultimodal>(
    provider => new TextGenerationMultimodal(provider));

builder.Services.AddSingleton<IChat, Chat>();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.MapGet("/", () => "");
app.MapAIGenerators();
app.MapStory();

app.Run();
