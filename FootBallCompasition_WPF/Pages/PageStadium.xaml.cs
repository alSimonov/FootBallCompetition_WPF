using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
    public partial class PageStadium : Page
    {


        public record class PartRole(int id, string last, string first, DateTime date, string NameRole);
        private MainDBContext? _db;

        List<StadiumShort> stadiumList = new List<StadiumShort>();


        public PageStadium()
        {
            InitializeComponent();


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            //Text1.Click += Text1_Click;

            loadStadium();

        }

        private void loadStadium()
        {
            //TODO разобраться почему не выводит TypeOfСoverage

            stadiumList = _db.Stadiums.Select(s => new StadiumShort()
                {
                    Id = s.Id,
                    StadiumName = s.Name,
                    CityName = s.City.Name,
                    Capacity = s.Capacity,
                    //TypeOfСoverageName = s.TypeOfСoverage.Name,
                    //TypeOfStadiumName = s.TypeOfStadium.Name
            }).ToList();

            //GridStadium.ItemsSource = stadiumList;



            GridStadium.ItemsSource = stadiumList.Take(10).ToList();

            pagGrid.MaxPageCount = (int)Math.Ceiling(stadiumList.Count / 10.0);

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += stadiumList.Count().ToString();



        }

  private void loadStadium(string filtrby)
        {

            if (filtrby == "")
            {
                loadStadium();

            }
            else
            {
                stadiumList = _db.Stadiums.Where(x => x.Name.Contains(filtrby))
                .Select(s => new StadiumShort()
                {
                    Id = s.Id,
                    StadiumName = s.Name,
                    CityName = s.City.Name,
                    Capacity = s.Capacity,
                    //TypeOfСoverageName = s.TypeOfСoverage.Name,
                    //TypeOfStadiumName = s.TypeOfStadium.Name
                }).ToList();

                GridStadium.ItemsSource = stadiumList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(stadiumList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += stadiumList.Count().ToString();

            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //Dialog.Show(new uscDialogMatchAdd(0, true));

            loadStadium();


        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

            int id = (GridStadium.SelectedItem as StadiumShort).Id;


            //Dialog.Show(new uscDialogMatchAdd(id, false));

            loadStadium();


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridStadium.SelectedItem as StadiumShort).Id;

            Stadium stadium = _db.Stadiums.Find(id);

            _db.Stadiums.Remove(stadium);
            _db.SaveChanges();

            loadStadium();

            Growl.Success("Стадион успешно удален!");

        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadStadium(tbFilter.Text.Trim());
        }

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadStadium();
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridStadium.ItemsSource = stadiumList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }




    }
}
