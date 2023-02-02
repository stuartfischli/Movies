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
using WpfApp1;
using static System.Windows.SystemParameters;

namespace movies
{
    /// <summary>
    /// Interaction logic for movieDetails.xaml
    /// </summary>
    public partial class movieDetails : Page
    {
        public string video { get; set; } = @"C:\Users\stuartfischli\OneDrive - UNSW\PXL_20210429_032802155.mp4"; //"https://t.tarahipro.ir/1401/05/thor-web/Thor.Love.and.Thunder.2022.480p.WEB-DL.SoftSub.Filmsara.mkv";//MainWindow.Global.urls[1][0];


        public movieDetails()
        {
            DataContext = this;
            InitializeComponent();
            
            
            //videoPlayer.LoadedBehavior = MediaState.Manual;



        }

        

        private void homeImageMD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow.Global.ClearData();
        }

        

        private void playButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            videoPlayer.Play();
            playButton.Visibility = Visibility.Hidden;

            movieBorder.Height = videoPlayer.Height + 3;
            movieBorder.Width = videoPlayer.Width;

            
        }

        private void fullScreenButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            

            fullScreenButton.Visibility = Visibility.Hidden;
            movieBorder.Visibility = Visibility.Hidden;

            
            movieGrid.Height = FullPrimaryScreenHeight;
            movieGrid.Width = FullPrimaryScreenWidth;

            videoPlayer.Height = FullPrimaryScreenHeight;
            videoPlayer.Width = FullPrimaryScreenWidth;
        }





        //public string Video
        //{
        //    get { return video; }
        //    set
        //    {
        //        video = value;

        //    }
        //}


    }
    
}
