using LibraryManager.Core.Data;
using LibraryManager.Core.Models;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace LibraryManager.Core.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		private readonly IMusicRepository repo;

		private List<Song> allSongs = new List<Song>();

		public ObservableCollection<Song> Songs { get; set; }
			= new ObservableCollection<Song>();

		public ObservableCollection<User> Users { get; set; }
			= new ObservableCollection<User>();

		public User CurrentUser { get; set; }

		private Song selectedSong;
		public Song SelectedSong
		{
			get => selectedSong;
			set
			{
				selectedSong = value;
				OnPropertyChanged(nameof(SelectedSong));
			}
		}

		private User selectedUser;
		public User SelectedUser
		{
			get => selectedUser;
			set
			{
				selectedUser = value;
				OnPropertyChanged(nameof(SelectedUser));
				OnPropertyChanged(nameof(SelectedUserFavorites));
			}
		}

		private Song selectedFavoriteSong;
		public Song SelectedFavoriteSong
		{
			get => selectedFavoriteSong;
			set
			{
				selectedFavoriteSong = value;
				OnPropertyChanged(nameof(SelectedFavoriteSong));
			}
		}


		private string titleFilter;
		public string TitleFilter
		{
			get => titleFilter;
			set
			{
				titleFilter = value;
				OnPropertyChanged(nameof(TitleFilter));
			}
		}

		private string artistFilter;
		public string ArtistFilter
		{
			get => artistFilter;
			set
			{
				artistFilter = value;
				OnPropertyChanged(nameof(ArtistFilter));
			}
		}


		public ObservableCollection<Song> SelectedUserFavorites =>
			CurrentUser?.FavoriteSongs;

		public ObservableCollection<Song> FavoriteSongs =>
			CurrentUser?.FavoriteSongs;


		public MainViewModel(IMusicRepository repository, User user = null)
		{
			repo = repository;

			CurrentUser = user;

			LoadSongs();
			LoadUsers();

			if (CurrentUser != null)
			{
				CurrentUser.FavoriteSongs = new ObservableCollection<Song>(
					repo.GetFavorites(CurrentUser.Id));
			}
		}


		private void LoadSongs()
		{
			allSongs = repo.GetSongs();

			Songs.Clear();
			foreach (var s in allSongs)
				Songs.Add(s);
		}

		private void LoadUsers()
		{
			Users.Clear();

			List<User> users = repo.GetUsers();

			foreach (var u in users)
				Users.Add(u);
		}

		public void AddSong(Song song)
		{
			if (song == null) return;

			repo.AddSong(song);
			LoadSongs();
		}

		public void DeleteSong()
		{
			if (SelectedSong == null) return;

			repo.DeleteSong(SelectedSong.Id);
			LoadSongs();
		}

		public void AddUser(User user)
		{
			if (user == null) return;

			repo.AddUser(user);
			LoadUsers();
		}

		public void DeleteUser()
		{
			if (SelectedUser == null) return;

			repo.DeleteUser(SelectedUser.Id);
			LoadUsers();
		}

		public void AddFavorite()
		{
			if (CurrentUser == null || SelectedSong == null) return;

			if (!CurrentUser.FavoriteSongs.Any(x => x.Id == SelectedSong.Id))
			{
				repo.AddFavorite(CurrentUser.Id, SelectedSong.Id); 
				CurrentUser.FavoriteSongs.Add(SelectedSong);
				OnPropertyChanged(nameof(FavoriteSongs));
				OnPropertyChanged(nameof(SelectedUserFavorites));
			}
		}

		public void RemoveFavorite()
		{
			if (CurrentUser == null) return;

			var toRemove = SelectedFavoriteSong ?? SelectedSong;
			if (toRemove == null) return;

			var song = CurrentUser.FavoriteSongs
				.FirstOrDefault(x => x.Id == toRemove.Id);

			if (song != null)
			{
				repo.RemoveFavorite(CurrentUser.Id, song.Id); 
				CurrentUser.FavoriteSongs.Remove(song);
				OnPropertyChanged(nameof(FavoriteSongs));
				OnPropertyChanged(nameof(SelectedUserFavorites));
			}
		}

		public void ApplyFilter()
		{
			Songs.Clear();

			var filtered = allSongs.AsEnumerable();

			if (!string.IsNullOrWhiteSpace(TitleFilter))
				filtered = filtered.Where(s =>
					s.Title.IndexOf(TitleFilter, System.StringComparison.OrdinalIgnoreCase) >= 0);

			if (!string.IsNullOrWhiteSpace(ArtistFilter))
				filtered = filtered.Where(s =>
					s.Artist.IndexOf(ArtistFilter, System.StringComparison.OrdinalIgnoreCase) >= 0);

			foreach (var s in filtered)
				Songs.Add(s);
		}
	}
}