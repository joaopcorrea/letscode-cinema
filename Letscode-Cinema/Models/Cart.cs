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
        public int SessionId { get; set; }
        public string[] Seats { get; set; }
        public int[] FoodIds { get; set; }
    }
}
