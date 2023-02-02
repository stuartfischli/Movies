using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace movies
{
    public class Movie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string sdUrl { get; set; }
        public string hdUrl { get; set; }
        public BitmapImage image { get; set; }
    }
}
