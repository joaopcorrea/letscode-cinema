using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    internal class CartList : Menu
    {
        int userId;
        Session session;

        public CartList(int userId)
        {
            this.userId = userId;
        }

        public bool ShowCart()
        {
            bool exit = false;
            do
            {
                Cart cart = GetCart();
                double totalPrice = 0;

                if (cart.Items.Count > 0)
                {
                    DrawMenu("Carrinho");
                    DrawSessionMenu(cart.SessionId);

                    Console.WriteLine("Id\tQtd\tDescrição\tTotal");
                    foreach (var item in cart.Items)
                    {
                        Console.WriteLine($"{item.Id}\t{item.Quantity}x\t{item.Description,-15}\tR$ {item.TotalPrice}");
                        totalPrice += item.TotalPrice;
                    }

                    Console.WriteLine($"\nValor total: R$ {totalPrice:N2}\n");

                    Console.WriteLine("Escolha uma opção: \n");
                    Console.WriteLine("0. Voltar");
                    Console.WriteLine("1. Remover item");
                    Console.WriteLine("2. Finalizar compra");

                    string opcao = Console.ReadLine();
                    switch (opcao)
                    {
                        case "0":
                            return false;

                        case "1":
                            RemoveItem();
                            break;

                        case "2":
                            exit = FinishCart();
                            break;

                        default:
                            Console.WriteLine("Opção inválida!");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    DrawMenu("Carrinho Vazio!");
                    Console.ReadLine();
                    exit = true;
                }
            }
            while (!exit);

            return true;
        }

        private bool FinishCart()
        {
            Console.WriteLine("Tem certeza que deseja finalizar a compra? (S / N)");
            if (Console.ReadLine().ToUpper() != "S")
                return false;

            try
            {
                Console.WriteLine("Criando ticket!");
                Console.ReadLine();

                Cart cart = Database.GetCart(userId);
                TicketList ticketList = new TicketList(cart);
                ticketList.CreateTicket();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return true;
        }

        private void RemoveItem()
        {
            Cart cart = GetCart();

            Console.WriteLine("Digite o Id de um item para remover: ");

            string id = Console.ReadLine();

            var item = cart.Items.FirstOrDefault(i => i.Id.ToUpper() == id.ToUpper());
            if (item == null)
            {
                Console.WriteLine("Id não encontrado no carrinho!");
                Console.ReadLine();
                return;
            }

            Database.RemoveCartItem(userId, item);

            Console.WriteLine("Id removido com sucesso!");
            Console.ReadLine();
        }

        private Cart GetCart()
        {
            Cart cart = Database.GetCart(userId);

            return cart;
        }

        public bool AddItemToCart(Cart.CartItem item)
        {
            Database.AddCartItem(userId, item);

            return true;
        }

        public bool ChangeCartSession(int sessionId)
        {
            Database.ChangeCartSession(userId, sessionId);

            return true;
        }
    }
}
