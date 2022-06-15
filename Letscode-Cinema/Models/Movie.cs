using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public int Minutes { get; set; }
        public double Review { get; set; }
        public int MinimumAge { get; set; }
    }
}
