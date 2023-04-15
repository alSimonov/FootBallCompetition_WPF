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
    class JudgingStaffConfigration : IEntityTypeConfiguration<JudgingStaff>
    {

        public void Configure(EntityTypeBuilder<JudgingStaff> builder)
        {

            builder
                .HasOne(x => x.Match)
                .WithMany()
                .HasForeignKey(x => x.IdMatch);

            builder
                .HasOne(x => x.Participant)
                .WithMany()
                .HasForeignKey(x => x.IdParticipant);

            builder
                .HasOne(x => x.AmpluaRole)
                .WithMany()
                .HasForeignKey(x => x.IdAmpluaRole);

        }
    }
}
