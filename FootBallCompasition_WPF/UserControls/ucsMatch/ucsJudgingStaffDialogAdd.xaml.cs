﻿using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.FootballClass;
using HandyControl.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;

namespace FootBallCompasition_WPF.UserControls.ucsMatch
{
    /// <summary>
    /// Логика взаимодействия для ucsJudgingStaffDialogAdd.xaml
    /// </summary>
    public partial class ucsJudgingStaffDialogAdd : UserControl
    {


        public MainDBContext? _db;

        ucsReferee _ucsReferee;

        FootballClass.JudgingStaff _judgingStaff;

        int _idP;
        bool _addOrModify;
        int _idM;


        public ucsJudgingStaffDialogAdd(int idP, bool addOrModify, int idM, ucsReferee ucsReferee)
        {
            InitializeComponent();

            tblPartErr.Visibility = Visibility.Collapsed;
            tblRoleErr.Visibility = Visibility.Collapsed;

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();


            _idP = idP;
            _addOrModify = addOrModify;
            _idM = idM;
            _ucsReferee = ucsReferee;


            if (_addOrModify)
                tglbtnActive.IsChecked = true;
            else tglbtnActive.IsChecked = false;



            loadComboboxData();


        }

        public void loadComboboxData()
        {

            List<Participant> ll;

            if (tglbtnActive.IsChecked == true)
                ll = _db.Participants.Where(x => x.Active == true && x.Role.Name == "Судья").ToList();
            else
                ll = _db.Participants.Where(x => x.Role.Name == "Судья").ToList();


            ll.ForEach(x => x.SetFullName());

            cbParticipant.ItemsSource = ll;
            cbParticipant.SelectedValuePath = "Id";
            cbParticipant.DisplayMemberPath = "FullName";


            cbAmpluaRole.ItemsSource = _db.AmpluaRoles.Where(x => x.Id == 5 || x.Id == 6 || x.Id == 7 || x.Id == 8).ToList();
            cbAmpluaRole.SelectedValuePath = "Id";
            cbAmpluaRole.DisplayMemberPath = "Name";



            if (!_addOrModify)
            {
                _judgingStaff = _db.JudgingStaffs.Find(_idP);

                cbParticipant.SelectedItem = _judgingStaff.Participant;
                cbAmpluaRole.SelectedItem = _judgingStaff.AmpluaRole;

            }


        }



        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            tblPartErr.Visibility = cbParticipant.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;
            tblRoleErr.Visibility = cbAmpluaRole.SelectedIndex == -1 ? Visibility.Visible : Visibility.Collapsed;

            if (tblPartErr.Visibility == 0 || tblRoleErr.Visibility == 0)
                return;




            if (_addOrModify)
            {
                FootballClass.JudgingStaff judgingStaff = new FootballClass.JudgingStaff();

                judgingStaff.Match = _db.Matches.Find(_idM);

                judgingStaff.Participant = (Participant)cbParticipant.SelectedItem;
                judgingStaff.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;


                _db.JudgingStaffs.Add(judgingStaff);
                _db.SaveChanges();

                _ucsReferee.loadDataGrid();
                Growl.Success("Судья успешно добавлен!");



            }
            else if (!_addOrModify)
            {
                _judgingStaff.Participant = (Participant)cbParticipant.SelectedItem;
                _judgingStaff.AmpluaRole = (AmpluaRole)cbAmpluaRole.SelectedItem;

                _db.Entry(_judgingStaff).State = EntityState.Modified;
                _db.SaveChanges();

                _ucsReferee.loadDataGrid();

                Growl.Success("Судья успешно изменен!");

            }





        }

        private void tglbtnActive_Click(object sender, RoutedEventArgs e)
        {
            loadComboboxData();
        }
    }
}
