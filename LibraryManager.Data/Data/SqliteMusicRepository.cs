using LibraryManager.Core.Data;
using LibraryManager.Core.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryManager.Data.Data
{
	public class SqliteMusicRepository : IMusicRepository
	{
		private string connectionString;

		public SqliteMusicRepository()
		{
			string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string dbPath = Path.Combine(folder, "LibraryManager", "music.db");
			Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
			connectionString = $"Data Source={dbPath}";

			CreateTables();
		}

		private void CreateTables()
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();

				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"
        CREATE TABLE IF NOT EXISTS Songs (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Title TEXT,
            Artist TEXT,
            Year INTEGER,
            Genre TEXT
        );

        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT,
            Password TEXT,
            Email TEXT
        );

        CREATE TABLE IF NOT EXISTS UserFavorites (
            UserId INTEGER,
            SongId INTEGER,
            PRIMARY KEY (UserId, SongId),
            FOREIGN KEY (UserId) REFERENCES Users(Id),
            FOREIGN KEY (SongId) REFERENCES Songs(Id)
        );

        INSERT INTO Songs (Title, Artist, Year, Genre)
        SELECT 'Shape of You', 'Ed Sheeran', 2017, 'Pop'
        WHERE NOT EXISTS (SELECT 1 FROM Songs);

        INSERT INTO Songs (Title, Artist, Year, Genre)
        SELECT 'Blinding Lights', 'The Weeknd', 2019, 'Synthwave'
        WHERE NOT EXISTS (SELECT 1 FROM Songs WHERE Title='Blinding Lights');

        INSERT INTO Users (Username, Password, Email)
        SELECT 'john', '123', 'john@mail.com'
        WHERE NOT EXISTS (SELECT 1 FROM Users);

        INSERT INTO Users (Username, Password, Email)
        SELECT 'anna', '123', 'anna@mail.com'
        WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username='anna');
        ";

				cmd.ExecuteNonQuery();
			}
		}

		public List<Song> GetSongs()
		{
			var list = new List<Song>();

			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText = "SELECT Id, Title, Artist, Year, Genre FROM Songs";

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(new Song
						{
							Id = reader.GetInt32(0),
							Title = reader.GetString(1),
							Artist = reader.GetString(2),
							Year = reader.GetInt32(3),
							Genre = reader.GetString(4)
						});
					}
				}
			}

			return list;
		}

		public void AddSong(Song song)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"INSERT INTO Songs (Title, Artist, Year, Genre)
                  VALUES ($title, $artist, $year, $genre)";
				cmd.Parameters.AddWithValue("$title", song.Title);
				cmd.Parameters.AddWithValue("$artist", song.Artist);
				cmd.Parameters.AddWithValue("$year", song.Year);
				cmd.Parameters.AddWithValue("$genre", song.Genre);
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteSong(int id)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText = "DELETE FROM Songs WHERE Id = $id";
				cmd.Parameters.AddWithValue("$id", id);
				cmd.ExecuteNonQuery();
			}
		}

		public List<User> GetUsers()
		{
			var list = new List<User>();

			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText = "SELECT Id, Username, Password, Email FROM Users";

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(new User
						{
							Id = reader.GetInt32(0),
							Username = reader.GetString(1),
							Password = reader.GetString(2),
							Email = reader.GetString(3)
						});
					}
				}
			}

			return list;
		}

		public void AddUser(User user)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"INSERT INTO Users (Username, Password, Email)
                  VALUES ($username, $password, $email)";
				cmd.Parameters.AddWithValue("$username", user.Username);
				cmd.Parameters.AddWithValue("$password", user.Password);
				cmd.Parameters.AddWithValue("$email", user.Email);
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteUser(int id)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText = "DELETE FROM Users WHERE Id = $id";
				cmd.Parameters.AddWithValue("$id", id);
				cmd.ExecuteNonQuery();
			}
		}


		public List<Song> GetFavorites(int userId)
		{
			var list = new List<Song>();

			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"SELECT s.Id, s.Title, s.Artist, s.Year, s.Genre
                  FROM Songs s
                  INNER JOIN UserFavorites f ON s.Id = f.SongId
                  WHERE f.UserId = $userId";
				cmd.Parameters.AddWithValue("$userId", userId);

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(new Song
						{
							Id = reader.GetInt32(0),
							Title = reader.GetString(1),
							Artist = reader.GetString(2),
							Year = reader.GetInt32(3),
							Genre = reader.GetString(4)
						});
					}
				}
			}

			return list;
		}

		public void AddFavorite(int userId, int songId)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"INSERT OR IGNORE INTO UserFavorites (UserId, SongId)
                  VALUES ($userId, $songId)";
				cmd.Parameters.AddWithValue("$userId", userId);
				cmd.Parameters.AddWithValue("$songId", songId);
				cmd.ExecuteNonQuery();
			}
		}

		public void RemoveFavorite(int userId, int songId)
		{
			using (var con = new SqliteConnection(connectionString))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"DELETE FROM UserFavorites
                  WHERE UserId = $userId AND SongId = $songId";
				cmd.Parameters.AddWithValue("$userId", userId);
				cmd.Parameters.AddWithValue("$songId", songId);
				cmd.ExecuteNonQuery();
			}
		}
	}
}