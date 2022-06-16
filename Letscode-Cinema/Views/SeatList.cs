using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    public class SeatList : Menu
    {
        public void ShowSeats(int sessionId)
        {
            DrawMenu("Escolher assentos");

            Session session = Database.GetSession(sessionId);
            Movie movie = Database.GetMovie(session.MovieId);

            Console.WriteLine($"{movie.Title}");
            Console.WriteLine($"{session.Room.Cinema.CinemaName} - {session.Room.RoomName}");
            Console.WriteLine($"{session.Date:dddd dd/MM HH:mm}");
            Console.WriteLine("\nAssentos disponíveis:");
            
            DrawSeats(session);

            Console.WriteLine();
            Console.WriteLine("Escolha um assento: ");

            string seat = Console.ReadLine();


        }

        public void DrawSeats(Session session)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Console.WriteLine();
            string screen = " T E L A "
                .PadLeft((session.Room.Columns < 10 ? session.Room.Columns * 4 : (session.Room.Columns * 5) - 10) / 2 + 7, '-')
                .PadRight((session.Room.Columns < 10 ? session.Room.Columns * 4 : (session.Room.Columns * 5) - 10) + 4, '-');
            Console.WriteLine(" " + screen);
            
            Console.WriteLine();
            for (int row = 0; row < session.Room.Rows; row++)
            {
                string text = $" |  ";
                for (int column = 0; column < session.Room.Columns; column++)
                {
                    text += session.SeatsUserId[column, row] > 0 ? "    " : $"{letters[row]}{column}  ";
                }
                Console.WriteLine(text + "|");
            }

            Console.Write(" ‾‾‾‾");
            for (int column = 0; column < session.Room.Columns; column++)
            {
                Console.Write(column < 10 ? "‾‾‾‾" : "‾‾‾‾‾");
            }
            Console.WriteLine();
        }
    }
}
