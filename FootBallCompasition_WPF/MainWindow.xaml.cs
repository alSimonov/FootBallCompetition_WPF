using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainDBContext? _db;


        public MainWindow()
        {
            InitializeComponent();



            smTeam.Selected += smTeam_Selected;
            smMatch.Selected += smMatch_Selected;
            smStadium.Selected += smStadium_Selected;
            smPlayers.Selected += smPlayers_Selected;
            smReferee.Selected += smReferee_Selected;

            btnClockDispose.Click += btnClockDispose_Click;


            smTeam.IsSelected = true;

            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();




            //Text1.Click += Text1_Click;

            //SideMenu.CommandBindings 
            //    += SwitchItemCmd_Executed;




        }

        private void btnClockDispose_Click(object sender, RoutedEventArgs e)
        {

            flipClock.Dispose();


        }

        private void smTeam_Selected(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new TeamPage());
        }
        private void smMatch_Selected(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new MatchPage());
        }
        private void smStadium_Selected(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new StadiumPage());
        }
        private void smPlayers_Selected(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ParticipantPage("Игрок"));
        }
        private void smReferee_Selected(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ParticipantPage("Судья"));
        }




        private void SwitchItemCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            MessageBox.Show("Вызов справки");
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximazed = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                if (IsMaximazed)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximazed = false;
                }
                else 
                {
                    this.WindowState = WindowState.Maximized;
                    
                    IsMaximazed = true;

                }
            }
        }
    }
}
