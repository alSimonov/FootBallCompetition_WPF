using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Pages.pgsStadium;
using HandyControl.Controls;
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

namespace FootBallCompasition_WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для uscDialogStadiumAdd.xaml
    /// </summary>
    public partial class uscDialogStadiumAdd : UserControl
    {


        public MainDBContext? _db;

        PageStadium _pageStadium;

        FootballClass.Stadium _stadium;

        int _idP;
        bool _addOrModify;



        public uscDialogStadiumAdd(int idP, bool addOrModify, PageStadium pageStadium)
        {
            InitializeComponent();


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _pageStadium = pageStadium;

            cbCity.ItemsSource = _db.Cities.ToList();
            cbCity.SelectedValuePath = "Id";
            cbCity.DisplayMemberPath = "Name";


            cbTypeOfCoverage.ItemsSource = _db.TypeOfCoverages.ToList();
            cbTypeOfCoverage.SelectedValuePath = "Id";
            cbTypeOfCoverage.DisplayMemberPath = "Name";

            cbTypeOfStadium.ItemsSource = _db.TypeOfStadiums.ToList();
            cbTypeOfStadium.SelectedValuePath = "Id";
            cbTypeOfStadium.DisplayMemberPath = "Name";



            _idP = idP;
            _addOrModify = addOrModify;



            if (!addOrModify)
            {
                _stadium = _db.Stadiums.Find(idP);

                tbStadiumName.Text = _stadium.Name;
                cbCity.SelectedItem = _stadium.City;
                tbCapacity.Text = _stadium.Capacity.ToString();
                cbTypeOfCoverage.SelectedItem = _stadium.TypeOfСoverage;
                cbTypeOfStadium.SelectedItem = _stadium.TypeOfStadium;


            }

        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_addOrModify)
            {
                FootballClass.Stadium stadium = new FootballClass.Stadium();

                stadium.Name = tbStadiumName.Text;
                stadium.City = (City)cbCity.SelectedItem;
                
                
                if (int.TryParse(tbCapacity.Text, out int capacity))
                {
                    stadium.Capacity =  capacity;
                }
                else
                {
                    Growl.Error("Вместимость ?????");
                }

                stadium.TypeOfСoverage = (TypeOfСoverage)cbTypeOfCoverage.SelectedItem;
                stadium.TypeOfStadium = (TypeOfStadium)cbTypeOfStadium.SelectedItem;

                _db.Stadiums.Add(stadium);
                _db.SaveChanges();
                _pageStadium.loadStadium();

                Growl.Success("Стадион успешно добавлен!");

            }
            else if (!_addOrModify)
            {

                _stadium.Name = tbStadiumName.Text;
                _stadium.City = (City)cbCity.SelectedItem;


                if (int.TryParse(tbCapacity.Text, out int capacity))
                {
                    _stadium.Capacity = capacity;
                }
                else
                {
                    Growl.Error("Вместимость ?????");
                }

                _stadium.TypeOfСoverage = (TypeOfСoverage)cbTypeOfCoverage.SelectedItem;
                _stadium.TypeOfStadium = (TypeOfStadium)cbTypeOfStadium.SelectedItem;


                _db.Entry(_stadium).State = EntityState.Modified;

                _db.SaveChanges();

                _pageStadium.loadStadium();

                Growl.Success("Стадион успешно изменен!");

            }


        }
    }
}
