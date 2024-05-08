using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using EUniManager.Application.Mappers;
using EUniManager.Application.Models.Cloudinary.Interfaces;
using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using PommaLabs.MimeTypes;

using static EUniManager.Application.Constants.EnviromentalVariablesConstant;

namespace EUniManager.Application.Services;

public sealed class CloudinaryService : ICloudinaryService
{
    private const string CLOUDINARY_CLOUD_NAME_ENV_VARIABLE_NAME = "CLOUDINARY_CLOUD_NAME";
    private const string CLOUDINARY_API_KEY_ENV_VARIABLE_NAME = "CLOUDINARY_API_KEY";
    private const string CLOUDINARY_API_SECRET_ENV_VARIABLE_NAME = "CLOUDINARY_API_SECRET";
    
    // Without dots
    private readonly string[] imageFileExtensionsSupportedByCloudinary =
    [
        MimeTypeMap.Extensions.JPG.Substring(1),
        MimeTypeMap.Extensions.PNG.Substring(1),
        MimeTypeMap.Extensions.GIF.Substring(1),
        MimeTypeMap.Extensions.BMP.Substring(1),
        MimeTypeMap.Extensions.TIFF.Substring(1),
        MimeTypeMap.Extensions.ICO.Substring(1),
        MimeTypeMap.Extensions.PDF.Substring(1),
        MimeTypeMap.Extensions.EPS.Substring(1),
        MimeTypeMap.Extensions.PSD.Substring(1),
        MimeTypeMap.Extensions.SVG.Substring(1),
        MimeTypeMap.Extensions.WEBP.Substring(1),
        MimeTypeMap.Extensions.WDP.Substring(1),
    ];

    private readonly IEUniManagerDbContext _dbContext;
    private readonly DbSet<CloudinaryFile> _dbSet;
    private readonly CloudinaryFileMapper _mapper = new();
    private readonly Account _cloudinaryAccount;
    private readonly Cloudinary _cloudinary;
    
    public CloudinaryService(IEUniManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<CloudinaryFile>();
        
        // Set Cloudinary account
        string canNotLoadEnvVariableMessage =
            string.Format(CAN_NOT_LOAD_ENV_VARIABLE_MESSAGE_TEMPLATE, CLOUDINARY_CLOUD_NAME_ENV_VARIABLE_NAME);
        string cloudinaryCloudName = Environment.GetEnvironmentVariable(CLOUDINARY_CLOUD_NAME_ENV_VARIABLE_NAME) ?? 
                                     throw new ArgumentNullException(canNotLoadEnvVariableMessage);

        canNotLoadEnvVariableMessage = canNotLoadEnvVariableMessage.Replace(CLOUDINARY_CLOUD_NAME_ENV_VARIABLE_NAME,
            CLOUDINARY_API_KEY_ENV_VARIABLE_NAME);
        
        string cloudinaryApiKey = Environment.GetEnvironmentVariable(CLOUDINARY_API_KEY_ENV_VARIABLE_NAME) ??
                                  throw new ArgumentNullException(canNotLoadEnvVariableMessage);
        
        canNotLoadEnvVariableMessage = canNotLoadEnvVariableMessage.Replace(CLOUDINARY_API_KEY_ENV_VARIABLE_NAME,
            CLOUDINARY_API_SECRET_ENV_VARIABLE_NAME);
        
        string cloudinaryApiSecret = Environment.GetEnvironmentVariable(CLOUDINARY_API_SECRET_ENV_VARIABLE_NAME) ?? 
                                     throw new ArgumentNullException(canNotLoadEnvVariableMessage);

        _cloudinaryAccount = new Account(cloudinaryCloudName, cloudinaryApiKey, cloudinaryApiSecret);
        _cloudinary = new Cloudinary(_cloudinaryAccount);
        _cloudinary.Api.Secure = true;
    }
    

