using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Ticket : Cart
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }


        //listar tickets
        //criar ticket
    }
}
