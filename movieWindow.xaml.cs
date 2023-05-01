using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Net;
using System.Net.Sockets;
using GoogleCast;
using GoogleCast.Channels;
using GoogleCast.Models.Media;

namespace movies
{
    /// <summary>
    /// Interaction logic for movieWindow.xaml
    /// </summary>
    public partial class movieWindow : Window
    {
        public ObservableCollection<IReceiver> CastDevices = new ObservableCollection<IReceiver>();
                                
        public movieWindow()
        {
            DataContext = this;
            InitializeComponent();
            createPlayer();
            createTitleBlock();
            this.Title = MainWindow.Global.titles[0].ToString();
            Global.movieUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
            Init();
                        
        }

        public void createTitleBlock()
        {
            try
            {
                titleLabel.Text = MainWindow.Global.movieList[MainWindow.Global.movieNumber].Title.ToString();
                ImageBrush tileBrush = new ImageBrush(MainWindow.Global.images[MainWindow.Global.movieNumber]);
                coverImage.Background = tileBrush;
                metadataBlock.Text = MainWindow.Global.descriptions[MainWindow.Global.movieNumber].ToString();
                
            }
            catch
            {

            }
            
        }
        
        public class Global
        {
            public static VideoDrawing videoDrawing = new VideoDrawing();
            public static MediaPlayer videoPlayer = new MediaPlayer();
            public static DrawingBrush drawingBrush = new DrawingBrush();
            public static string movieUrl = "";
            public static bool isPlaying = false;
            public static Rect rect = new Rect();
            public static DispatcherTimer timer = new DispatcherTimer();
            public static TimeSpan TotalTime = new TimeSpan();
            public static double mbWidth = new double();
            public static double mbHeight = new double();   
            public static bool isFullscreen = false;
                                    
        }
       
        
        public void createPlayer()
        {
            try
            {

                if (MainWindow.Global.movieList[MainWindow.Global.movieNumber].hdUrl != null)
                {
                    Global.movieUrl = MainWindow.Global.movieList[MainWindow.Global.movieNumber].hdUrl;
                }
                else if (MainWindow.Global.movieList[MainWindow.Global.movieNumber].sdUrl != null)
                {
                    Global.movieUrl = MainWindow.Global.movieList[MainWindow.Global.movieNumber].sdUrl;
                }
                else
                {
                    MessageBox.Show("Movie not found");
                }
                Global.movieUrl = MainWindow.Global.movieList[MainWindow.Global.movieNumber].sdUrl;
                Global.videoPlayer.Open(new Uri(Global.movieUrl, UriKind.Absolute));//@"C:\Users\stuartfischli\OneDrive - UNSW\PXL_20210429_032802155.mp4" @"https://t.tarahipro.ir/1401/05/thor-web/Thor.Love.and.Thunder.2022.480p.WEB-DL.SoftSub.Filmsara.mkv"
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
                controlsGrid.Margin = new Thickness(0, 500 - 50, 0, 2);
                castBorder.Visibility = Visibility.Hidden;
            }
            catch
            {

            }
            
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
        void goFullscreen()
        {
            controlsGrid.Visibility = Visibility.Visible;
            titleGrid.Visibility = Visibility.Hidden;
            backLabel.Visibility = Visibility.Hidden;
            homeImage.Visibility = Visibility.Hidden;
            scrollViewer.IsEnabled = false;

            this.WindowStyle = WindowStyle.None;

            Global.mbHeight = (this.Width * Global.videoPlayer.NaturalVideoHeight) / Global.videoPlayer.NaturalVideoWidth;
            movieBorder.Stroke.Opacity = 0;
            Canvas.SetZIndex(playerGrid, 3);
            Canvas.SetZIndex(movieGrid, 0);
            Canvas.SetZIndex(controlsGrid, 15);
            Canvas.SetZIndex(smallPlayButton, 12);
            Canvas.SetZIndex(controlsBorder, 6);
            movieBorder.Fill = Global.drawingBrush;
            playerGrid.HorizontalAlignment = HorizontalAlignment.Center;
            playerGrid.Width = this.Width;
            playerGrid.Height = Global.mbHeight;
            movieBorder.Width = this.Width;
            movieBorder.Height = Global.mbHeight;
            controlsGrid.Margin = new Thickness(0, Global.mbHeight - 51, 0, 2);
            playerGrid.VerticalAlignment = VerticalAlignment.Center;
            playerGrid.Margin = new Thickness(0, 0, 0, (movieGrid.ActualHeight - ActualHeight) / 2);


            Global.drawingBrush.Stretch = Stretch.Fill;
            Global.isFullscreen = true;
        }

        private void fullScreenButton_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            goFullscreen();                        
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
            Canvas.SetZIndex(playerGrid, 1);
            Canvas.SetZIndex(movieGrid, 0);
            Canvas.SetZIndex(controlsGrid, 4);
            
        }

