using FootBallCompasition_WPF.FootballClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.Configuration
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasOne(x => x.Participant)
                .WithMany()
                .HasForeignKey(x => x.IdParticipant);

            builder
                .HasOne(x => x.AccountRole)
                .WithMany()
                .HasForeignKey(x => x.IdAccountRole);



        }
    }
}
