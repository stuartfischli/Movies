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
using movies;
using WpfApp1;



namespace movies
{
    /// <summary>
    /// Interaction logic for searchResults.xaml
    /// </summary>
    public partial class SearchResults : Page
    {
        public SearchResults()
        {
            InitializeComponent();

            try
            {
                int k = 0;
                while (k < MainWindow.Global.urls.Count && k < 10)
                {
                    image1.Source = MainWindow.Global.movieList[k].image;
                    label1.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image2.Source = MainWindow.Global.movieList[k].image;
                    label2.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image3.Source = MainWindow.Global.movieList[k].image;
                    label3.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image4.Source = MainWindow.Global.movieList[k].image;
                    label4.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image5.Source = MainWindow.Global.movieList[k].image;
                    label5.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image6.Source = MainWindow.Global.movieList[k].image;
                    label6.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image7.Source = MainWindow.Global.movieList[k].image;
                    label7.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image8.Source = MainWindow.Global.movieList[k].image;
                    label8.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image9.Source = MainWindow.Global.movieList[k].image;
                    label9.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image10.Source = MainWindow.Global.movieList[k].image;
                    label10.Text = MainWindow.Global.movieList[k].Title;
                    k++;

                }
            }
            catch
            {

            }

            resultsLabel.Content = "Results for \"" + MainWindow.Global.searchTerm + "\":";

            prevBorder.Background = Brushes.Gray;
            prevLabel.Background = Brushes.Gray;
            prevLabel.Foreground = Brushes.DarkGray;

            if (MainWindow.Global.movieList.Count <= 10)
            {
                nextBorder.Background = Brushes.Gray;
                nextLabel.Foreground = Brushes.DarkGray;
                nextLabel.Background = Brushes.Gray;
            }
        }

        private void nextLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.Global.movieList.Count <= 10)
            {

            }
            else
            {
                this.NavigationService.Navigate(new Uri("searchResultsPage2.xaml", UriKind.Relative));
                this.Visibility = Visibility.Hidden;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow.Global.ClearData();
        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Hidden;
            Window windowToHide = new MainWindow();
            windowToHide.Hide();
            Window windowToNavigate = new movieWindow();
            windowToNavigate.Show();
            

        }
    }

        
}
