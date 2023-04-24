using FootBallCompasition_WPF.context;
using FootBallCompasition_WPF.Pages;
using FootBallCompasition_WPF.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;

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


            btnFrameTeam.Click += btnFrameTeam_Click;
            btnFrameMatch.Click += btnFrameMatch_Click;
            btnFrameStadium.Click += btnFrameStadium_Click;
            btnFrameParticipant.Click += btnFrameParticipant_Click;


            btnWindowClose.Click += btnWindowClose_Click;
            //tglbtnWindowScreen.Click += tglbtnWindowScreen_Click;
            btnWindowHide.Click += btnWindowHide_Click;



            var dialog = new WindowAuthorization();
            if(dialog.ShowDialog() == false)
            {
                Close();

            }
            else     
            {
                dbConfiguration.ConfigureServices();
                _db = dbConfiguration.Services.GetService<MainDBContext>();

            }




        }

        



        private void btnFrameTeam_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageTeam());
        }

        private void btnFrameParticipant_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageParticipant("Игрок"));
        }

        private void btnFrameStadium_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageStadium());
        }

        private void btnFrameMatch_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageMatch());
        }

        //private void tglbtnWindowScreen_Click(object sender, RoutedEventArgs e)
        //{
        //    if()
        //    WindowState = WindowState.Maximized;
        //    WindowStyle = WindowStyle.None;
        //}



        private void btnWindowClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void btnWindowHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        
    }
}