    public async Task<string> UploadAsync(byte[] fileBytes, string mimeType, CancellationToken cancellationToken)
    {
        string fileExtension = MimeTypeMap.GetExtensionWithoutDot(mimeType);
        string publicId = Guid.NewGuid().ToString();
        string fileName = $"{publicId}.{fileExtension}";

        bool willBeUploadedLikeImage = mimeType.Contains("image") || fileExtension == "pdf";
        RawUploadParams uploadParams = willBeUploadedLikeImage ? new ImageUploadParams() : new RawUploadParams();
        
        await using MemoryStream stream = new(fileBytes);
        
        SetBaseUploadParams(uploadParams, fileName, stream, publicId);
            
        RawUploadResult? uploadResult = await UploadDependingOnParamsTypeAsync(uploadParams, cancellationToken);
        ValidateUploadResult(uploadResult);

        CloudinaryFile cloudinaryFile = _mapper.Map(uploadResult!);
        cloudinaryFile.Extension = fileExtension;

        try
        {
            await _dbSet.AddAsync(cloudinaryFile, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await DeleteAsync(publicId);
            
            throw;
        }

        return publicId;
    }

    public async Task UpdateAsync(string publicId, 
                                  byte[] newFileBytes,
                                  string newMimeType,
                                  CancellationToken cancellationToken)
    {
        await ValidateFileExistenceInDatabaseAndCloudinaryAsync(publicId, cancellationToken);
        
        string fileExtension = MimeTypeMap.GetExtensionWithoutDot(newMimeType);
        string fileName = $"{publicId}.{fileExtension}";

        bool willBeUploadedLikeImage = newMimeType.Contains("image") || fileExtension == "pdf";
        RawUploadParams uploadParams = willBeUploadedLikeImage ? new ImageUploadParams() : new RawUploadParams();
        
        await using MemoryStream stream = new(newFileBytes);
        
        SetBaseUploadParams(uploadParams, fileName, stream, publicId);
        uploadParams.Overwrite = true;
        uploadParams.Invalidate = true;
            
        RawUploadResult? uploadResult = await UploadDependingOnParamsTypeAsync(uploadParams, cancellationToken);
        ValidateUploadResult(uploadResult);

        CloudinaryFile cloudinaryFile = _mapper.Map(uploadResult!);
        cloudinaryFile.Extension = fileExtension;

        try
        {
            _dbSet.Update(cloudinaryFile);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await DeleteAsync(publicId);
            
            throw;
        }
    }

    public async Task<(byte[] fileBytes, string mimeType)> DownloadAsync(string publicId, 
                                                                         CancellationToken cancellationToken)
    {
        string? fileExtension = await GetFileExtensionAsync(publicId, cancellationToken);
        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            throw new ArgumentException("File is not found or has missing extension!");
        }
        
        GetResourceResult? result = await GetResourceResultAsync(publicId, fileExtension);
        bool existsInCloudinary = result is not null;
        if (!existsInCloudinary) throw new FileNotFoundException("File is not found!");

        if (string.IsNullOrWhiteSpace(result!.SecureUrl) && string.IsNullOrWhiteSpace(result.Url))
        {
            throw new ArgumentException("Missing URL!");
        }

        byte[] fileBytes = await GetFileBytesFromCloudinaryAsync(result.SecureUrl ?? result.Url);

        if (!fileBytes.Any()) throw new ArgumentException("File is empty!");

        string mimeType = MimeTypeMap.GetMimeType($".{fileExtension}");

        return (fileBytes, mimeType);
    }

    private async Task<byte[]> GetFileBytesFromCloudinaryAsync(string url)
    {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        await using Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
        using MemoryStream memoryStream = new();
        
        await streamToReadFrom.CopyToAsync(memoryStream);
        
        return memoryStream.ToArray();
    }

    private async Task DeleteAsync(string publicId)
    {
        await ValidateFileExistenceInDatabaseAndCloudinaryAsync(publicId);
        
        DeletionParams deletionParams = new(publicId);
        DeletionResult deletionResult = await _cloudinary.DestroyAsync(deletionParams);
        
        if (deletionResult.Result != "ok") throw new Exception("Unable to delete file!");
        
        CloudinaryFile? deletedFile = await _dbSet.FindAsync(publicId);
        ArgumentNullException.ThrowIfNull(deletedFile);
        
        _dbSet.Remove(deletedFile);
        // If image was deleted from Cloudinary it should be deleted from the database also no matter if request
        // was cancelled or not
        _dbContext.SaveChangesAsync();
    }
    
    private async Task ValidateFileExistenceInDatabaseAndCloudinaryAsync
    (
        string publicId, 
        CancellationToken cancellationToken = default
    )
    {
        string? fileExtension = await GetFileExtensionAsync(publicId, cancellationToken);
        if (string.IsNullOrWhiteSpace(fileExtension))
        {
            throw new ArgumentException("File is not found or has missing extension!");
        }
        
        GetResourceResult? result = await GetResourceResultAsync(publicId, fileExtension);
        bool existsInCloudinary = result is not null;
        if (!existsInCloudinary) throw new FileNotFoundException("File is not found!");
    }

    private async Task<string?> GetFileExtensionAsync(string id, CancellationToken cancellationToken)
    {
        string? fileExtension = await _dbSet.AsNoTracking()
                                            .Where(f => f.Id == id)
                                            .Select(f => f.Extension)
                                            .FirstOrDefaultAsync(cancellationToken);

        return fileExtension;
    }

    private async Task<GetResourceResult?> GetResourceResultAsync(string id, string fileExtension)
    {
        string publicId = id;
        ResourceType resourceType = ResourceType.Image;
        if (!imageFileExtensionsSupportedByCloudinary.Contains(fileExtension))
        {
            publicId = $"{id}.{fileExtension}";
            resourceType = ResourceType.Raw;
        }
        
        GetResourceParams parameters = new(publicId)
        {
            ResourceType = resourceType
        };
        GetResourceResult result = await _cloudinary.GetResourceAsync(parameters);

        return result;
    }

    private void SetBaseUploadParams(RawUploadParams uploadParams, 
                                     string fileName, 
                                     Stream stream, 
                                     string publicId)
    {
        uploadParams.File = new FileDescription(fileName, stream);
        uploadParams.AssetFolder = nameof(EUniManager);
        uploadParams.UniqueFilename = false;
        uploadParams.UseFilename = true;
        uploadParams.UseAssetFolderAsPublicIdPrefix = false;
        uploadParams.PublicId = publicId;
    }

    private async Task<RawUploadResult?> UploadDependingOnParamsTypeAsync(RawUploadParams uploadParams,
                                                                          CancellationToken cancellationToken)
    {
        RawUploadResult? uploadResult;
        if (uploadParams is ImageUploadParams imageUploadParams)
        {
            uploadResult = await _cloudinary.UploadAsync(imageUploadParams, cancellationToken);
            
        }
        else
        {
            uploadResult = await _cloudinary.UploadAsync(uploadParams, type: "raw", cancellationToken);
        }

        return uploadResult;
    }

    private void ValidateUploadResult(RawUploadResult? uploadResult)
    {
        if (uploadResult is null || uploadResult.Error is not null)
        {
            throw new InvalidOperationException($"Upload failed! {uploadResult?.Error}");
        }
    }
}
