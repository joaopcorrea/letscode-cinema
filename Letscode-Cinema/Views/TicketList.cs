using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    public class TicketList : Menu
    {
        Cart cart;

        Ticket ticket = new Ticket();

        public TicketList(Cart cart)
        {
            this.cart = cart;
        }

        public void ListTickets()
        {
            var tickets = Database.GetTickets(cart.UserId);

            bool exit = false;
            do
            {

                foreach (var t in tickets)
                {
                    Console.WriteLine($"Ticket N {t.Id}");
                }

                Console.WriteLine("Digite o número do ticket: ");

                string num = Console.ReadLine();

                var ticket = tickets.FirstOrDefault(t => t.Id.ToString() == num);
                if (ticket == null)
                {
                    throw new NullReferenceException("Ticket não existente!");
                }

                ShowTicket(ticket);
            }
            while (!exit);

        }

        private void ShowTicket(Ticket ticket)
        {
            User user = Database.GetUser(ticket.UserId);

            Console.WriteLine($"Número: {ticket.Id}");
            Console.WriteLine($"Data: {ticket.Date}");
            Console.WriteLine($"Preço: {ticket.Price}");
            Console.WriteLine($"Usuário: {user.Name}");

            foreach (var item in ticket.Items)
            {
                Console.WriteLine($"{item.Id}\t{item.Quantity}x\t{item.Description}\t{item.TotalPrice}");
            }

            //Console.WriteLine("Comida: ");
            //double totalFoodValue = 0;
            //foreach (var food in ticket.Foods)
            //{
            //    if (food.Key == 1)
            //    {
            //        Console.WriteLine($"{food.Key} Pipoca Salgada --- "
            //            + $"Quantidade: {food.Value} --- Preço: {food.Value * 12}");
            //        totalFoodValue += food.Value * 12;
            //    }
            //    else if (food.Key == 2)
            //    {
            //        Console.WriteLine($"{food.Key} Pipoca Doce ------ "
            //            + $"Quantidade: {food.Value} --- Preço: {food.Value * 14}");
            //        totalFoodValue += food.Value * 14;
            //    }
            //    else if (food.Key == 3)
            //    {
            //        Console.WriteLine($"{food.Key} Coca-Cola -------- "
            //           + $"Quantidade: {food.Value} --- Preço: {food.Value * 5}");
            //        totalFoodValue += food.Value * 5;
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{food.Key} Coca-Cola Diet --- "
            //           + $"Quantidade: {food.Value} --- Preço: {food.Value * 4.5}");
            //        totalFoodValue += food.Value * 4.5;
            //    }

            //    Console.ReadLine();
            //}
        }

        public void CreateTicket()
        {
            try
            {
                Ticket ticket = new Ticket()
                { 
                    SessionId = cart.SessionId,
                    UserId = cart.UserId,
                    Items = cart.Items
                };

                ticket.Date = DateTime.Now;
                ticket.Price = cart.Items.Sum(c => c.TotalPrice);

                Database.CreateTicket(ticket);

                Console.WriteLine("Ticket criado com sucesso!");

                ShowTicket(ticket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        //public TicketList(Session pSession, List<int[]> pChosenSeats, Dictionary<int, int> pChosenFoods)
        //{
        //    //ticket.UserId = pUser;
        //    ticket.SessionId = pSession;
        //    ticket.Seats = pChosenSeats;
        //    ticket.FoodIds = pChosenFoods;
        //}
        //    Console.WriteLine("|\n| Valor total da compra: " 
        //        + (totalFoodValue + ticket.Price).ToString("C") + "\n");
        //}
    }
}
