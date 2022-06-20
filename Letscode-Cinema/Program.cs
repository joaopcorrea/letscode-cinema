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
            User user = new User();
            do
            {
                user = login.Show();
            } while (user == null);
            Console.Clear();
            //user.BirthDate = DateTime.Now.AddYears(-15);

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
                        //Console.WriteLine("Sessao " + session.Id);
                        SeatList seatList = new SeatList();
                        seatList.ChooseSeats(session);
                        chosen = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //FoodList foodList = new FoodList();
            //Dictionary<int, int> chosenFoods = foodList.ShowFoods();

            //TicketList ticketList = new TicketList(chosenFoods);
            //ticketList.ShowTicket();

        }
    }
}
