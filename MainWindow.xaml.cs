using HtmlAgilityPack;
using movies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public MainWindow()
        {
            WindowState = WindowState.Maximized;
            

            InitializeComponent();
            AppWindow = this;
                      
        }

        public class Global
        {
            public static List<string> titles = new List<string>();
            public static List<string> descriptions = new List<string>();
            public static List<BitmapImage> images = new List<BitmapImage>();
            public static List<List<string>> urls = new List<List<string>>();
            public static List<Movie> movieList = new List<Movie>();
            public static string searchTerm = "";
            public static int proceed = new int();
            public static int movieNumber = new int();
            

            public static void GetInfo(string query)
            {
                Global.proceed = 0;
                var formattedQuery = query.Replace(" ", "+");


                string url = "https://lightdlmovies.blogspot.com/search?q=" + formattedQuery + "&max-results=30";

                var web = new HtmlWeb();
                var doc = web.Load(url);

                try
                {

                    var titlesPrelim = new List<string>();


                    foreach (var title in doc.DocumentNode.SelectNodes("//script[contains(text(), 'var x=')]"))
                    {
                        titlesPrelim.Add(title.InnerText);
                    }

                    foreach (var title in titlesPrelim)
                    {
                        int pFrom = title.IndexOf("var x=") + "var x='".Length;
                        int pTo = title.IndexOf("var y=") - 2;
                        titles.Add(title.Substring(pFrom, pTo - pFrom).Trim());
                    }

                    for (int i = 0; i < titles.Count; i++)
                    {
                        int index = Math.Min(titles[i].IndexOf("1080"), titles[i].IndexOf("720"));
                        if (index >= 0)
                        {
                            titles[i] = titles[i].Substring(0, index-1);
                        }
                    }

                    var cards = doc.DocumentNode.SelectNodes("//div[@class='post-body']");

                    for (int i = 0; i < cards.Count; i++)
                    {
                        urls.Add(new List<string>());
                    }


                    for (int i = 0; i < cards.Count; i++)
                    {

                        foreach (var j in cards[i].SelectNodes("//a[@href]"))
                        {
                            if (j.Attributes["href"].Value.EndsWith(".mkv"))
                            {
                                urls[i].Add(j.GetAttributeValue("href", ""));
                            }
                        }


                    }


                    foreach (var div in doc.DocumentNode.SelectNodes("//div[@class='post-body']/span"))
                    {
                        descriptions.Add(div.InnerText.Trim());

                    }
                    string[] temp = { "Writers", "Stars", "Genre", "Release", "Runtime" };
                    List<string> keywords = new List<string>(temp);

                    for (int i = 0; i < descriptions.Count; i++)
                    {
                        foreach (var keyword in keywords)
                        {
                            int index = descriptions[i].IndexOf(keyword);
                            if (index >= 0)
                            {
                                descriptions[i] = descriptions[i].Insert(index, "\n");
                            }
                        }
                        
                    }
                    for (int j = 0; j < descriptions.Count; j++)
                    {
                        int index = descriptions[j].IndexOf("min");
                        if (index > 0)
                        {
                            descriptions[j] = descriptions[j].Insert(index + 4, "\nSummary: ");
                        }
                        string targetPhrase = "Please Support";
                        int index2 = descriptions[j].IndexOf(targetPhrase);
                        if (index2 > 0)
                        {
                            descriptions[j] = descriptions[j].Substring(0, index2);
                        }
                    }
                                        
                    var prelimImages = new List<string>();
                    foreach (var div in doc.DocumentNode.SelectNodes("//div[@class='post-body']//img"))
                    {
                        prelimImages.Add(div.GetAttributeValue("src", ""));

                    }

                    prelimImages.RemoveAll(s => s.Contains("twitterheader"));

                    foreach (var s in prelimImages)
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(s, UriKind.Absolute);
                        bitmap.EndInit();

                        images.Add(bitmap);
                    }


                    for (int i = 0; i < urls.Count; i++)
                    {
                        movieList.Add(new Movie());
                        movieList[i].Title = titles[i];
                        movieList[i].Description = descriptions[i];
                        movieList[i].sdUrl = urls[i].FirstOrDefault(s => s.Contains("720"));
                        movieList[i].hdUrl = urls[i].FirstOrDefault(s => s.Contains("1080"));
                        movieList[i].image = images[i];
                    }
                }
                catch 
                {
                    Global.proceed = 1;
                }
            }

            public static void ClearData()
            {
                titles.Clear();
                descriptions.Clear();
                images.Clear();
                urls.Clear();
                movieList.Clear();
                searchTerm = String.Empty;
                proceed = 0;
                Canvas.SetZIndex(AppWindow.searchBar, 5);
                Canvas.SetZIndex(AppWindow.searchBorder, 5);
                AppWindow.Title = "Scax";
            }
        }

       
        public void searchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string query = searchBar.Text;
                Global.searchTerm = searchBar.Text;
                Global.GetInfo(query);

                if (Global.proceed == 1)
                {
                    noResultsLabel.Content = "No results.";
                    Canvas.SetZIndex(noResultsLabel, 3);
                    Global.ClearData();
                }
                else
                {
                    noResultsLabel.Visibility = Visibility.Hidden;
                    MainFrame.Navigate(new Uri("searchResultsPage1.xaml", UriKind.Relative));


                    Canvas.SetZIndex(searchBorder, 0);
                    Canvas.SetZIndex(logo, 0);
                }
                
            }
        }
    }
}
