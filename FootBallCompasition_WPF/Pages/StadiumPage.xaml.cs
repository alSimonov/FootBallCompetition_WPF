using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Short;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FootBallCompasition_WPF.MainWindow;

namespace FootBallCompasition_WPF
{
    /// <summary>
    /// Логика взаимодействия для StadiumPage.xaml
    /// </summary>
    public partial class StadiumPage : Page
    {


        public record class PartRole(int id, string last, string first, DateTime date, string NameRole);
        private MainDBContext? _db;

        List<StadiumShort> stadiumList = new List<StadiumShort>();


        public StadiumPage()
        {

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            //Text1.Click += Text1_Click;

            loadStadium();

        }

        private void loadStadium()
        {

            stadiumList = _db.Stadiums.Select(s => new StadiumShort()
                {
                    //Id = s.Id,
                    StadiumName = s.Name,
                    CityName = s.City.Name,
                    Capacity = s.Capacity,
                    TypeOfСoverageName = s.TypeOfСoverage.Name,
                    TypeOfStadiumName = s.TypeOfStadium.Name
                }).ToList();

            GridStadium.ItemsSource = stadiumList;


        }



    }
}
