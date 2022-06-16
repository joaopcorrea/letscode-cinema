using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public virtual bool IsVegan()
        {
            if (Id == 55)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void TotalQuantity()
        {
            Console.WriteLine(this.Id + "------" + this.Description 
                + "------" + this.Quantity + "g ------ R$" + this.Price);
        }
    }
}
