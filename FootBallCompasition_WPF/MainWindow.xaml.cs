using FootBallCompasition_WPF.context;
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

            btnWindowClose.Click += btnWindowClose_Click;
            //tglbtnWindowScreen.Click += tglbtnWindowScreen_Click;
            btnWindowHide.Click += btnWindowHide_Click;


            dbConfiguration.ConfigureServices();
            _db = dbConfiguration.Services.GetService<MainDBContext>();







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
        private void btnFrameTeam_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PageParticipant("Игрок"));
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
