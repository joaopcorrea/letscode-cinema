using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    internal class MovieList : Menu
    {
        Dictionary<int, string> movies = new Dictionary<int, string>()
        {

        };

        public void Show()
        {
            DrawMenu("Lista de filmes");

            do
            {

            }
            while (true);
        }
    }
}
