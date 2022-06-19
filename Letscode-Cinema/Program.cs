using Letscode_Cinema.Services;
using Letscode_Cinema.Views;

Database db = new Database();
db.CreateFiles();

//SeatList seatList = new SeatList();
//seatList.ShowSeats(1);

//Login login = new Login();
//login.Show();

//User user = new User();
//user.BirthDate = DateTime.Now.AddYears(-15);

//bool chosen = false;
//while (!chosen)
//{
//    MovieList movieList = new MovieList();
//    Movie movie = movieList.ChooseMovie();

//    SessionList sessionList = new SessionList();
//    try
//    {
//        Session session = sessionList.ChooseSession(movie, user);
//        if (session != null)
//        {
//            Console.WriteLine("Sessao " + session.Id);
//            chosen = true;
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

FoodList foodList = new FoodList();
Dictionary<int, int> chosenFoods = foodList.ShowFoods();

TicketList ticketList = new TicketList(chosenFoods);
ticketList.ShowTicket();