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
using System.Windows.Shapes;
using WpfApp1;

namespace movies
{
    /// <summary>
    /// Interaction logic for movieWindow.xaml
    /// </summary>
    public partial class movieWindow : Window
    {

        public movieWindow()
        {
            DataContext = this;
            InitializeComponent();
            createPlayer();
            
        }

        public class Global
        {
            public static VideoDrawing videoDrawing = new VideoDrawing();
            public static MediaPlayer videoPlayer = new MediaPlayer();
            public static DrawingBrush drawingBrush = new DrawingBrush();
            public static bool isPlaying = false;
        }
       
        
        public void createPlayer()
        {
            
            Global.videoPlayer.Open(new Uri(@"C:\Users\stuartfischli\OneDrive - UNSW\PXL_20210429_032802155.mp4", UriKind.Relative));

            Global.videoDrawing.Rect = new Rect(this.Width / 2, this.Height / 2, movieBorder.Width, movieBorder.Height);
            Global.videoDrawing.Player = Global.videoPlayer;
            Global.drawingBrush = new DrawingBrush(Global.videoDrawing);
            movieBorder.Fill = Global.drawingBrush;
        }

        

        

        private void fullScreenButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fullScreenButton.Visibility = Visibility.Hidden;

            this.WindowStyle = WindowStyle.None;
            //this.Background = Global.drawingBrush;
            movieGrid.Background = Global.drawingBrush;

            movieBorder.Opacity = 0;
            movieBorder.Height = this.Height;
            movieBorder.Width = this.Width;
                       
            
        }

        private void playButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Global.videoPlayer.Play();
            playButton.Visibility = Visibility.Hidden;
            Global.isPlaying = true;
            
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                movieBorder.Opacity = 100;
                movieBorder.Height = 500;//(9/16)*Height;
                movieBorder.Width = 800;// (9/16)*Width;
                movieBorder.HorizontalAlignment = HorizontalAlignment.Center;
                movieBorder.VerticalAlignment = VerticalAlignment.Center;
                Global.videoDrawing.Rect = new Rect(this.Width / 2, this.Height / 2, movieBorder.Width, movieBorder.Height);
                movieGrid.Background = Brushes.Black;
                movieBorder.Fill = Global.drawingBrush;
                Canvas.SetZIndex(movieBorder, 3);
                Canvas.SetZIndex(movieGrid, 0);
                
            }

            else if (e.Key == Key.Space)
            {
                if (Global.isPlaying == true)
                {
                    Global.videoPlayer.Pause();
                    Global.isPlaying = false;
                }
                else
                {
                    Global.videoPlayer.Play();
                    Global.isPlaying = true;
                }
                
            }

            else
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
