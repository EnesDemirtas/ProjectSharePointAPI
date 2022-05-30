using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Dal.Configurations
{

    internal class ProjectInteractionConfig : IEntityTypeConfiguration<ProjectInteraction>
    {

        public void Configure(EntityTypeBuilder<ProjectInteraction> builder)
        {
            builder.HasKey(pi => pi.InteractionId);
        }
    }
}