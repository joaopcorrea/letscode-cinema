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
        Ticket ticket = new Ticket();
        public TicketList(Dictionary<int, int> pChosenFoods)
        {
            //ticket.SessionId = pSession;
            ticket.FoodIds = pChosenFoods;
        }

        public void ShowTicket()
        {
            //objeto do carrinho, tudo que ele gastou, um por compra,
            //poltronas, valor total, comida
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("|------------------- T I C K E T ---------------------|");
            Console.WriteLine("| Data: " + ticket.Date);
            Console.WriteLine("| Sessão: " + ticket.SessionId);
            //Console.WriteLine("| Assento: " + session.MovieId);
            Console.WriteLine("| Comida: ");
            double totalFoodValue = 0;
            foreach (var food in ticket.FoodIds)
            {
                if (food.Key == 1)
                {
                    Console.WriteLine($"| {food.Key} Pipoca Salgada --- "
                        + $"Quantidade: {food.Value} --- Preço: {food.Value * 12}");
                    totalFoodValue += food.Value * 12;
                }
                else if (food.Key == 2)
                {
                    Console.WriteLine($"| {food.Key} Pipoca Doce ------ "
                        + $"Quantidade: {food.Value} --- Preço: {food.Value * 14}");
                    totalFoodValue += food.Value * 14;
                }
                else if (food.Key == 3)
                {
                    Console.WriteLine($"| {food.Key} Coca-Cola -------- "
                       + $"Quantidade: {food.Value} --- Preço: {food.Value * 5}");
                    totalFoodValue += food.Value * 5;
                }
                else
                {
                    Console.WriteLine($"| {food.Key} Coca-Cola Diet --- "
                       + $"Quantidade: {food.Value} --- Preço: {food.Value * 4.5}");
                    totalFoodValue += food.Value * 4.5;
                }
            }
            Console.WriteLine("| Valor total da compra: " 
                + (totalFoodValue + ticket.Price).ToString("C"));
        }
    }
}
