using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class PopCorn : Food
    {
        public bool PopCornIsSalty { get; set; }
        public bool IsSalty(bool salt)
        {
            PopCornIsSalty = salt;
            if (salt)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
