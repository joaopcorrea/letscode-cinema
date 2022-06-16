﻿using Letscode_Cinema.Models;
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

                file = $"{dir}/ticket";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    File.WriteAllText(file, "[]");
                }

                file = $"{dir}/session";
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                    FillSessions(file);
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
                new Food()
                {
                    Id = 1,
                    Description = "Pipoca",
                    Price = 12
                },

                new Food()
                {
                    Id = 2,
                    Description = "Coca-Cola",
                    Price = 5
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
                List<Session> sessions = new List<Session>()
            {
                new Session()
                {
                    Id = 1,
                    CinemaName = "Cinemark",
                    City = "São Paulo",
                    RoomName = "Sala 1",
                    Columns = 10,
                    Rows = 20,
                    Date = DateTime.Now.AddDays(5),
                    Is3d = false,
                    MovieId = 1,
                    Price = 15,
                    SeatsUserId = new int[10,20]
                },

                new Session()
                {
                    Id = 2,
                    CinemaName = "Cinemark",
                    City = "São Paulo",
                    RoomName = "Sala 2",
                    Columns = 20,
                    Rows = 20,
                    Date = DateTime.Now.AddDays(5),
                    Is3d = true,
                    MovieId = 2,
                    Price = 30,
                    SeatsUserId = new int[20,20]
                },

                new Session()
                {
                    Id = 3,
                    CinemaName = "Cinemark",
                    City = "São Paulo",
                    RoomName = "Sala 1",
                    Columns = 10,
                    Rows = 20,
                    Date = DateTime.Now.AddDays(6),
                    Is3d = false,
                    MovieId = 3,
                    Price = 15,
                    SeatsUserId = new int[10,20]
                },

                new Session()
                {
                    Id = 4,
                    CinemaName = "Cinemark",
                    City = "São Paulo",
                    RoomName = "Sala 2",
                    Columns = 20,
                    Rows = 20,
                    Date = DateTime.Now.AddDays(6),
                    Is3d = true,
                    MovieId = 1,
                    Price = 30,
                    SeatsUserId = new int[10,20]
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
            List<Session> sessions = new List<Session>();
            return sessions;
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

        public static List<Food> GetFoods()
        {
            string file = "database/food";
            string json = File.ReadAllText(file);

            List<Food> foods = JsonConvert.DeserializeObject<List<Food>>(json);
            return foods;
        }

        public static List<Ticket> GetTickets()
        {
            string file = "database/ticket";
            string json = File.ReadAllText(file);

            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            return tickets;
        }
    }
}