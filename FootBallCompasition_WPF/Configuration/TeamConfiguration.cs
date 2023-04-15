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
    class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {

            builder
                    .HasOne(x => x.City)
                    .WithMany()
                    .HasForeignKey(x => x.IdCity);

        }

    }
}
