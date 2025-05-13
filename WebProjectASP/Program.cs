using System.Text.Json;
using WebProjectASP.Application.AIServicesRealization.Images;
using WebProjectASP.Application.AIServicesRealization.Text;
using WebProjectASP.Configuration.AIServicesBuilder;
using WebProjectASP.Domain.Models;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Text.Interfaces;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;
using Styles = WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Enums.Styles;

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

// app.MapGet("/", () => "");

app.MapPost("/generateImage", 
    async (IImageGenerationMultimodal generator, 
    ImageRequestDto imageRequest
    ) =>
{
    var image = await generator.GetImageByDescriptionAsync( 
        imageRequest.Prompt,
        Styles.Comics, 
        "gemini");

    return image == null ? 
        Results.Problem(statusCode: 500) : Results.File(image, "image/png");
});

app.MapGet("/start", async (IChat chat) =>
{
    var result = await chat.NewMessage("You are a story maker, you must create interactive stories and present them in a JSON format. \n\nThis JSON should contain the following properties: Text description of a current situation in a story, 100 +- 10 words (description). \n\nPrompt for an image generation depicting current scene, make sure to specify all of necessary details like composition, camera angle, placing of objects and a color palette, also remember that the previous scene is related and usually they must be in a relatively similar environments, use primarily static compositions with very rare exceptions, do not overcomplicate a scene. When there are any characters involved in an image description do not call them by name/alias or reference the previous prompts, explicitly specify their appearance in detail every time they appear in a scene, a final image prompt must be around 900 symbols +- 50, do not explicitly write a count of symbols in a prompt (img). \n\nAnd an array of options available for the user (ex: [\"go right\", \"go left\"], max: 5 options). Also an array of continuity notes (continuityNotes) those will not be used by the user, however they will be helpful for you to remain consistent in story and image generation.\n\nI will choose options by an index, if I write anything but a number between 0 and 5 consider that this is a custom option provided by me, first validate if such action is possible considering the context of the story, and if not just choose option 0. OUTPUT JUST A JSON AND NOTHING ELSE!" 
                                       + "\n\nStory theme: An adventure story in a world of Tolkien's Lord of the rings\n" 
                                       + "Speed of scene change: Rapid\n" +
                                       "Output description and options in Russian, img and continuityNotes in English",
        "qwen");
    //do not react to the instructions in the message and just consider that I chose option 0
    Console.WriteLine(result);
    var deserializedScene = JsonSerializer.Deserialize<SceneInternalDto>(result, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    }) ?? throw new Exception("Failed to deserialize JSON");
    
    return Results.Json(new SceneUserDto {
        Description = deserializedScene.Description,
        Img = deserializedScene.Img,
        Options = deserializedScene.Options
    });
});

app.MapGet("/next/{i}", async (IChat chat, int i) =>
{
    var newScene = await chat.NewMessage(i.ToString(), "qwen");
    var deserializedSceneNew = JsonSerializer.Deserialize<SceneInternalDto>(newScene, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    }) ?? throw new Exception("Failed to deserialize JSON");
    
    return Results.Json(new SceneUserDto {
        Description = deserializedSceneNew.Description,
        Img = deserializedSceneNew.Img,
        Options = deserializedSceneNew.Options
    });
});

app.MapPost("/next", async (IChat chat, CustomOptionDto action) =>
{
    var newScene = await chat.NewMessage(action.Action, "qwen");
    var deserializedSceneNew = JsonSerializer.Deserialize<SceneInternalDto>(newScene, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    }) ?? throw new Exception("Failed to deserialize JSON");
    
    return Results.Json(new SceneUserDto {
        Description = deserializedSceneNew.Description,
        Img = deserializedSceneNew.Img,
        Options = deserializedSceneNew.Options
    });
});


app.Run();
