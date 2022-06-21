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

            User user;

            Login login = new Login();
            do
            {
                user = login.Show();
            } while (user == null);

            string option;
            do
            {
                DrawOptions();

                option = Console.ReadLine();

                Console.Clear();
                switch (option)
                {
                    case "1":
                        try
                        {
                            MovieList movieList = new MovieList();
                            Movie movie = movieList.ChooseMovie();

                            SessionList sessionList = new SessionList();

                            Session session = sessionList.ChooseSession(movie, user);
                            if (session != null)
                            {
                                CartList cart = new CartList(user.Id);
                                cart.ChangeCartSession(session.Id);

                                SeatList seatList = new SeatList();
                                seatList.ChooseSeats(user.Id, session);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao escolher filme: " + ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                        break;

                    case "2":
                        try
                        {
                            FoodList foodList = new FoodList();
                            Dictionary<int, int> chosenFoods = foodList.ShowFoods();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao escolher comida: " + ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                        break;

                    case "3":
                        try
                        {
                            CartList cartList = new CartList(1);
                            cartList.ShowCart();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao visualizar carrinho: " + ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                        break;

                    case "4":
                        try
                        {
                            //TicketList ticketList = new TicketList(chosenFoods);
                            //ticketList.ShowTicket();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Erro ao consultar tickets: " + ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                        break;

                    case "5":
                        Console.WriteLine("Você escolheu sair!");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                Console.ReadLine();
            }
            while (option != "5");
        }

        private static void DrawOptions()
        {
            Console.Clear();
            Console.WriteLine("Digite uma opção: \n");
            Console.WriteLine("1. Escolher Filme");
            Console.WriteLine("2. Escolher Comida");
            Console.WriteLine("3. Visualizar Carrinho");
            Console.WriteLine("4. Consultar Tickets");
            Console.WriteLine("5. Sair");
            Console.WriteLine();
        }
    }
}
