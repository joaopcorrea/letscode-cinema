using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Models
{
    public class Room : Cinema
    {
        public string RoomName { get; set; }
        public bool Is3d { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
