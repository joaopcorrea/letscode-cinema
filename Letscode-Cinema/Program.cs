using Letscode_Cinema.Services;
using Letscode_Cinema.Views;

Database db = new Database();
db.CreateFiles();

Login login = new Login();
login.Show();