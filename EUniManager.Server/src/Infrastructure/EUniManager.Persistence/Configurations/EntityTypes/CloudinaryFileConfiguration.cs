using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.CloudinaryFileConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class CloudinaryFileConfiguration : BaseEntityConfiguration<CloudinaryFile, string>
{
    public override void Configure(EntityTypeBuilder<CloudinaryFile> entity)
    {
        base.Configure(entity);

        entity.Property(f => f.Id).IsUnicode(false)
                                  .HasMaxLength(ID_MAX_STRING_LENGTH);
        
        
        entity.Property(f => f.Extension).IsRequired()
                                         .IsUnicode(false)
                                         .HasMaxLength(EXTENSION_MAX_STRING_LENGTH);

        entity.Property(f => f.Version).IsRequired();
    }
}