        void exitFullscreen()
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            movieBorder.Stroke.Opacity = 100;
            movieBorder.Height = 500;
            movieBorder.Width = Global.mbWidth;
            Global.videoDrawing.Rect = new Rect(this.Width / 2, this.Height / 2, movieBorder.Width, movieBorder.Height);
            movieBorder.Fill = Global.drawingBrush;
            Canvas.SetZIndex(movieBorder, 4);
            Canvas.SetZIndex(movieGrid, 0);
            Canvas.SetZIndex(controlsGrid, 10);
            Canvas.SetZIndex(smallPlayButton, 8);
            Canvas.SetZIndex(slider, 8);
            Canvas.SetZIndex(fullScreenButton, 8);
            Canvas.SetZIndex(castButton, 8);
            Global.isFullscreen = false;
            controlsGrid.Margin = new Thickness(0, (ActualHeight / 2) + 250 - 51, 0, 0);
            titleGrid.Visibility = Visibility.Visible;
            backLabel.Visibility = Visibility.Visible;
            homeImage.Visibility = Visibility.Visible;
            playerGrid.HorizontalAlignment = HorizontalAlignment.Center;
            playerGrid.VerticalAlignment = VerticalAlignment.Center;
            playerGrid.Margin = new Thickness(0, -430, 0, 0); 
            titleGrid.Margin = new Thickness(0, 765, 0, 0);
            scrollViewer.IsEnabled = true;
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                exitFullscreen();                
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
            else if (e.Key == Key.F)
            {
                if (Global.isFullscreen == false)
                {
                    goFullscreen();
                }
                else
                {
                    exitFullscreen();
                }
            }

            else
            {

            }
        }
                                

       private void slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (Global.TotalTime.TotalSeconds > 0)
            {
                Global.videoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            }
        }

        private void movieBorder_MouseMove(object sender, MouseEventArgs e)
        {
            controlsGrid.Visibility = Visibility.Visible;
            Canvas.SetZIndex(controlsGrid, 16);
                        
            if (controlsGrid.IsMouseOver == false)
            {
                Task.Delay(2500).ContinueWith(_ =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        controlsGrid.Visibility = Visibility.Hidden;
                    });
                });
            }
            
            
        }

     
        private void homeImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Global.videoPlayer.Stop();
            Window windowToNavigate = new MainWindow();
            windowToNavigate.Show();
            MainWindow.Global.ClearData();
            
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {                        
            this.Close();
            Global.videoPlayer.Stop();
            if (MainWindow.Global.movieNumber <= 10)
            {
                MainWindow.AppWindow.MainFrame.Navigate(new Uri("searchResultsPage1.xaml", UriKind.Relative));
                
            }
            else if (MainWindow.Global.movieNumber <= 20)
            {
                MainWindow.AppWindow.MainFrame.Navigate(new Uri("searchResultsPage2.xaml", UriKind.Relative));
            }
            else if (MainWindow.Global.movieNumber <= 30)
            {
                MainWindow.AppWindow.MainFrame.Navigate(new Uri("searchResultsPage3.xaml", UriKind.Relative));
            }
            
        }

        private void slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Global.TotalTime.TotalSeconds > 0)
            {
                Global.videoPlayer.Position = TimeSpan.FromSeconds(slider.Value);
            }
        }

                            
        async Task Init()
        {
            var chromecasts = (await new DeviceLocator().FindReceiversAsync());
            foreach (var r in chromecasts)
            {
                CastDevices.Add(r);
            }
            chromecastList.ItemsSource = CastDevices;
            chromecastList.DisplayMemberPath = "FriendlyName";
        }   
       
        private void castButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            castBorder.Visibility = Visibility.Visible;
            chromecastList.Visibility = Visibility.Visible;
            Canvas.SetZIndex(chromecastList, 10);
            Canvas.SetZIndex(castBorder, 10);
                                    
        }

        private async void chromecastList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            castBorder.Visibility = Visibility.Hidden;
            if (Global.isPlaying == true)
            {
                Global.videoPlayer.Pause();
                Global.isPlaying = false;
            }
            int selected = chromecastList.SelectedIndex;
            var caster = new Sender();
            await caster.ConnectAsync(CastDevices[selected]);
            var mediaChannel = caster.GetChannel<IMediaChannel>();
            await caster.LaunchAsync(mediaChannel);
            // Load and play Big Buck Bunny video
            _ = await mediaChannel.LoadAsync(
                new MediaInformation() { ContentId = Global.movieUrl });

            
        }

        private void playerWindow_Closed(object sender, EventArgs e)
        {
            Global.videoPlayer.Stop();
        }

        private void controlsGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            controlsGrid.Visibility = Visibility.Visible;
        }

        private void playButton_MouseEnter(object sender, MouseEventArgs e)
        {
            playButton.Visibility = Visibility.Visible;
            controlsGrid.Visibility = Visibility.Visible;
        }

        private void movieBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (castBorder.Visibility == Visibility.Visible)
            {
                castBorder.Visibility = Visibility.Hidden;
                chromecastList.Visibility = Visibility.Hidden;
                Init();
                CastDevices.Clear();
            }
            else if (Global.isPlaying == true)
            {
                Global.videoPlayer.Pause();
                Global.isPlaying = false;
            }
            else if (Global.isPlaying == false)
            {
                Global.videoPlayer.Play();
                Global.isPlaying = true;
            }

        }

        
    }
}
