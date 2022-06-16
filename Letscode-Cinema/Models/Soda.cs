using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Soda : Food
    {
        public Soda(int id, string description, double price, int quantity, string sodaFlavor)
        {
            Id = id;
            Description = description;
            Price = 3.50;
            Quantity = quantity;
            SodaFlavor = sodaFlavor;
        }

        public string SodaFlavor { get; set; }

        public bool IsDiet()
        {
            if (SodaFlavor.ToLower().Contains("diet"))
            {
                Id = 33;
                return true;
            }
            else
            {
                Id = 34;
                return false;
            }
        }
        public override bool IsVegan()
        {
            if (SodaFlavor.ToLower().Contains("juice"))
            {
                Id = 32;
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void TotalQuantity()
        {
            Console.WriteLine(this.Id + "------" + this.Description
                + "------" + this.Quantity + "ml ------ R$" + this.Price);
        }
    }
}
