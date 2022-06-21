using Letscode_Cinema.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letscode_Cinema.Services
{
    public class Database
    {
        public bool CreateFiles()
        {
            try
            {
                string dir, file;

                dir = "database";
                if (!Directory.Exists("database"))
                    Directory.CreateDirectory("database");


                file = $"{dir}/user";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, "[]");
                }

                file = $"{dir}/movie";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    FillMovies(file);
                }

                file = $"{dir}/food";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    FillFoods(file);
                }

                file = $"{dir}/session";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    FillSessions(file);
                }

                file = $"{dir}/cart";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, "[]");
                }

                file = $"{dir}/ticket";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, "[]");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Erro: " + ex.Message);
                Console.ReadLine();
                return false;
            }
        }

        private bool FillMovies(string fileName)
        {
            try
            {
                List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
                    Title = "O Homem do Norte",
                    Description = "",
                    Gender = "Ação",
                    MinimumAge = 18,
                    Minutes = 137,
                    Review = 7.3
                },

                new Movie()
                {
                    Id = 2,
                    Title = "Assassino Sem Rastro",
                    Description = "",
                    Gender = "Crime",
                    MinimumAge = 16,
                    Minutes = 114,
                    Review = 7.3
                },

                new Movie()
                {
                    Id = 3,
                    Title = "Jurassic World: Domínio",
                    Description = "",
                    Gender = "Aventura",
                    MinimumAge = 12,
                    Minutes = 146,
                    Review = 6.7
                }
            };

                string json = JsonConvert.SerializeObject(movies);
                File.WriteAllText(fileName, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Erro: " + ex.Message);
                Console.ReadLine();
                return false;
            }
        }

        private bool FillFoods(string fileName)
        {
            try
            {
                List<Food> foods = new List<Food>()
            {
                new PopCorn()
                {
                    Id = 1,
                    Description = "Pipoca Salgada",
                    Price = 12,
                    PopCornIsSalty = true,
                },

                new PopCorn()
                {
                    Id = 2,
                    Description = "Pipoca Doce",
                    Price = 14,
                    PopCornIsSalty = false,
                },
                new Soda()
                {
                    Id = 3,
                    Description = "Coca-Cola",
                    Price = 5,
                    IsDiet = false,
                },
                new Soda()
                {
                    Id = 4,
                    Description = "Coca-Cola Diet",
                    Price = 4.5,
                    IsDiet = true,
                }
            };

                string json = JsonConvert.SerializeObject(foods);
                File.WriteAllText(fileName, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Erro: " + ex.Message);
                Console.ReadLine();
                return false;
            }
        }

        private bool FillSessions(string fileName)
        {
            try
            {
                Cinema cinema = new Cinema()
                {
                    CinemaName = "Cinemark",
                    City = "São Paulo"
                };

                Room sala1 = new Room()
                {
                    Cinema = cinema,
                    RoomName = "Sala 1",
                    Columns = 10,
                    Rows = 20,
                };

                Room sala2 = new Room()
                {
                    Cinema = cinema,
                    RoomName = "Sala 2",
                    Columns = 20,
                    Rows = 20,
                };

                List<Session> sessions = new List<Session>()
                {
                    new Session()
                    {
                        Id = 1,
                        Room = sala1,
                        Date = DateTime.Now.AddDays(5),
                        MovieId = 1,
                        Price = 15,
                        SeatsUserId = new int[sala1.Rows,sala1.Columns]
                    },

                    new Session()
                    {
                        Id = 2,
                        Room = sala2,
                        Date = DateTime.Now.AddDays(5),
                        MovieId = 2,
                        Price = 30,
                        SeatsUserId = new int[sala2.Rows,sala2.Columns]
                    },

                    new Session()
                    {
                        Id = 3,
                        Room = sala1,
                        Date = DateTime.Now.AddDays(6),
                        MovieId = 3,
                        Price = 15,
                        SeatsUserId = new int[sala1.Rows,sala1.Columns]
                    },

                    new Session()
                    {
                        Id = 4,
                        Room = sala2,
                        Date = DateTime.Now.AddDays(6),
                        MovieId = 1,
                        Price = 30,
                        SeatsUserId = new int[sala2.Rows,sala2.Columns]
                    },
                };

                string json = JsonConvert.SerializeObject(sessions);
                File.WriteAllText(fileName, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Erro: " + ex.Message);
                Console.ReadLine();
                return false;
            }
        }

        public static List<Session> GetSessions()
        {
            string file = "database/session";
            string json = File.ReadAllText(file);

            List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(json);
            return sessions;
        }

        public static Session GetSession(int id)
        {
            Session session = GetSessions().FirstOrDefault(s => s.Id == id);
            return session;
        }

        public static List<User> GetUsers()
        {
            string file = "database/user";
            string json = File.ReadAllText(file);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        public static bool AddUser(User user)
        {
            List<User> users = GetUsers();
            user.Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;
            users.Add(user);

            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText("database/user", json);

            return true;
        }

        public static List<Movie> GetMovies()
        {
            string file = "database/movie";
            string json = File.ReadAllText(file);

            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }

        public static Movie GetMovie(int id)
        {
            Movie movie = GetMovies().FirstOrDefault(m => m.Id == id);
            return movie;
        }

        public static List<Food> GetFoods()
        {
            string file = "database/food";
            string json = File.ReadAllText(file);

            List<Food> foods = JsonConvert.DeserializeObject<List<Food>>(json);
            return foods;
        }

        public static Food GetFood(int id)
        {
            Food food = GetFoods().FirstOrDefault(f => f.Id == id);
            return food;
        }

        public static List<Cart> GetCarts()
        {
            string file = "database/cart";
            string json = File.ReadAllText(file);

            List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(json);
            return carts;
        }

        public static Cart GetCart(int userId)
        {
            Cart cart = GetCarts().FirstOrDefault(c => c.UserId == userId);

            return cart;
        }

        public static Cart AddCart(int userId)
        {
            List<Cart> carts = GetCarts();

            Cart cart = new Cart()
            {
                UserId = userId,
                Items = new List<Cart.CartItem>()
            };

            carts.Add(cart);

            string json = JsonConvert.SerializeObject(carts);
            File.WriteAllText("database/cart", json);

            return cart;
        }

        public static bool RemoveCartItem(int userId, Cart.CartItem item)
        {
            List<Cart> carts = GetCarts();
            Cart cart = carts.FirstOrDefault(c => c.UserId == userId);

            cart.Items.RemoveAt(cart.Items.IndexOf(item));

            UpdateCarts(carts);

            return true;
        }

        public static bool AddCartItem(int userId, Cart.CartItem item)
        {
            List<Cart> carts = GetCarts();

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            cart.Items.Add(item);

            UpdateCarts(carts);

            return true;
        }

        public static bool ChangeCartSession(int userId, int sessionId)
        {
            List<Cart> carts = GetCarts();

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            cart.SessionId = sessionId;

            UpdateCarts(carts);

            return true;
        }

        private static bool UpdateCarts(List<Cart> carts)
        {
            string json = JsonConvert.SerializeObject(carts);
            File.WriteAllText("database/cart", json);

            return true;
        }

        public static List<Ticket> GetTicket()
        {
            string file = "database/ticket";
            string json = File.ReadAllText(file);

            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            return tickets;
        }
    }
}
