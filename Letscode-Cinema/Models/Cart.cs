using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public Session SessionId { get; set; }
        public List<int[]> Seats { get; set; }
        public Dictionary<int, int> FoodIds { get; set; }
    }
}
