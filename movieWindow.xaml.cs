using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
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
            public static Rect rect = new Rect();
            public static DispatcherTimer timer = new DispatcherTimer();
            public static TimeSpan TotalTime = new TimeSpan();
            public static double mbWidth = new double();
            public static int isFullscreen = 0;
        }
       
        
        public void createPlayer()
        {
            Global.videoPlayer.Open(new Uri(@"C:\Users\stuartfischli\Videos\Captures\Halo Infinite 2022-06-21 16-52-03_Slomo.mp4", UriKind.RelativeOrAbsolute));//@"C:\Users\stuartfischli\OneDrive - UNSW\PXL_20210429_032802155.mp4" @"https://t.tarahipro.ir/1401/05/thor-web/Thor.Love.and.Thunder.2022.480p.WEB-DL.SoftSub.Filmsara.mkv"
            Global.rect = new Rect(this.Width / 2, this.Height / 2, movieBorder.Width, movieBorder.Height);
            Global.videoDrawing.Rect = Global.rect;
            Global.videoDrawing.Player = Global.videoPlayer;
            Global.drawingBrush = new DrawingBrush(Global.videoDrawing);
            movieBorder.Fill = Global.drawingBrush;
            controlsGrid.Visibility = Visibility.Hidden;
            Global.drawingBrush.Stretch = Stretch.Fill;
            Global.timer.Interval = TimeSpan.FromSeconds(.1);
            Global.timer.Tick += new EventHandler(ticktock);
            Global.videoPlayer.MediaOpened += new EventHandler(mediaOpened);
            
                                    
        }

        void ticktock(object sender, EventArgs e)
        {
            
            try
            {
                Global.TotalTime = Global.videoPlayer.NaturalDuration.TimeSpan;
                slider.Maximum = Global.TotalTime.TotalSeconds;

                if (Global.TotalTime.TotalSeconds > 0)
                {
                    slider.Value = Global.videoPlayer.Position.TotalSeconds;

                }
            }
            catch
            {

            }
            
        }
               

        private void fullScreenButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            controlsGrid.Visibility = Visibility.Hidden;

            this.WindowStyle = WindowStyle.None;
            //this.Background = Global.drawingBrush;
            //movieGrid.Background = Global.drawingBrush;

            movieBorder.Stroke.Opacity = 0;
            Canvas.SetZIndex(playerGrid, 3);
            Canvas.SetZIndex(movieGrid, 0);
            movieBorder.Fill = Global.drawingBrush;
            playerGrid.Width = this.Width;
            playerGrid.Height = (this.Width * Global.videoPlayer.NaturalVideoHeight) / Global.videoPlayer.NaturalVideoWidth;
            movieBorder.Width = this.Width;
            movieBorder.Height = (this.Width * Global.videoPlayer.NaturalVideoHeight) / Global.videoPlayer.NaturalVideoWidth;
            controlsGrid.Width = movieBorder.Width;
            slider.Width = controlsGrid.Width * .9;
            movieBorder.VerticalAlignment = VerticalAlignment.Center;

            Global.drawingBrush.Stretch = Stretch.Fill;
            Global.isFullscreen = 1;
        }

        private void playButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Global.videoPlayer.Play();
            playButton.Visibility = Visibility.Hidden;
            Global.isPlaying = true;
            Global.timer.Start();
                        
        }

        public void mediaOpened(object sender, EventArgs e)
        {
            Global.mbWidth = (movieBorder.ActualHeight * Global.videoPlayer.NaturalVideoWidth) / Global.videoPlayer.NaturalVideoHeight;
            playerGrid.Width = Global.mbWidth;
            movieBorder.Width = Global.mbWidth;
            controlsGrid.Width = movieBorder.Width;
            Canvas.SetZIndex(playerGrid, 1);
            Canvas.SetZIndex(movieGrid, 0);
            Canvas.SetZIndex(controlsGrid, 4);
            controlsGrid.Margin = new Thickness(movieBorder.Margin.Left, movieBorder.Margin.Top + movieBorder.ActualHeight - controlsGrid.ActualHeight, movieBorder.Margin.Right, movieBorder.Margin.Bottom);
            
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                movieBorder.Stroke.Opacity = 100;
                movieBorder.Height = 500;
                movieBorder.Width = Global.mbWidth;
                movieBorder.HorizontalAlignment = HorizontalAlignment.Center;
                movieBorder.VerticalAlignment = VerticalAlignment.Center;
                Global.videoDrawing.Rect = new Rect(this.Width / 2, this.Height / 2, movieBorder.Width, movieBorder.Height);
                movieGrid.Background = Brushes.Black;
                movieBorder.Fill = Global.drawingBrush;
                Canvas.SetZIndex(movieBorder, 3);
                Canvas.SetZIndex(movieGrid, 0);
                Canvas.SetZIndex(controlsGrid, 4);
                controlsGrid.HorizontalAlignment= HorizontalAlignment.Center;
                controlsGrid.Margin = new Thickness(movieBorder.Margin.Left, movieBorder.Margin.Top + movieBorder.ActualHeight - controlsGrid.ActualHeight, movieBorder.Margin.Right, movieBorder.Margin.Bottom);
                Global.isFullscreen = 0;
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

                

       private void slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (Global.TotalTime.TotalSeconds > 0)
            {
                Global.videoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            }
        }

        private void playerGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            playButton.Visibility = Visibility.Visible;
            controlsGrid.Visibility = Visibility.Visible;
        }

        private void playerGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            playButton.Visibility = Visibility.Hidden;
            controlsGrid.Visibility = Visibility.Hidden;
        }

        private void playerGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (Global.isFullscreen == 1)
            {
                Canvas.SetZIndex(controlsGrid, 6);
                controlsGrid.Visibility = Visibility.Visible;

                Task.Delay(2000).ContinueWith(_ =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        controlsGrid.Visibility = Visibility.Hidden;
                    });
                });

            }
        }
    }
}
