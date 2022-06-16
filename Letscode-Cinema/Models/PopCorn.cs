using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class PopCorn : Food
    {
        public PopCorn(int id, string description, double price, int quantity, bool isSalty)
        {
            Id = id;
            Description = description;
            Price = 5.00;
            Quantity = quantity;
            PopCornIsSalty = isSalty;
        }

        public bool PopCornIsSalty { get; set; }

        public bool IsSalty(bool salt)
        {
            PopCornIsSalty = salt;
            if (salt)
            {
                Id = 55;
                return true;
            }
            else
            {
                Id = 56;
                return false;
            }
        } 
    }
}
