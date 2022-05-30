using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Dal.Configurations
{

    internal class ProjectCommentConfig : IEntityTypeConfiguration<ProjectComment>
    {

        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.HasKey(pc => pc.CommentId);
        }
    }
}