using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Enums;
using WebProjectASP.Domain.Abstractions.AIServicesContracts.Images.Interfaces;
using WebProjectASP.Domain.Models;

namespace WebProjectASP.Api.Endpoints;

public static class AIEndpointsExtension
{
    public static IEndpointRouteBuilder MapAIGenerators(this IEndpointRouteBuilder endpoints)
    {
        var generateGroup = endpoints.MapGroup("/generate");

        generateGroup.MapPost("/image",
            async (IImageGenerationMultimodal generator,
                ImageRequestDto imageRequest
            ) =>
            {
                var image = await generator.GetImageByDescriptionAsync(
                    imageRequest.Prompt,
                    Styles.Comics,
                    imageRequest.Model);

                return image == null ? Results.Problem(statusCode: 500) : Results.File(image, "image/png");
            });

        return generateGroup;
    }
}