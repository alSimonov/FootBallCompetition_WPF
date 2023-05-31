using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootBallCompasition_WPF.Configuration
{
    class MatchConfiguration : IEntityTypeConfiguration<Match>
    {

        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder
                .HasOne(x => x.Season)
                .WithMany()
                .HasForeignKey(x => x.IdSeason);

            builder
                .HasOne(x => x.Team1)
                .WithMany()
                .HasForeignKey(x => x.IdTeam1);

            builder
                .HasOne(x => x.Team2)
                .WithMany()
                .HasForeignKey(x => x.IdTeam2);


            builder
                .HasOne(x => x.Stadium)
                .WithMany()
                .HasForeignKey(x => x.IdStadium);


            builder
                .HasOne(x => x.Stadium)
                .WithMany()
                .HasForeignKey(x => x.IdStadium);

            builder
                .HasOne(x => x.TypeOfMatch)
                .WithMany()
                .HasForeignKey(x => x.IdTypeOfMatch);



        }
    }
}
