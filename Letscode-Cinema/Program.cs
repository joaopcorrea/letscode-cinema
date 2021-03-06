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

            Database.GetCart(user.Id);

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
                            ChooseMovie(user);
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
                            ChooseFood(user);
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

        private static void ChooseFood(User user)
        {
            Cart cart = Database.GetCart(user.Id);

            if (cart.SessionId == 0)
            {
                Console.WriteLine("Escolha um filme primeiro!");
                Console.ReadLine();
                return;
            }

            FoodList foodList = new FoodList();
            foodList.ShowFoods(user.Id);
        }

        private static void ChooseMovie(User user)
        {
            Cart c = Database.GetCart(user.Id);
            if (c.Items.Count > 0)
            {
                Console.WriteLine("Você tem itens no carrinho. Ao selecionar uma sessão diferente, " +
                    "o carrinho será esvaziado!\nDeseja continuar? (S/N)");
                string option = Console.ReadLine();
                if (option.ToUpper() != "S")
                    return;
            }


            MovieList movieList = new MovieList();
            SessionList sessionList = new SessionList();
            Movie movie;
            do
            {
                movie = movieList.ChooseMovie(user);
                if (movie != null)
                {
                    Session session;
                    do
                    {
                        session = sessionList.ChooseSession(movie);
                        if (session != null)
                        {
                            CartList cart = new CartList(user.Id);
                            cart.ChangeCartSession(session.Id);

                            SeatList seatList = new SeatList();
                            if (seatList.ChooseSeats(user.Id, session))
                            {
                                Console.WriteLine("Deseja comprar comida? (S/N)");
                                string option = Console.ReadLine();
                                if (option.ToUpper() == "S")
                                    ChooseFood(user);

                                return;
                            }
                        }
                    } while (session != null);
                }
            } while (movie != null);
        }
    }
}
