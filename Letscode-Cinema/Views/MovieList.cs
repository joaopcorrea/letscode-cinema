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
    public class MovieList : Menu
    {
        List<Movie> movies = Database.GetMovies();

        public void ShowMovies()
        {
            foreach (Movie movie in movies)
            {
                Console.WriteLine($"{movie.Id}. {movie.Title}");
                Console.WriteLine($"    Genero: {movie.Gender}");
                Console.WriteLine($"    Minutos: {movie.Minutes}");
                Console.WriteLine($"    Classificacao indicativa: +{movie.MinimumAge}");
                Console.WriteLine($"    Avaliacao: {movie.Review}");
            }
        }

        public Movie ChooseMovie()
        {
            this.ShowMovies();
            Console.WriteLine();

            bool choiseIsInt = false;
            int movieId = 0;
            Movie movie = new Movie();
            do
            {
                Console.Write("Escolha um filme: ");
                string choise = Console.ReadLine();
                choiseIsInt = Int32.TryParse(choise, out movieId);
                if (!choiseIsInt)
                    Console.Write("Digite o numero do filme. ");
                else
                {
                    movie = Database.GetMovies().FirstOrDefault(movie => movie.Id == movieId);
                    if (movie == null)
                        Console.Write("Opcao invalida. ");
                }
            } while (!choiseIsInt || movie == null);
            Console.Clear();

            return movie;
        }
    }
}
