using LibraryManager.Core.Data;
using LibraryManager.Core.Models;
using LibraryManager.Data.Data;
using LibraryManager.WPF;  
using System.Linq;
using System.Windows;

namespace WPF
{
	public partial class LoginWindow : Window
	{
		private IMusicRepository repo = RepositoryFactory.Create("sql");

		public LoginWindow()
		{
			InitializeComponent();
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			string username = txtUsername.Text;
			string password = txtPassword.Password;

			var user = repo.GetUsers().FirstOrDefault(x =>
				x.Username == username &&
				x.Password == password);

			if (user != null)
			{
				// ✅ ВАЖНО: трябва да се подаде user обекта на MainWindow
				MainWindow main = new MainWindow(user);
				main.Show();
				this.Close();
			}
			else
			{
				MessageBox.Show("Invalid username or password!");
			}
		}
	}
}