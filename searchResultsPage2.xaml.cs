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
using movies;

namespace movies
{
    /// <summary>
    /// Interaction logic for searchResultsPage2.xaml
    /// </summary>
    public partial class searchResultsPage2 : Page
    {
        public searchResultsPage2()
        {
            InitializeComponent();

            try
            {
                int k = 10;
                while (k < MainWindow.Global.urls.Count && k < 20)
                {
                    image11.Source = MainWindow.Global.movieList[k].image;
                    label11.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image12.Source = MainWindow.Global.movieList[k].image;
                    label12.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image13.Source = MainWindow.Global.movieList[k].image;
                    label13.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image14.Source = MainWindow.Global.movieList[k].image;
                    label14.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image15.Source = MainWindow.Global.movieList[k].image;
                    label15.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image16.Source = MainWindow.Global.movieList[k].image;
                    label16.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image17.Source = MainWindow.Global.movieList[k].image;
                    label17.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image18.Source = MainWindow.Global.movieList[k].image;
                    label18.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image19.Source = MainWindow.Global.movieList[k].image;
                    label19.Text = MainWindow.Global.movieList[k].Title;
                    k++;
                    image20.Source = MainWindow.Global.movieList[k].image;
                    label20.Text = MainWindow.Global.movieList[k].Title;
                    k++;

                }
            }
            catch
            {

            }

            resultsLabelPage2.Content = "Results for \"" + MainWindow.Global.searchTerm + "\":";

            if (MainWindow.Global.movieList.Count <= 20)
            {
                nextBorderPage2.Background = Brushes.Gray;
                nextLabelPage2.Background = Brushes.Gray;
                nextLabelPage2.Foreground = Brushes.DarkGray;
            }
        }

        private void prevLabelPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("searchResultsPage1.xaml", UriKind.Relative));
        }

        private void nextLabelPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.Global.movieList.Count <= 20)
            {

            }
            else
            {
                this.NavigationService.Navigate(new Uri("searchResultsPage3.xaml", UriKind.Relative));
                this.Visibility = Visibility.Hidden;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Hidden;
            MainWindow.Global.ClearData();
        }
    }
}
