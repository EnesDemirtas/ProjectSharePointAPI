using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Dal.Configurations {

    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo> {

        public void Configure(EntityTypeBuilder<BasicInfo> builder) {
        }
    }
}