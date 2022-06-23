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
        public List<CartItem> Items { get; set; }

        public class CartItem
        {
            public string Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double TotalPrice { get; set; }
        }
    }
}
