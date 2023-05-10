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
    class EventConfiguration : IEntityTypeConfiguration<Event>
    {

        public void Configure(EntityTypeBuilder<Event> builder)
        {

            builder
                .HasOne(x => x.Match)
                .WithMany()
                .HasForeignKey(x => x.IdMatch);

            builder
                .HasOne(x => x.TeamComposition)
                .WithMany()
                .HasForeignKey(x => x.IdTeamComposition);

            builder
                .HasOne(x => x.TypeOfEvent)
                .WithMany()
                .HasForeignKey(x => x.IdTypeOfEvent);


        }


    }
}
