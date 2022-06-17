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
        public void ShowFoods()
        {
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

            do
            {
                DrawMenu("Escolher comida");

                Dictionary<int, double> dictChooseFood = new Dictionary<int, double>(4);
                dictChooseFood.Add(1, 12);
                dictChooseFood.Add(2, 14);
                dictChooseFood.Add(3, 5);
                dictChooseFood.Add(4, 4.5);

                List<Food> foodList = Database.GetFoods();
                foreach (var food in foodList)
                {
                    Console.WriteLine(food.Id + " ------ " + food.Description
                         + " ------ " + food.Price.ToString("C"));
                }
                Console.WriteLine();
                Console.WriteLine("Escolha o número da comida que deseja: ");
                int.TryParse(Console.ReadLine(), out chooseFood);

                Console.WriteLine("Digite a quantidade que deseja: ");
                int.TryParse(Console.ReadLine(), out int quantity);

                if (!dictChooseFood.TryGetValue(chooseFood, out price))
                {
                    Console.WriteLine("Opção inválida! Tente novamente...");
                }
                else
                {
                    switch (chooseFood)
                    {
                        case 1:
                            quantitySaltyPopCorn = quantitySaltyPopCorn + quantity;
                            priceSaltyPopCorn = quantitySaltyPopCorn * price;
                            validateFood[0] = "Pipoca ------------- " + quantitySaltyPopCorn + " ------------- " + priceSaltyPopCorn.ToString("C");
                            break;
                        case 2:
                            quantitySweetPopCorn = quantitySweetPopCorn + quantity;
                            priceSweetPopCorn = quantitySweetPopCorn * price;
                            validateFood[1] = "Pipoca Doce -------- " + quantitySweetPopCorn + " ------------- " + priceSweetPopCorn.ToString("C");
                            break;
                        case 3:
                            quantityCoke = quantityCoke + quantity;
                            priceCoke = quantityCoke * price;
                            validateFood[2] = "Coca-Cola ---------- " + quantityCoke + " ------------- " + priceCoke.ToString("C");
                            break;
                        case 4:
                            quantityDietCoke = quantityDietCoke + quantity;
                            priceDietCoke = quantityDietCoke * price;
                            validateFood[3] = "Coca-Cola Diet ----- " + quantityDietCoke + " ------------- " + priceDietCoke.ToString("C");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("================== PEDIDO ===================");
                Console.WriteLine("Produto -------- Quantidade -------- Valor ");
                foreach (string item in validateFood)
                {
                    if (item != null)
                    {
                        Console.WriteLine(item);
                    }
                }
                Console.WriteLine();
            }
            while (chooseFood != 1 || chooseFood != 2 || chooseFood != 3 || chooseFood != 4);
        }
    }
}
