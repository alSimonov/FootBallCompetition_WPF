using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootBallCompasition_WPF.Configuration
{
    class TeamCompositionConfiguration : IEntityTypeConfiguration<TeamComposition>
    {

        public void Configure(EntityTypeBuilder<TeamComposition> builder)
        {
            builder.Ignore(x=>x.FullNameAndNumPlayer);

            builder
                .HasOne(x => x.Team)
                .WithMany()
                .HasForeignKey(x => x.IdTeam);

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
