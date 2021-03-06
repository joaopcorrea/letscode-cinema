using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    public class FoodList : Menu
    {
        public bool ShowFoods(int userId)
        {
            Dictionary<int, int> chosenFood = new Dictionary<int, int>();
            int chooseFood = 0;
            double price = 0;
            int quantitySaltyPopCorn = 0;
            double priceSaltyPopCorn = 0;
            int quantitySweetPopCorn = 0;
            double priceSweetPopCorn = 0;
            int quantityCoke = 0;
            double priceCoke = 0;
            int quantityDietCoke = 0;
            double priceDietCoke = 0;
            string[] validateFood = new string[4];
            string answer = "S";

            do
            {
                DrawMenu("Escolher Comida");

                Dictionary<int, double> dictChooseFood = new Dictionary<int, double>(4);
                dictChooseFood.Add(1, 12);
                dictChooseFood.Add(2, 14);
                dictChooseFood.Add(3, 5);
                dictChooseFood.Add(4, 4.5);

                Console.WriteLine("0 ------ Voltar");
                List<Food> foodList = Database.GetFoods();
                foreach (var food in foodList)
                {
                    Console.WriteLine(food.Id + " ------ " + food.Description
                         + " ------ " + food.Price.ToString("C"));
                }
                Console.WriteLine();
                Console.Write("Escolha o número da comida que deseja: ");
                int.TryParse(Console.ReadLine(), out chooseFood);

                if (chooseFood == 0)
                {
                    return false;
                }

                Console.Write("Digite a quantidade que deseja: ");
                int.TryParse(Console.ReadLine(), out int quantity);

                if (quantity == 0)
                {
                    Console.WriteLine("Valor inválido!");
                    Console.ReadLine();
                    return false;
                }

                if (!dictChooseFood.TryGetValue(chooseFood, out price))
                {
                    Console.WriteLine("Opção inválida! Tente novamente...");
                    continue;
                }
                else
                {
                    switch (chooseFood)
                    {
                        case 1:
                            quantitySaltyPopCorn += quantity;
                            priceSaltyPopCorn = quantitySaltyPopCorn * price;
                            validateFood[0] = "Pipoca ------------- " + quantitySaltyPopCorn + " ------------- " + priceSaltyPopCorn.ToString("C");
                            break;

                        case 2:
                            quantitySweetPopCorn += + quantity;
                            priceSweetPopCorn = quantitySweetPopCorn * price;
                            validateFood[1] = "Pipoca Doce -------- " + quantitySweetPopCorn + " ------------- " + priceSweetPopCorn.ToString("C");
                            break;

                        case 3:
                            quantityCoke += quantity;
                            priceCoke = quantityCoke * price;
                            validateFood[2] = "Coca-Cola ---------- " + quantityCoke + " ------------- " + priceCoke.ToString("C");
                            break;

                        case 4:
                            quantityDietCoke += quantity;
                            priceDietCoke = quantityDietCoke * price;
                            validateFood[3] = "Coca-Cola Diet ----- " + quantityDietCoke + " ------------- " + priceDietCoke.ToString("C");
                            break;

                        default:
                            Console.WriteLine("Opção inválida! Tente novamente...");
                            continue;
                    }

                    if (chosenFood.ContainsKey(chooseFood))
                        chosenFood[chooseFood] += quantity;
                    else
                        chosenFood.Add(chooseFood, quantity);
                }

                Console.Clear();
                Console.WriteLine("--------------- P E D I D O -----------------");
                Console.WriteLine("Produto -------- Quantidade -------- Valor ");
                foreach (string item in validateFood)
                {
                    if (item != null)
                    {
                        Console.WriteLine(item);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Deseja continuar comprando? (S / N)");
                answer = Console.ReadLine().ToUpper();
            }
            while (answer == "S");

            CartList cart = new CartList(userId);

            foreach (var food in chosenFood)
            {
                Food f = Database.GetFood(food.Key);

                Cart.CartItem item = new Cart.CartItem()
                {
                    Id = food.Key.ToString(),
                    Description = f.Description,
                    Quantity = food.Value,
                    Price = f.Price,
                    TotalPrice = f.Price * food.Value
                };

                cart.AddItemToCart(item);
            }

            Console.Clear();

            return true;
        }
    }
}
