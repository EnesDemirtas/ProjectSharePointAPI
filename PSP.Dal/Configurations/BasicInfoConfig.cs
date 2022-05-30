using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Dal.Configurations
{

    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
    {

        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
        }
    }
}