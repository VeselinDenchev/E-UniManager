using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class ResourcesSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.Resources.AsNoTracking().AnyAsync()) return;

        Activity[] activities = await dbContext.Activities.ToArrayAsync();
        CloudinaryFile[] cloudinaryFiles = await dbContext.CloudinaryFiles.ToArrayAsync();
        CloudinaryFile wordDocument = cloudinaryFiles.First(f => f.Extension == ".docx");
        CloudinaryFile excelWorksheet = cloudinaryFiles.First(f => f.Extension == ".xlsx");
        CloudinaryFile powerPointPresentation = cloudinaryFiles.First(f => f.Extension == ".pptx");
        CloudinaryFile pdfDocument = cloudinaryFiles.First(f => f.Extension == ".pdf");
        CloudinaryFile image = cloudinaryFiles.First(f => f.Extension == ".bmp");
        
        List<Resource> resources = new();
        foreach (Activity activity in activities)
        {
            Resource noFileResource = new()
            {
                Title = "Ресурс без файл",
                Type = ResourceType.Info,
                Info = "Този ресурс няма файл",
                File = null,
                Activity = activity,
                Assignment = null
            };
            resources.Add(noFileResource);
            
            Resource wordDocumentResource = new()
            {
                Title = "Word документ",
                Type = ResourceType.Info,
                Info = "Този ресурс има Word документ",
                File = wordDocument,
                Activity = activity,
                Assignment = null
            };
            resources.Add(wordDocumentResource);
            
            Resource excelWorksheetResource = new()
            {
                Title = "Excel таблица",
                Type = ResourceType.Info,
                Info = "Този ресурс има Excel таблица",
                File = excelWorksheet,
                Activity = activity,
                Assignment = null
            };
            resources.Add(excelWorksheetResource);
            
            Resource powerPointPresentationResource = new()
            {
                Title = "PowerPoint презентация",
                Type = ResourceType.Info,
                Info = "Този ресурс има PowerPoint презентация",
                File = powerPointPresentation,
                Activity = activity,
                Assignment = null
            };
            resources.Add(powerPointPresentationResource);
            
            Resource pdfDocumentResource = new()
            {
                Title = "PDF документ",
                Type = ResourceType.Info,
                Info = "Този ресурс има PDF документ",
                File = pdfDocument,
                Activity = activity,
                Assignment = null
            };
            resources.Add(pdfDocumentResource);
            
            Resource imageResource = new()
            {
                Title = "Изображение",
                Type = ResourceType.Info,
                Info = "Този ресурс има изображение",
                File = image,
                Activity = activity,
                Assignment = null
            };
            resources.Add(imageResource);
            
            Resource firstAssignmentResource = new()
            {
                Type = ResourceType.Assignment,
                File = null,
                Activity = activity,
                Assignment = null // Will be seeded in AssignmentSeeder
            };
            resources.Add(firstAssignmentResource);
            
            Resource secondAssignmentResource = new()
            {
                Type = ResourceType.Assignment,
                File = null,
                Activity = activity,
                Assignment = null // Will be seeded in AssignmentSeeder
            };
            resources.Add(secondAssignmentResource);
        }

        await dbContext.Resources.AddRangeAsync(resources);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Resources seeded successfully");        
    }
}