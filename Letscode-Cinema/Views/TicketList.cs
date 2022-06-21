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
        public TicketList(User pUserLogin, Session pSession, List<int[]> pChosenSeats, Dictionary<int, int> pChosenFoods)
        {
            ticket.UserId = pUserLogin.Id;
            ticket.SessionId = pSession.Id;
            //ticket.Seats = pChosenSeats;
            ticket.FoodIds = pChosenFoods;
        }

        public void ShowTicket()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("|------------------- T I C K E T ---------------------|");
            Console.WriteLine("| Usuário: " + ticket.UserId);
            Console.WriteLine("| Data: " + ticket.Date);
            Console.WriteLine("| Sessão: " + ticket.SessionId);
            Console.Write("| Assento: " );
            //foreach (var seat in ticket.Seats)
            //{
            //    const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //    Console.WriteLine(string.Join("", seat));
            //}

            //foreach (int[] seat in ticket.Seats)
            //{
            //    Console.Write($"{seat[0]}{seat[1]}, ");
            //}
            Console.WriteLine();
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
            Console.WriteLine("|\n| Valor total da compra: " 
                + (totalFoodValue + ticket.Price).ToString("C") + "\n");
        }
    }
}
