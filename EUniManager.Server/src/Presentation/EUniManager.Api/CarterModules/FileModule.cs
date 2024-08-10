using Carter;

using EUniManager.Application.Models.Cloudinary.Interfaces;

using Microsoft.AspNetCore.Mvc;

using static EUniManager.Api.Constants.RoutesConstant;

namespace EUniManager.Api.CarterModules
{
    public class FileModule : CarterModule
    {
        public FileModule() : base(string.Format(BASE_ROUTE_TEMPLATE, FILES_ROUTE))
        {
            WithTags(nameof(File));
            RequireAuthorization();
        }
        
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/download/{id}", Download);
        }

        private async Task<IResult> Download(ICloudinaryService cloudinaryService,
                                             [FromRoute] string id,
                                             CancellationToken cancellationToken)
        {
            (byte[] fileBytes, string mimeType) = await cloudinaryService.DownloadAsync(id, cancellationToken);
                    
            return TypedResults.File(fileBytes, mimeType);
        }
    }
}