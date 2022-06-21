using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using Letscode_Cinema.Views;
using Letscode_Cinema.Models;

namespace Letscode_Cinema
{
    public class Program
    {
        public static void Main()
        {
            Database db = new Database();
            db.CreateFiles();

            Login login = new Login();
            login.Show();

            User user = new User();

            bool chosen = false;
            while (!chosen)
            {
                MovieList movieList = new MovieList();
                Movie movie = movieList.ChooseMovie();

                SessionList sessionList = new SessionList();
                try
                {
                    Session session = sessionList.ChooseSession(movie, user);
                    if (session != null)
                    {
                        Console.WriteLine("Sessao " + session.Id);
                        chosen = true;
                    }

                    SeatList seatList = new SeatList();
                    List<int[]> chosenSeats = seatList.ChooseSeats(session);

                    FoodList foodList = new FoodList();
                    Dictionary<int, int> chosenFoods = foodList.ShowFoods();

                    TicketList ticketList = new TicketList(session, chosenSeats, chosenFoods);
                    ticketList.ShowTicket();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
