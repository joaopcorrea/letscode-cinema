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
                    Console.WriteLine($"Sessao {session.Id}:");
                    Console.WriteLine($"    Cinema: {session.Room.Cinema.CinemaName}, {session.Room.Cinema.City}");
                    Console.WriteLine($"    Data: {session.Date.ToString("d")}");
                    Console.WriteLine($"    Horario: +{session.Date.ToString("t")}");
                    Console.WriteLine($"    Preco: R$ {session.Price:N2}");
                    if (session.Is3d)
                        Console.WriteLine($"    Sessao 3D");
                }
            }
        }

        public Session ChooseSession(Movie movie, User user)
        {
            if ((DateTime.Now - user.BirthDate).Days < movie.MinimumAge*365)
                 throw new Exception($"E preciso ter mais de {movie.MinimumAge} anos para comprar ingressos para o filme {movie.Title}.{System.Environment.NewLine}Escolha outro filme.");
            if (sessions.Count() <= 0)
                throw new Exception($"Nenhuma sessao disponivel para o filme {movie.Title}");
            Console.WriteLine("Sessoes disponiveis para o filme " + movie.Title);
            this.ShowSessions(movie.Id);
            Console.WriteLine("Digite 0 para voltar.");
            Console.WriteLine();

            bool choiseIsInt = false;
            int sessionId = 0;
            Session session = new Session();
            do
            {
                Console.Write("Escolha uma sessao: ");
                string choise = Console.ReadLine();
                choiseIsInt = Int32.TryParse(choise, out sessionId);
                if (!choiseIsInt)
                    Console.Write("Digite o numero da sessao. ");
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
                            Console.Write("Opcao invalida. ");
                    }
                }
            } while (!choiseIsInt || session == null);
            Console.Clear();

            return session;
        }
    }
}
