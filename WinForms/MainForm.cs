using LibraryManager.Core.Models;
using LibraryManager.Core.ViewModels;
using LibraryManager.Data.Data;

namespace WinForms
{
	public partial class MainForm : Form
	{
		private MainViewModel vm;

		public MainForm()
		{
			InitializeComponent();
			var repo = RepositoryFactory.Create("sql");
			vm = new MainViewModel(repo);
			LoadData();
		}

		private void LoadData()
		{
			dgvSongs.DataSource = null;
			dgvSongs.DataSource = vm.Songs.ToList();

			dgvUsers.DataSource = null;
			dgvUsers.DataSource = vm.Users.ToList();
		}

		private void AddSong_Click(object sender, EventArgs e)
		{
			var song = new Song
			{
				Title = string.IsNullOrWhiteSpace(Title.Text) ? "Unknown Title" : Title.Text.Trim(),
				Artist = string.IsNullOrWhiteSpace(Artist.Text) ? "Unknown Artist" : Artist.Text.Trim(),
				Year = int.TryParse(Year.Text, out int y) ? y : 0,
				Genre = string.IsNullOrWhiteSpace(Genre.Text) ? "Unknown" : Genre.Text.Trim()
			};

			vm.AddSong(song);
			LoadData();
		}

		private void DeleteSong_Click_1(object sender, EventArgs e)
		{
			if (dgvSongs.CurrentRow == null) return;

			var song = (Song)dgvSongs.CurrentRow.DataBoundItem;
			vm.SelectedSong = song;
			vm.DeleteSong();
			LoadData();
		}

		private void AddUser_Click_1(object sender, EventArgs e)
		{
		
			var user = new User
			{
				Username = string.IsNullOrWhiteSpace(Username.Text) ? "Unknown" : Username.Text.Trim(),
				Password = Password.Text,
				Email = string.IsNullOrWhiteSpace(Email.Text) ? "" : Email.Text.Trim()
			};

			vm.AddUser(user);
			LoadData();
		}

		private void DeleteUser_Click_1(object sender, EventArgs e)
		{
			if (dgvUsers.CurrentRow == null) return;

			var user = (User)dgvUsers.CurrentRow.DataBoundItem;
			vm.SelectedUser = user;
			vm.DeleteUser();
			LoadData();
		}
	}
}