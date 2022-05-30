using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Dal.Configurations
{

    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {

        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.OwnsOne(up => up.BasicInfo);
        }
    }
}