using EUniManager.Domain.Entities.Students;
using EUniManager.Persistence.Configurations.EntityTypes.Base;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static EUniManager.Persistence.Constants.Entities.Students.IdCardConstant;

namespace EUniManager.Persistence.Configurations.EntityTypes.Students;

public sealed class IdCardConfiguration : BaseEntityConfiguration<IdCard, Guid>
{
    public override void Configure(EntityTypeBuilder<IdCard> entity)
    {
        base.Configure(entity);

        entity.Property(ic => ic.IdNumber).IsRequired()
                                          .HasMaxLength(ID_CARD_ID_NUMBER_MAX_STRING_LENGTH)
                                          .IsUnicode(false);

        entity.Property(ic => ic.DateIssued).IsRequired();
    }
}