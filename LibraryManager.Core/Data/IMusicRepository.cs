using LibraryManager.Core.Models;
using System.Collections.Generic;

namespace LibraryManager.Core.Data
{
	public interface IMusicRepository
	{
		List<Song> GetSongs();
		void AddSong(Song song);
		void DeleteSong(int id);

		List<User> GetUsers();
		void AddUser(User user);
		void DeleteUser(int id);

		List<Song> GetFavorites(int userId);
		void AddFavorite(int userId, int songId);
		void RemoveFavorite(int userId, int songId);
	}
}