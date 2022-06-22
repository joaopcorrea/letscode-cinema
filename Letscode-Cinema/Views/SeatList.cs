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
        public bool ChooseSeats(int userId, Session session)
        {
            List<int[]> seatsChosen = new List<int[]>();      
            List<int[]> seatsInCart = new List<int[]>();

            Cart c = Database.GetCart(userId);

            foreach (var item in c.Items)
            {
                if (item.Description == "ASSENTO")
                {
                    seatsInCart.Add(GetSeatIndexes(item.Id));
                }
            }

            bool exit = false;
            do
            {
                try
                {
                    DrawMenu("Escolher Assentos");
                    DrawSessionMenu(session.Id);

                    if (seatsInCart.Count > 0 || seatsChosen.Count > 0)
                    {
                        Console.Write("Assentos escolhidos: ");
                        string chosenSeats = "";

                        foreach (var s in seatsInCart)
                        {
                            chosenSeats += $"{GetSeatName(s[0], s[1])}, ";
                        }

                        foreach (var s in seatsChosen)
                        {
                            chosenSeats += $"{GetSeatName(s[0], s[1])}, ";
                        }
                        
                        chosenSeats = chosenSeats.Remove(chosenSeats.Length - 2);
                        Console.WriteLine(chosenSeats);
                        Console.WriteLine();
                    }

                    Console.WriteLine("Assentos disponíveis:");

                    DrawSeats(session);

                    Console.WriteLine("\n0. Voltar");
                    if (seatsChosen.Count > 0)
                        Console.WriteLine("\n1. Continuar");
                    Console.WriteLine();

                    Console.Write("Escolha um assento: ");

                    string seat = Console.ReadLine();
                    if (seat == "0")
                        return false;
                    else if (seat == "1" && seatsChosen.Count > 0)
                    {
                        exit = true;
                        continue;
                    }
                    else if (string.IsNullOrEmpty(seat))
                        throw new FormatException();

                    int[] indexes = GetSeatIndexes(seat);

                    if (session.SeatsUserId[indexes[0], indexes[1]] > 0)
                    {
                        Console.WriteLine("Assento ocupado, escolha outro!");
                        Console.ReadLine();
                    }
                    else if (seatsChosen.Exists(i => i[0] == indexes[0] && i[1] == indexes[1]) ||
                             seatsInCart.Exists(i => i[0] == indexes[0] && i[1] == indexes[1]))
                    {
                        Console.WriteLine("Você já escolheu esse assento, escolha outro!");
                        Console.ReadLine();
                    }
                    else
                    {
                        

                        seatsChosen.Add(indexes);
                        Console.WriteLine("Gostaria de selecionar outro assento? (S / N)");

                        if (Console.ReadLine().ToUpper() != "S")
                        {
                            exit = true;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Assento inválido!");
                    Console.ReadLine();
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Assento inválido!");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadLine();
                }
            }
            while (!exit);

            CartList cart = new CartList(userId);
            foreach (var seat in seatsChosen)
            {
                Cart.CartItem item = new Cart.CartItem()
                {
                    Id = GetSeatName(seat[0], seat[1]),
                    Description = "ASSENTO",
                    Quantity = 1,
                    Price = session.Price,
                    TotalPrice = session.Price
                };

                cart.AddItemToCart(item);
            }

            return true;
        }

        private void DrawSeats(Session session)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
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
                    text += session.SeatsUserId[row, column] > 0 ? "    " : $"{GetSeatName(row, column)}  ";
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

        private int[] GetSeatIndexes(string seat)
        {
            int[] indexes = new int[2];

            indexes[0] = char.Parse(seat[..1].ToUpper()) - 65;
            indexes[1] = Convert.ToInt32(seat[1..]);

            return indexes;
        }

        private string GetSeatName(int row, int column)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return $"{letters[row]}{column}";
        }
    }
}
