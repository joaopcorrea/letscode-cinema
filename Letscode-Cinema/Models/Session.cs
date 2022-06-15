using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Session : Room
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime Date { get; set; }
        public int[,] SeatsUserId { get; set; }
        public double Price { get; set; }
    }
}
