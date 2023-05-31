using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
