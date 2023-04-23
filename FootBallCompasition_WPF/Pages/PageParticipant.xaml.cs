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

namespace FootBallCompasition_WPF
{
    /// <summary>
    /// Логика взаимодействия для ParticipantPage.xaml
    /// </summary>
    public partial class PageParticipant : Page
    {
        public MainDBContext? _db;

        //public record class PartRole(int id, string last, string first, DateTime date, string NameRole);

        List<ParticipantShort> partList = new List<ParticipantShort>();


        public PageParticipant(string whereStr)
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



            loadPart(whereStr);
            //pagPart.MaxPageCount = (int) Math.Ceiling(partList.Count / 10.0);
        
            
        
        }


        private void loadPart(string whereStr)
        {
            
            partList = _db.Participants.Where(s => s.Role.Name == whereStr).
                Select(s =>
                new ParticipantShort()
                {
                    //Id = s.Id, 
                    Surname = s.Surname, 
                    Name = s.Name, 
                    Patronymic = s.Patronymic,
                    DateOfBirth = s.DateOfBirth.ToString("D"), 
                    Telephone = s.Telephone
                }).ToList();


            //GridPart.ItemsSource = partList;



        }


        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            //GridPart.ItemsSource = partList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }


    }
}
