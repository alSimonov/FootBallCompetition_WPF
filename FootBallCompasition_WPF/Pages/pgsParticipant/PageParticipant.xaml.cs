using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.Pages.pgsParticipant
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


            btnTabBtnPlayer.Click += btnTabBtnPlayer_Click;
            btnTabBtnReferee.Click += btnTabBtnReferee_Click;

            //tbFilter.KeyDown += KeyEventHandler(tbFilter_KeyDown);


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();



            loadPart(whereStr);
            
        }

        private void btnTabBtnPlayer_Click(object sender, RoutedEventArgs e)
        {
            loadPart("Игрок");
        }

        private void btnTabBtnReferee_Click(object sender, RoutedEventArgs e)
        {
            loadPart("Судья");

        }

        private void loadPart(string whereStr)
        {

            partList = _db.Participants.Where(s => s.Role.Name == whereStr).
                Select(s =>
                new ParticipantShort()
                {
                    Id = s.Id,
                    //Surname = s.Surname, 
                    //Name = s.Name, 
                    //Patronymic = s.Patronymic,
                    FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                    DateOfBirth = s.DateOfBirth.ToString("D"),
                    Telephone = s.Telephone
                }).ToList();


            //GridPart.ItemsSource = partList;

            pagPart.MaxPageCount = (int)Math.Ceiling(partList.Count / 10.0);


            GridPart.ItemsSource = partList.Take(10).ToList();

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += partList.Count().ToString();



        }
        
        
        
        
        private void loadPart(string whereStr, string filtrby)
        {

            if (filtrby == "")
            {
                loadPart(whereStr);

            }
            else
            {

                //string[] strings = filtrby.Split(" ", StringSplitOptions.RemoveEmptyEntries);


                partList = _db.Participants.Where( x => x.Role.Name == whereStr && x.Surname.StartsWith(filtrby) ).
                //            .Where(x => x.FirstName.Contains(firstName))
                Select(s =>
                new ParticipantShort()
                {
                    Id = s.Id,
                    //Surname = s.Surname, 
                    //Name = s.Name, 
                    //Patronymic = s.Patronymic,
                    FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                    DateOfBirth = s.DateOfBirth.ToString("D"),
                    Telephone = s.Telephone
                }).ToList();


            //GridPart.ItemsSource = partList;

            pagPart.MaxPageCount = (int)Math.Ceiling(partList.Count / 10.0);


            GridPart.ItemsSource = partList.Take(10).ToList();

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += partList.Count().ToString();



            }




        }


        //private void loadEmpl(string firstName)
        //{
        //    if (firstName == "")
        //    {
        //        loadEmpl();

        //    }
        //    else
        //    {
        //        emplList = _db.Employees
        //            .Where(x => x.FirstName.Contains(firstName))
        //            .Select(s => new EmployeeShort()
        //            {
        //                Id = s.Id,
        //                FirstName = s.FirstName,
        //                LastName = s.LastName,
        //                Patronymic = s.Patronymic,
        //                DateOfBirth = s.DateOfBirth,
        //                DateStartContract = s.DateStartContract,
        //                DateEndContract = s.DateEndContract,
        //                LastQualification = s.LastQualification,
        //                PostName = s.Post.Name,
        //                GenderName = s.Gender.LongName
        //            })
        //            .ToList();

        //        loadDataGrid();

        //    }
        //}



        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridPart.ItemsSource = partList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }

       
        private void btnPartAdd_Click(object sender, RoutedEventArgs e)
        {

            Dialog.Show(new UsControlDialogPartAdd(0, true));

            

            loadPart("Игрок");

        }




        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridPart.SelectedItem as ParticipantShort).Id;

            Participant participant = _db.Participants.Find(id);
            
            _db.Participants.Remove(participant);
            _db.SaveChanges();

            loadPart("Игрок");

            Growl.Success("Участник успешно удален!");


        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {

            int id = (GridPart.SelectedItem as ParticipantShort).Id;


            Dialog.Show(new UsControlDialogPartAdd(id, false));

            loadPart("Игрок");

        }

        //private void tbFilter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        //{

        //    if (e.Key == Key.Enter)
        //    {
        //        loadPart("Игрок", tbFilter.Text.Trim());
        //    }
        //}

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadPart("Игрок", tbFilter.Text.Trim());
        }
    }
}
