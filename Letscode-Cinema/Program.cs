using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using Letscode_Cinema.Views;

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

            Menu menu = new Menu();
            string option;
            do
            {
                menu.DrawMainMenu();
                option = Console.ReadLine();

                Console.Clear();
                switch (option)
                {
                    case "1":
                        try
                        {
                            MovieList movieList = new MovieList();
                            SessionList sessionList = new SessionList();
                            Movie movie = null;
                            do
                            {
                                movie = movieList.ChooseMovie(user);
                                if (movie != null)
                                {
                                    Session session = null;
                                    do
                                    {
                                        session = sessionList.ChooseSession(movie);
                                        if (session != null)
                                        {
                                            CartList cart = new CartList(user.Id);
                                            cart.ChangeCartSession(session.Id);

                                            SeatList seatList = new SeatList();
                                            seatList.ChooseSeats(user.Id, session);
                                        }
                                    } while (session != null);
                                }
                            } while (movie != null);
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
                            ChooseFood();
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
                            ShowCart(user);
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
                            SelectTickets(user);
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
            } while (option != "5");
        }

        private static void SelectTickets(User user)
        {
            Cart cart = Database.GetCart(user.Id);
            TicketList ticketList = new TicketList(cart);
            ticketList.ListTickets();
        }

        private static void ShowCart(User user)
        {
            CartList cartList = new CartList(user.Id);
            cartList.ShowCart();
        }

        private static void ChooseFood()
        {
            FoodList foodList = new FoodList();
            Dictionary<int, int> chosenFoods = foodList.ShowFoods();
        }

        private static void ChooseMovie(User user)
        {
            MovieList movieList = new MovieList();
            SessionList sessionList = new SessionList();

            Movie movie = null;
                movie = movieList.ChooseMovie(user);
                if (movie != null)
                {
                    Session session = null;
                    session = sessionList.ChooseSession(movie);
                    if (session != null)
                    {
                        CartList cart = new CartList(user.Id);
                        cart.ChangeCartSession(session.Id);

                        SeatList seatList = new SeatList();
                        seatList.ChooseSeats(user.Id, session);
                    }
                }
        }
    }
}
