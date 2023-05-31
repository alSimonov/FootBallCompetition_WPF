using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.Pages.pgsStadium
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

            
            loadStadium();

        }

        public void loadStadium()
        {
            
            stadiumList = _db.Stadiums.Where(x => x.Active == tglbtnActive.IsChecked)
                .Select(s => new StadiumShort()
            {
                Id = s.Id,
                StadiumName = s.Name,
                CityName = s.City.Name,
                Capacity = s.Capacity,
                TypeOfСoverageName = s.TypeOfСoverage.Name,
                TypeOfStadiumName = s.TypeOfStadium.Name,
                Active = s.Active,
            }).ToList();


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
                stadiumList = _db.Stadiums.Where(x => x.Active == tglbtnActive.IsChecked && x.Name.StartsWith(filtrby))
                .Select(s => new StadiumShort()
                {
                    Id = s.Id,
                    StadiumName = s.Name,
                    CityName = s.City.Name,
                    Capacity = s.Capacity,
                    TypeOfСoverageName = s.TypeOfСoverage.Name,
                    TypeOfStadiumName = s.TypeOfStadium.Name,
                    Active = s.Active,
                }).ToList();

                GridStadium.ItemsSource = stadiumList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(stadiumList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += stadiumList.Count().ToString();

            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new uscDialogStadiumAdd(0, true, this));
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridStadium.SelectedItem as StadiumShort).Id;

            Dialog.Show(new uscDialogStadiumAdd(id, false, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //int id = (GridStadium.SelectedItem as StadiumShort).Id;

            //Stadium stadium = _db.Stadiums.Find(id);

            //_db.Stadiums.Remove(stadium);
            //_db.SaveChanges();

            //loadStadium();

            //Growl.Success("Стадион успешно удален!");

            var dialog = new windowConfirmation("Изменение активности");
            if (dialog.ShowDialog() == true)
            {

                int id = (GridStadium.SelectedItem as StadiumShort).Id;
                Stadium stadium = _db.Stadiums.Find(id);

                stadium.Active = stadium.Active ? false : true;

                _db.Entry(stadium).State = EntityState.Modified;
                _db.SaveChanges();

                loadStadium();

                Growl.Warning("Состояние активности стадиона изменено!");

            }


        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadStadium(tbFilter.Text.Trim());
        }


        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadStadium();
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridStadium.ItemsSource = stadiumList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }




    }
}
