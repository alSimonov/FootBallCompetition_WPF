using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.Configuration
{
    class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {

        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.Ignore(x => x.FullName);

            builder
                    .HasOne(p => p.Role)
                    .WithMany()
                    .HasForeignKey(p => p.IdRole);

        }


    }
}
