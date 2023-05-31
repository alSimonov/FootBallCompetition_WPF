using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootBallCompasition_WPF.Configuration
{
    class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {

        public void Configure(EntityTypeBuilder<Stadium> builder)
        {


            builder.Ignore(x => x.StadiumAndCityName);

            builder
                    .HasOne(x => x.TypeOfСoverage)
                    .WithMany()
                    .HasForeignKey(x => x.IdTypeOfСoverage);

            builder
                    .HasOne(x => x.TypeOfStadium)
                    .WithMany()
                    .HasForeignKey(x => x.IdTypeOfStadium);

            builder
                    .HasOne(x => x.City)
                    .WithMany()
                    .HasForeignKey(x => x.IdCity);




        }

    }
}
