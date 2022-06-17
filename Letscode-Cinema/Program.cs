using Letscode_Cinema.Services;
using Letscode_Cinema.Views;

Database db = new Database();
db.CreateFiles();

SeatList seatList = new SeatList();
seatList.ShowSeats(1);

Login login = new Login();
login.Show();

FoodList foodList = new FoodList();
foodList.ShowFoods();