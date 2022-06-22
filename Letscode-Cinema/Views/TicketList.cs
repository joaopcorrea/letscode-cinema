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
        List <Food> foods;

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
            Session session = Database.GetSession(ticket.SessionId);

            DrawMenu($"TICKET Nº {ticket.Id}");
            Console.WriteLine($"{session.Id}. {session.Room.Cinema.CinemaName}, {session.Room.Cinema.City} - {session.Room.RoomName}");
            Console.WriteLine($"Data: {session.Date.ToString("d")}");
            Console.WriteLine($"Horário: +{session.Date.ToString("t")}");
            //Console.WriteLine(cart.SessionId);
            //Console.ReadLine();
            //DrawSessionMenu(cart.SessionId);
            Console.WriteLine($"Preço: {ticket.Price}");
            Console.WriteLine($"Usuário: {user.Name}");
            Console.WriteLine("Id\tQtd\tDescrição\tTotal");
            foreach (var item in ticket.Items.OrderBy(t => t.Description))
            {
                Console.WriteLine($"{item.Id}\t{item.Quantity}x\t{item.Description}\t"
                    + $"        {item.TotalPrice.ToString("C")}");
            }
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
                UpdateSessionSeats(ticket.SessionId, ticket);

                Console.WriteLine("Ticket criado com sucesso!");

                ShowTicket(ticket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void UpdateSessionSeats(int sessionId, Ticket ticket)
        {
            Session session = Database.GetSession(sessionId);

            List<int[]> seats = new List<int[]>();

            foreach (var item in ticket.Items)
            {
                if (item.Description == "ASSENTO")
                {
                    seats.Add(GetSeatIndexes(item.Id));
                }
            }

            foreach (var seat in seats)
            {
                session.SeatsUserId[seat[0], seat[1]] = ticket.UserId;
            }

            Database.UpdateSessionSeats(sessionId, session.SeatsUserId);
        }

        private int[] GetSeatIndexes(string seat)
        {
            int[] indexes = new int[2];

            indexes[0] = char.Parse(seat[..1].ToUpper()) - 65;
            indexes[1] = Convert.ToInt32(seat[1..]);

            return indexes;
        }
    }
}
