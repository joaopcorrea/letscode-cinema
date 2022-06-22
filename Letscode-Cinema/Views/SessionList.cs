using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    public class SessionList : Menu
    {
        List<Session> sessions = Database.GetSessions();

        public void ShowSessions(int movieId)
        {
            Console.WriteLine("\n0. Voltar");
            foreach (Session session in sessions)
            {
                if (session.MovieId == movieId)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{session.Id}. {session.Room.Cinema.CinemaName}, {session.Room.Cinema.City} - {session.Room.RoomName}");
                    Console.WriteLine($"    Data: {session.Date.ToString("d")}");
                    Console.WriteLine($"    Horário: +{session.Date.ToString("t")}");
                    Console.WriteLine($"    Preço: R$ {session.Price:N2}");
                    if (session.Is3d)
                        Console.WriteLine($"    Sessão 3D");
                }
            }
        }

        public Session ChooseSession(Movie movie)
        {
            if (sessions.Count() <= 0)
                throw new Exception($"Nenhuma sessão disponível para o filme {movie.Title}");
            else
            {
                DrawMenu("Escolher Sessão");

                Console.WriteLine($"Sessões disponíveis para o filme\n{movie.Title}");
                
                ShowSessions(movie.Id);
            }
            Console.WriteLine();

            bool choiseIsInt = false;
            int sessionId = 0;
            Session session = new Session();
            do
            {
                Console.Write("Escolha uma sessão: ");
                string choise = Console.ReadLine();
                choiseIsInt = Int32.TryParse(choise, out sessionId);
                if (!choiseIsInt)
                    Console.Write("Digite o número da sessão. ");
                else
                {
                    if (sessionId == 0)
                    {
                        Console.Clear();
                        return null;
                    }
                    else
                    {
                        session = Database.GetSessions().FirstOrDefault(session => session.Id == sessionId && session.MovieId == movie.Id);
                        if (session == null)
                            Console.Write("Opção inválida. ");
                    }
                }
            } while (!choiseIsInt || session == null);
            Console.Clear();

            return session;
        }
    }
}
