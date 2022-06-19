using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    internal class SessionList
    {
        List<Session> sessions = Database.GetSession();

        public void ShowSessions(int movieId)
        {
            foreach (Session session in sessions)
            {
                if (session.MovieId == movieId)
                {
                    Console.WriteLine($"Sessão {session.Id}:");
                    Console.WriteLine($"    Cinema: {session.Room.Cinema.CinemaName}, {session.Room.Cinema.City}");
                    Console.WriteLine($"    Data: {session.Date.ToString("d")}");
                    Console.WriteLine($"    Horário: +{session.Date.ToString("t")}");
                    Console.WriteLine($"    Preço: R$ {session.Price:N2}");
                    if (session.Is3d)
                        Console.WriteLine($"    Sessão 3D");
                }
            }
        }

        public Session ChooseSession(Movie movie, User user)
        {
            if ((DateTime.Now - user.BirthDate).Days < movie.MinimumAge*365)
                 throw new Exception($"É preciso ter mais de {movie.MinimumAge} anos para comprar ingressos para o filme {movie.Title}.{System.Environment.NewLine}Escolha outro filme.");
            if (sessions.Count() <= 0)
                throw new Exception($"Nenhuma sessão disponível para o filme {movie.Title}");
            Console.WriteLine("Sessões disponíveis para o filme " + movie.Title);
            this.ShowSessions(movie.Id);
            Console.WriteLine("Digite 0 para voltar.");
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
                        session = Database.GetSession().FirstOrDefault(session => session.Id == sessionId);
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
