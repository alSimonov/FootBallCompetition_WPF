using FootBallCompasition_WPF.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF
{
    static class dbConfiguration
    {
        private static IServiceCollection services { get; set; }
        public static IServiceProvider Services { get; set; }


        public static void ConfigureServices()
        {
            services = new ServiceCollection();
            services.AddDbContext<MainDBContext>(options =>
            options.UseSqlServer("Data Source=DESKTOP-IBJCCC1;Initial Catalog=FootballCompetitions;Trusted_Connection=True;Encrypt=False;"));

            Services = services.BuildServiceProvider();

        }


    }
}
