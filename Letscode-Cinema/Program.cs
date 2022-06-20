using Letscode_Cinema.Models;
using Letscode_Cinema.Services;
using Letscode_Cinema.Views;

Database db = new Database();
db.CreateFiles();

User user = new User();
user.BirthDate = DateTime.Now.AddYears(-15);

Login login = new Login();
login.Show();

bool chosen = false;
while (!chosen)
{
    MovieList movieList = new MovieList();
    Movie movie = movieList.ChooseMovie();

    SessionList sessionList = new SessionList();
    try
    {
        Session session = sessionList.ChooseSession(movie, user);
        if (session != null)
        {
            Console.WriteLine("Sessao " + session.Id);
            chosen = true;
        }

        SeatList seatList = new SeatList();
        seatList.ChooseSeats(1);

        FoodList foodList = new FoodList();
        foodList.ShowFoods();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}