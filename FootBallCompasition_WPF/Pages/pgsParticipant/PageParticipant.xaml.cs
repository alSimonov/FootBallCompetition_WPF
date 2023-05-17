using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.fUscTeamComposition;
using FootBallCompasition_WPF.UserControls.ucsMatch;
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

        byte? _idRole;

        //public record class PartRole(int id, string last, string first, DateTime date, string NameRole);

        List<ParticipantShort> partList = new List<ParticipantShort>();


        public PageParticipant()
        {
            InitializeComponent();

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            cbRole.ItemsSource = _db.Roles.ToList();
            cbRole.SelectedValuePath = "Id";
            cbRole.DisplayMemberPath = "Name";

            loadPart();            
        }

        public void loadPart()
        {

            if(_idRole == null)
            {
                partList = _db.Participants
                    .Select(s => new ParticipantShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                        DateOfBirth = s.DateOfBirth.ToString("D"),
                        Telephone = s.Telephone,
                        RoleName = s.Role.Name,
                    }).ToList();

            }
            else
            {
                partList = _db.Participants.Where(s => s.IdRole == _idRole).
                    Select(s =>
                    new ParticipantShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                        DateOfBirth = s.DateOfBirth.ToString("D"),
                        Telephone = s.Telephone,
                        RoleName = s.Role.Name,
                    }).ToList();

            }

            //GridPart.ItemsSource = partList;

            pagPart.MaxPageCount = (int)Math.Ceiling(partList.Count / 10.0);


            GridPart.ItemsSource = partList.Take(10).ToList();

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += partList.Count().ToString();

        }
        
        
        
        
        private void loadPart(string filtrby)
        {

            if (filtrby == "")
            {
                loadPart();

            }
            else
            {

                //string[] strings = filtrby.Split(" ", StringSplitOptions.RemoveEmptyEntries);


                if (_idRole == null)
                {

                    partList = _db.Participants.Where(x => x.Surname.StartsWith(filtrby)).
                    Select(s =>
                    new ParticipantShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                        DateOfBirth = s.DateOfBirth.ToString("D"),
                        Telephone = s.Telephone,
                        RoleName = s.Role.Name,
                    }).ToList();


                }
                else
                {


                    partList = _db.Participants.Where(x => x.IdRole == _idRole && x.Surname.StartsWith(filtrby)).
                    Select(s =>
                    new ParticipantShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                        DateOfBirth = s.DateOfBirth.ToString("D"),
                        Telephone = s.Telephone,
                        RoleName = s.Role.Name,
                    }).ToList();


                }

              
                pagPart.MaxPageCount = (int)Math.Ceiling(partList.Count / 10.0);


                GridPart.ItemsSource = partList.Take(10).ToList();

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += partList.Count().ToString();

            }


        }


        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridPart.ItemsSource = partList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }

       
        private void btnPartAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new UsControlDialogPartAdd(0, true,  this)); 
        }




        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridPart.SelectedItem as ParticipantShort).Id;

            Participant participant = _db.Participants.Find(id);
            
            _db.Participants.Remove(participant);
            _db.SaveChanges();

            loadPart();
            Growl.Success("Участник успешно удален!");
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridPart.SelectedItem as ParticipantShort).Id;
         
            Dialog.Show(new UsControlDialogPartAdd(id, false, this));
        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadPart(tbFilter.Text.Trim());
        }

        private void btnTabBtnTeamComposition_Click(object sender, RoutedEventArgs e)
        {


            if (GridPart.SelectedItem == null && (GridPart.SelectedItem as ParticipantShort).RoleName != "Игрок")
            {
                Growl.Error("Игрок не был выбран!");
            }
            else
            {
                Dialog.Show(new uscTeamComposition((ParticipantShort)GridPart.SelectedItem));
            }
        }

        private void cbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _idRole = cbRole.SelectedValue == null ? null : (cbRole.SelectedItem as Role).Id;
            loadPart();
        }
    }
}
