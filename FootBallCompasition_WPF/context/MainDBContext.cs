using Microsoft.EntityFrameworkCore;
using FootBallCompasition_WPF.FootballClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootBallCompasition_WPF.Configuration;

namespace FootBallCompasition_WPF.context
{
    public class MainDBContext : DbContext
    {
        public DbSet<AmpluaRole> AmpluaRoles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<JudgingStaff> JudgingStaffs { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamComposition> TeamCompositions { get; set; }
        public DbSet<TypeOfEvent> TypeOfEvents { get; set; }
        public DbSet<TypeOfMatch> TypeOfMatches { get; set; }
        public DbSet<TypeOfStadium> TypeOfStadiums{ get; set; }
        public DbSet<TypeOfСoverage> TypeOfCoverages { get; set; }

        public MainDBContext(DbContextOptions<MainDBContext> options) :base(options)
        {
            //this.Stadiums.s
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-IBJCCC1;Initial Catalog=FootballCompetitions;" +
        //    "Trusted_Connection=True;Encrypt=False;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Participant>().ToTable("Participant");


            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new JudgingStaffConfigration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());
            
            modelBuilder.ApplyConfiguration(new ParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new StadiumConfiguration());
            modelBuilder.ApplyConfiguration(new TeamCompositionConfiguration());

            modelBuilder.ApplyConfiguration(new TeamConfiguration());




        }
        //DESKTOP-IBJCCC1\lenovo
        //[FootballCompetitions]



    }
}
