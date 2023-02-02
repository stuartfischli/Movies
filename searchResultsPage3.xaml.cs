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

namespace movies
{
    /// <summary>
    /// Interaction logic for searchResultsPage3.xaml
    /// </summary>
    public partial class searchResultsPage3 : Page
    {
        public searchResultsPage3()
        {
            InitializeComponent();

            try
            {
                int k = 20;
                while (k < MainWindow.Global.urls.Count && k < 30)
                {
                    image21.Source = MainWindow.Global.movieList[k].image;
                    label21.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image22.Source = MainWindow.Global.movieList[k].image;
                    label22.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image23.Source = MainWindow.Global.movieList[k].image;
                    label23.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image24.Source = MainWindow.Global.movieList[k].image;
                    label24.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image25.Source = MainWindow.Global.movieList[k].image;
                    label25.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image26.Source = MainWindow.Global.movieList[k].image;
                    label26.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image27.Source = MainWindow.Global.movieList[k].image;
                    label27.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image28.Source = MainWindow.Global.movieList[k].image;
                    label28.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image29.Source = MainWindow.Global.movieList[k].image;
                    label29.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image30.Source = MainWindow.Global.movieList[k].image;
                    label30.Text = MainWindow.Global.movieList[k].Title;
                    k++;

                }
            }
            catch
            {

            }

            resultsLabelPage3.Content = "Results for \"" + MainWindow.Global.searchTerm + "\":";

            nextBorderPage3.Background = Brushes.Gray;
            nextLabelPage3.Background = Brushes.Gray;
            nextLabelPage3.Foreground = Brushes.DarkGray;

        }

        
        private void prevLabelPage3_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("searchResultsPage2.xaml", UriKind.Relative));
        }

        private void homeImagePage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            MainWindow.Global.ClearData();
        }

        private void image21_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("movieDetails.xaml", UriKind.Relative));
        }
    }
    
}
