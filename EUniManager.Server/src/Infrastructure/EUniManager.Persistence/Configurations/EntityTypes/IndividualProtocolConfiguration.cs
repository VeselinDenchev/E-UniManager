using EUniManager.Domain.Entities;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.IndividualProtocolConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes;

public sealed class IndividualProtocolConfiguration: BaseEntityConfiguration<IndividualProtocol, Guid>
{
    public override void Configure(EntityTypeBuilder<IndividualProtocol> entity)
    {
        base.Configure(entity);

        entity.HasOne(ip => ip.Subject);

        entity.Property(ip => ip.Status).IsRequired()
                                        .HasConversion<string>()
                                        .IsUnicode(false)
                                        .HasMaxLength(STATUS_MAX_STRING_LENGTH);
    }
}