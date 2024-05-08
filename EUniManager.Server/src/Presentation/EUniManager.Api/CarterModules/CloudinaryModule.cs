using Carter;

using EUniManager.Application.Models.Cloudinary.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace EUniManager.Api.CarterModules
{
    public class CloudinaryModule : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            // app.MapGet("/{publicId}", 
            //     async (ICloudinaryService service, string publicId, CancellationToken cancellationToken) =>
            //     {
            //         bool exists = await service.ExistsAsync(publicId, cancellationToken);
            //
            //         return TypedResults.Ok(exists);
            //     });
            
            // app.MapPost("/upload",
            //     async (ICloudinaryService service, [FromBody] Dto dto, CancellationToken cancellationToken) =>
            //     {
            //         string publicId = await service.UploadAsync(dto.FileBytes, dto.MimeType, cancellationToken);
            //
            //         return TypedResults.Ok(publicId);
            //     });
            //
            // app.MapPost("/update/{id}",
            //     async (ICloudinaryService service, [FromRoute] string id, [FromBody] Dto dto, CancellationToken cancellationToken) =>
            //     {
            //         await service.UpdateAsync(id, dto.FileBytes, dto.MimeType, cancellationToken);
            //
            //         return TypedResults.Ok(id);
            //     });
            
            app.MapGet("/download/{id}",
                async (ICloudinaryService service, [FromRoute] string id, CancellationToken cancellationToken) =>
                {
                    (byte[] fileBytes, string mimeType) = await service.DownloadAsync(id, cancellationToken);
                    
                    return TypedResults.File(fileBytes, mimeType);
                });
        }
    }

    public class Dto
    {
        public byte[] FileBytes { get; set; }
        
        public string MimeType { get; set; }
    }
}