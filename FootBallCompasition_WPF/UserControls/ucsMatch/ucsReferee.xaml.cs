﻿using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using FootBallCompasition_WPF.Short;
using FootBallCompasition_WPF.Windows;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.UserControls.ucsMatch
{
    /// <summary>
    /// Логика взаимодействия для ucsReferee.xaml
    /// </summary>
    public partial class ucsReferee : UserControl
    {

        public record class PartRole(int id, string last, string first, DateTime date, string NameRole);
        private MainDBContext? _db;

        List<JudgingStaffShort> dataGridList = new List<JudgingStaffShort>();

        int _idM;
        


        public ucsReferee(GetMatchListModelShort getMatchListModelShort)
        {
            InitializeComponent();




            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();

            _idM = getMatchListModelShort.IdMatch0;

            tblMatchInfo.Text = $"{getMatchListModelShort.Team1AndTeam2Name} | {getMatchListModelShort.DateMatch0}";

            loadDataGrid();


        }

        public void loadDataGrid()
        {

            dataGridList = _db.JudgingStaffs.Where(x => x.IdMatch == _idM)
            .Select(s => new JudgingStaffShort()
            {
                Id = s.Id,
                FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                Telephone = s.Participant.Telephone,
                AmpluaRoleName = s.AmpluaRole.Name,
            }).ToList();


            GridReferee.ItemsSource = dataGridList.Take(10).ToList();

            pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 10.0);

            txtblListCount.Text = "Найдено записей: ";
            txtblListCount.Text += dataGridList.Count().ToString();



        }

        private void loadDataGrid(string filtrby)
        {

            if (filtrby == "")
            {
                loadDataGrid();

            }
            else
            {
                dataGridList = _db.JudgingStaffs.Where(x => x.IdMatch == _idM && x.Participant.Surname.StartsWith(filtrby))
                    .Select(s => new JudgingStaffShort()
                    {
                        Id = s.Id,
                        FIO = $"{s.Participant.Surname} {s.Participant.Name} {s.Participant.Patronymic}",
                        Telephone = s.Participant.Telephone,
                        AmpluaRoleName = s.AmpluaRole.Name,
                    })
                    .ToList();
                //.Where(x => )

                GridReferee.ItemsSource = dataGridList.Take(10).ToList();

                pagGrid.MaxPageCount = (int)Math.Ceiling(dataGridList.Count / 10.0);

                txtblListCount.Text = "Найдено записей: ";
                txtblListCount.Text += dataGridList.Count().ToString();

            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new ucsJudgingStaffDialogAdd(0, true, _idM, this));
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            int id = (GridReferee.SelectedItem as JudgingStaffShort).Id;

            Dialog.Show(new ucsJudgingStaffDialogAdd(id, false, _idM, this));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new windowConfirmation();
            if (dialog.ShowDialog() == true)
            {

                int id = (GridReferee.SelectedItem as JudgingStaffShort).Id;

                JudgingStaff judgingStaff = _db.JudgingStaffs.Find(id);

                _db.JudgingStaffs.Remove(judgingStaff);
                _db.SaveChanges();

                loadDataGrid();

                Growl.Success("Судья успешно удален!");

            }
            
        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadDataGrid(tbFilter.Text.Trim());
        }

        private void btnTabBtnList_Click(object sender, RoutedEventArgs e)
        {
            loadDataGrid();
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            GridReferee.ItemsSource = dataGridList.Skip((e.Info - 1) * 10).Take(10).ToList();
        }




    }
}
