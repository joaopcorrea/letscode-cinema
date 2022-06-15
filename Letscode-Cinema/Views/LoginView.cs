using Letscode_Cinema.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Views
{
    internal class LoginView : MenuView
    {
        public void Show()
        {
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
                        Login();
                        break;

                    case "2":
                        Signup();
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
            
        }

        private void Login()
        {
            try
            {
                string email, senha;
                Console.WriteLine("Digite o email: ");
                email = Console.ReadLine();
                Console.WriteLine("Digite a senha: ");
                senha = Console.ReadLine();

                if (!Directory.Exists("database"))
                    Directory.CreateDirectory("database");
                if (!File.Exists("database/user.db"))
                {
                    File.Create("database/user.db");
                    File.WriteAllText("database/user.db", "[]");
                }

                string json = File.ReadAllText("database/user.db");

                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

                User user = users.Where(u => u.Email == email && u.Password == senha).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("Credenciais inválidas!");
                }

                Console.WriteLine("Usuário encontrado!");
                Console.WriteLine($"Id:{user.Id}\nNome:{user.Name}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                Console.ReadLine();
            }
        }

        private void Signup()
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

                if (!Directory.Exists("database"))
                    Directory.CreateDirectory("database");
                if (!File.Exists("database/user.db"))
                {
                    File.Create("database/user.db");
                    File.WriteAllText("database/user.db", "[]");
                }

                string json = File.ReadAllText("database/user.db");

                List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                users.Add(new User
                {
                    Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1,
                    Email = email,
                    Password = password,
                    Name = username,
                    CPF = cpf,
                    BirthDate = birthDate,
                    IsStudent = isStudent,
                    IsAdmin = isAdmin
                });

                json = JsonConvert.SerializeObject(users);
                File.WriteAllText("database/user.db", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
                Console.ReadLine();
            }
        }
    }
}
