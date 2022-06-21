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
            try
            {
                string email, senha;
                Console.WriteLine("Digite o email: ");
                email = Console.ReadLine();
                Console.WriteLine("Digite a senha: ");
                senha = Console.ReadLine();

                User user = Database.GetUsers().FirstOrDefault(u => u.Email == email && u.Password == senha);
                if (user == null)
                {
                    throw new Exception("Credenciais inválidas!");
                }

                Console.WriteLine("Usuário encontrado!");
                Console.WriteLine($"Id:{user.Id}\nNome:{user.Name}");
                Console.ReadLine();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.ReadLine();
            }

            return null;
        }

        private User SignUp()
        {
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

                User user = new User
                {
                    Email = email,
                    Password = password,
                    Name = username,
                    CPF = cpf,
                    BirthDate = birthDate,
                    IsStudent = isStudent,
                    IsAdmin = isAdmin
                };

                Database.AddUser(user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
                Console.ReadLine();
            }

            return null;
        }
    }
}
