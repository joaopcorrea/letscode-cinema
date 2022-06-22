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

        //parametro como carrinho, somente colocar um for de quantidade, descrição
        //lista antes com os tickets que possuem, imprimir o ticket
        //Chamar método da database de criação de ticket, vincular com a classe ticket
        //Receber ojeto da classe carrinho, pegar infos, novo objeto de ticket mandar pra database
        //Carrinho.items.Quantidade = gerar ticket que via para o database
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

                Console.Clear();
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

            Console.ReadLine();
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
