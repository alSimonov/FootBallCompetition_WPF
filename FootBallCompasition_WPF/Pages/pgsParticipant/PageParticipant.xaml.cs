using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.UserControls;
using FootBallCompasition_WPF.UserControls.fUscTeamComposition;
using FootBallCompasition_WPF.UserControls.ucsMatch;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
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

            List<Participant> tempPart;

            if(_idRole == null)
            {
                tempPart = _db.Participants.Where(x => x.Active == tglbtnActive.IsChecked).Include(x => x.Role).ToList();
            }
            else
            {
                tempPart = _db.Participants.Where(s => s.Active == tglbtnActive.IsChecked && s.IdRole == _idRole).Include(x => x.Role).ToList();   
            }

            partList = tempPart.Select(s => new ParticipantShort()
             {
                 Id = s.Id,
                 FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                 DateOfBirth = s.DateOfBirth.ToString("D"),
                 Telephone = s.Telephone,
                 RoleName = s.Role.Name,
                 Active = s.Active,
             }).ToList();



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



                List<Participant> tempPart;

                if (_idRole == null)
                {
                    tempPart = _db.Participants.Where(x => x.Active == tglbtnActive.IsChecked && x.Surname.StartsWith(filtrby)).Include(x => x.Role).ToList();
                }
                else
                {
                    tempPart = _db.Participants.Where(s => s.Active == tglbtnActive.IsChecked && s.IdRole == _idRole && s.Surname.StartsWith(filtrby)).Include(x => x.Role).ToList();
                }

                partList = tempPart.Select(s => new ParticipantShort()
                {
                    Id = s.Id,
                    FIO = $"{s.Surname} {s.Name} {s.Patronymic}",
                    DateOfBirth = s.DateOfBirth.ToString("D"),
                    Telephone = s.Telephone,
                    RoleName = s.Role.Name,
                    Active = s.Active,
                }).ToList();


              
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
            //int id = (GridPart.SelectedItem as ParticipantShort).Id;

            //Participant participant = _db.Participants.Find(id);

            //_db.Participants.Remove(participant);
            //_db.SaveChanges();

            //loadPart();
            //Growl.Success("Участник успешно удален!");

            var dialog = new windowConfirmation("Изменение активности");
            if (dialog.ShowDialog() == true)
            {

                int id = (GridPart.SelectedItem as ParticipantShort).Id;

                Participant participant = _db.Participants.Find(id);

                participant.Active = participant.Active ? false : true;

           
                _db.Entry(participant).State = EntityState.Modified;
                _db.SaveChanges();

                loadPart();
                Growl.Warning("Состояние активности участника изменено!");
            
            }


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

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadPart();
        }
    }
}
