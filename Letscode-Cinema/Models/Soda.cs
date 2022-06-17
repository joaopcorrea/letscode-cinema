using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Soda : Food
    {
        public string SodaFlavor { get; set; }
        public bool IsDiet { get; set; }


        public bool IsItDiet(bool isdiet)
        {
            IsDiet = isdiet;
            if (isdiet)
            {
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
