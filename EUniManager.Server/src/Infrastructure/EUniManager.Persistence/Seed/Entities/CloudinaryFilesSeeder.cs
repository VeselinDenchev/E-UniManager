using System.Reflection;

using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using PommaLabs.MimeTypes;

namespace EUniManager.Persistence.Seed.Entities;

public static class CloudinaryFilesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext, ICloudinaryService cloudinaryService)
    {
        if (await dbContext.CloudinaryFiles.AsNoTracking().AnyAsync()) return;

        const string FILES_PATH_TEMPLATE = "/app/src/Infrastructure/EUniManager.Persistence/Seed/Entities/Files/{0}";
        const string WORD_DOCUMENT_FILE_NAME = "word-document.docx";
        const string EXCEL_WORKSHEET_FILE_NAME = "excel-worksheet.xlsx";
        const string POWER_POINT_PRESENTATION_FILE_NAME = "power-point-presentation.pptx";
        const string POF_DOCUMENT_FILE_NAME = "pdf-document.pdf";
        const string IMAGE_FILE_NAME = "bitmap-image.bmp";
        
        string[] filePaths =
        [
            string.Format(FILES_PATH_TEMPLATE, WORD_DOCUMENT_FILE_NAME),
            string.Format(FILES_PATH_TEMPLATE, EXCEL_WORKSHEET_FILE_NAME),
            string.Format(FILES_PATH_TEMPLATE, POWER_POINT_PRESENTATION_FILE_NAME),
            string.Format(FILES_PATH_TEMPLATE, POF_DOCUMENT_FILE_NAME),
            string.Format(FILES_PATH_TEMPLATE, IMAGE_FILE_NAME)
        ];

        var filesBytesArray = new byte[5][];
        for (int i = 0; i < filesBytesArray.Length; i++)
        {
            filesBytesArray[i] = await File.ReadAllBytesAsync(filePaths[i]);
        }

        Task<CloudinaryFile>[] uploadTasks =
        [
            cloudinaryService.UploadAsync(filesBytesArray[0], MimeTypeMap.GetMimeType(WORD_DOCUMENT_FILE_NAME), default),
            cloudinaryService.UploadAsync(filesBytesArray[1], MimeTypeMap.GetMimeType(EXCEL_WORKSHEET_FILE_NAME), default),
            cloudinaryService.UploadAsync(filesBytesArray[2], MimeTypeMap.GetMimeType(POWER_POINT_PRESENTATION_FILE_NAME), default),
            cloudinaryService.UploadAsync(filesBytesArray[3], MimeTypeMap.GetMimeType(POF_DOCUMENT_FILE_NAME), default),
            cloudinaryService.UploadAsync(filesBytesArray[4], MimeTypeMap.GetMimeType(IMAGE_FILE_NAME), default)
        ];
        await Task.WhenAll(uploadTasks);
        
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Cloudinary files uploaded and seeded successfully");
    }
}