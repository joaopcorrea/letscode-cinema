using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    internal class Login : Menu
    {
        public User Show()
        {
            User user = new User();
            DrawMenu("Login");

            string opcao = "";

            do
            {
                Console.Clear();
                Console.WriteLine("1. Entrar");
                Console.WriteLine("2. Registrar");
                Console.WriteLine("3. Sair");
                Console.WriteLine();
                Console.Write("Digite uma opção: ");
                opcao = Console.ReadLine();
                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        return SignIn();
                        break;
                    case "2":
                        return SignUp();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente...");
                        Console.ReadLine();
                        break;
                }
            }
            while (opcao != "3");

            return null;
        }

        private User SignIn()
        {
            bool userExists = false;
            bool correctPassword = false;
            string email = "@";
            string senha;
            User user = new User();
            int tries = 1;
            do
            {
                if (!userExists)
                {
                    Console.Write("Digite o email: ");
                    email = Console.ReadLine();
                }
                Console.Write("Digite a senha: ");
                senha = Console.ReadLine();
                user = Database.GetUsers().FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    Console.WriteLine("Usuário não encontrado.");
                    Console.ReadLine();
                    return null;
                }
                else
                {
                    userExists = true;
                    user = Database.GetUsers().FirstOrDefault(u => u.Email == email && u.Password == senha);
                    if (user == null)
                    {
                        tries++;
                        Console.Clear();
                        Console.WriteLine("Senha incorreta.");
                        Console.WriteLine("Digite o email: " + email);
                    }
                    else
                        correctPassword = true;
                }
            } while (!correctPassword && tries <= 3);
            if (tries >= 3)
            {
                Console.Clear();
                return null;
            }
            else
                return user;
        }

        private User SignUp()
        {
            User user = new User();
            try
            {
                string email, password, username;
                long cpf;
                DateTime birthDate;
                bool isStudent, isAdmin;

                Console.WriteLine("Digite o Email: ");
                email = Console.ReadLine();
                Console.WriteLine("Digite a Senha: ");
                password = Console.ReadLine();
                Console.WriteLine("Digite o Nome do Usuário: ");
                username = Console.ReadLine();
                Console.WriteLine("Digite o CPF: ");
                cpf = long.Parse(Console.ReadLine());
                Console.WriteLine("Digite a Data de Nascimento (dd/MM/yyyy): ");
                birthDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);
                Console.WriteLine("O usuário é estudante? (S / N): ");
                isStudent = Console.ReadLine() == "S";
                Console.WriteLine("O usuário é administrador? (S / N): ");
                isAdmin = Console.ReadLine() == "S";

                user.Email = email;
                user.Password = password;
                user.Name = username;
                user.CPF = cpf;
                user.BirthDate = birthDate;
                user.IsStudent = isStudent;
                user.IsAdmin = isAdmin;

                Database.AddUser(user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
                Console.ReadLine();
            }
            return user;
        }
    }
}
