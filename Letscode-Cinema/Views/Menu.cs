﻿using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    public class Menu
    {
        public Menu()
        {

        }

        public void DrawMenu(string title)
        {
            Console.Clear();
            Console.WriteLine("==================");
            Console.WriteLine(title);
            Console.WriteLine("==================");
            Console.WriteLine();
        }

        public void DrawSessionMenu(int sessionId)
        {
            Session session = Database.GetSession(sessionId);
            Movie movie = Database.GetMovie(session.MovieId);

            Console.WriteLine($"{movie.Title}");
            Console.WriteLine($"{session.Room.Cinema.CinemaName} - {session.Room.RoomName}");
            Console.WriteLine($"{session.Date:dddd dd/MM HH:mm}\n");
        }
    }
}
