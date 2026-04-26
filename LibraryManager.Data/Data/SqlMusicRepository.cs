using LibraryManager.Core.Data;
using LibraryManager.Core.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace LibraryManager.Data.Data
{
	public class SqlMusicRepository : IMusicRepository
	{
		// ✅ ПОПРАВЕНО: използваме MusicDb, не master
		private string connectionString =
			"Server=localhost\\SQLEXPRESS;Database=MusicDb;Trusted_Connection=True;Encrypt=False;";

		public SqlMusicRepository()
		{
			EnsureDatabase();
			CreateTables();
			SeedData();
		}

		// ✅ НОВО: създаваме MusicDb ако не съществува (връзката е към master)
		private void EnsureDatabase()
		{
			string masterConnection =
				"Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;Encrypt=False;";

			using (var con = new SqlConnection(masterConnection))
			{
				con.Open();
				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MusicDb')
                  CREATE DATABASE MusicDb;";
				cmd.ExecuteNonQuery();
			}
		}

		// ================= CREATE TABLES =================
		private void CreateTables()
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();

				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Songs' AND xtype='U')
                CREATE TABLE Songs (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Title NVARCHAR(100),
                    Artist NVARCHAR(100),
                    Year INT,
                    Genre NVARCHAR(50)
                );

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                CREATE TABLE Users (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(100),
                    Password NVARCHAR(100),
                    Email NVARCHAR(100)
                );

                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserFavorites' AND xtype='U')
                CREATE TABLE UserFavorites (
                    UserId INT,
                    SongId INT,
                    PRIMARY KEY (UserId, SongId),
                    FOREIGN KEY (UserId) REFERENCES Users(Id),
                    FOREIGN KEY (SongId) REFERENCES Songs(Id)
                );
                ";

				cmd.ExecuteNonQuery();
			}
		}

		// ================= SEED DATA =================
		private void SeedData()
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();

				var cmd = con.CreateCommand();
				cmd.CommandText =
				@"
                IF NOT EXISTS (SELECT 1 FROM Users)
                BEGIN
                    INSERT INTO Users (Username, Password, Email) VALUES ('admin', '123', 'admin@mail.com');
                    INSERT INTO Users (Username, Password, Email) VALUES ('john', '123', 'john@mail.com');
                    INSERT INTO Users (Username, Password, Email) VALUES ('anna', '123', 'anna@mail.com');
                END

                IF NOT EXISTS (SELECT 1 FROM Songs)
                BEGIN
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Shape of You', 'Ed Sheeran', 2017, 'Pop');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Blinding Lights', 'The Weeknd', 2019, 'Synthwave');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Bohemian Rhapsody', 'Queen', 1975, 'Rock');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Hotel California', 'Eagles', 1977, 'Rock');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Smells Like Teen Spirit', 'Nirvana', 1991, 'Grunge');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Rolling in the Deep', 'Adele', 2010, 'Soul');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Stairway to Heaven', 'Led Zeppelin', 1971, 'Rock');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Lose Yourself', 'Eminem', 2002, 'Hip-Hop');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Thriller', 'Michael Jackson', 1982, 'Pop');
                    INSERT INTO Songs (Title, Artist, Year, Genre) VALUES ('Uptown Funk', 'Bruno Mars', 2014, 'Funk');
                END
                ";

				cmd.ExecuteNonQuery();
			}
		}

		// ================= SONGS =================
		public List<Song> GetSongs()
		{
			var list = new List<Song>();

			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand("SELECT Id, Title, Artist, Year, Genre FROM Songs", con);

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
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand(
					"INSERT INTO Songs (Title, Artist, Year, Genre) VALUES (@t,@a,@y,@g)", con);
				cmd.Parameters.AddWithValue("@t", song.Title ?? "Unknown Title");
				cmd.Parameters.AddWithValue("@a", song.Artist ?? "Unknown Artist");
				cmd.Parameters.AddWithValue("@y", song.Year);
				cmd.Parameters.AddWithValue("@g", song.Genre ?? "Unknown");
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteSong(int id)
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand("DELETE FROM Songs WHERE Id=@id", con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
		}

		// ================= USERS =================
		public List<User> GetUsers()
		{
			var list = new List<User>();

			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand("SELECT Id, Username, Password, Email FROM Users", con);

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
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand(
					"INSERT INTO Users (Username, Password, Email) VALUES (@u,@p,@e)", con);
				cmd.Parameters.AddWithValue("@u", user.Username);
				cmd.Parameters.AddWithValue("@p", user.Password);
				cmd.Parameters.AddWithValue("@e", user.Email);
				cmd.ExecuteNonQuery();
			}
		}

		public void DeleteUser(int id)
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand("DELETE FROM Users WHERE Id=@id", con);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.ExecuteNonQuery();
			}
		}

		// ================= FAVORITES =================

		public List<Song> GetFavorites(int userId)
		{
			var list = new List<Song>();

			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand(
				@"SELECT s.Id, s.Title, s.Artist, s.Year, s.Genre
                  FROM Songs s
                  INNER JOIN UserFavorites f ON s.Id = f.SongId
                  WHERE f.UserId = @userId", con);
				cmd.Parameters.AddWithValue("@userId", userId);

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
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand(
				@"IF NOT EXISTS (SELECT 1 FROM UserFavorites WHERE UserId=@u AND SongId=@s)
                  INSERT INTO UserFavorites (UserId, SongId) VALUES (@u, @s)", con);
				cmd.Parameters.AddWithValue("@u", userId);
				cmd.Parameters.AddWithValue("@s", songId);
				cmd.ExecuteNonQuery();
			}
		}

		public void RemoveFavorite(int userId, int songId)
		{
			using (var con = new SqlConnection(connectionString))
			{
				con.Open();
				var cmd = new SqlCommand(
					"DELETE FROM UserFavorites WHERE UserId=@u AND SongId=@s", con);
				cmd.Parameters.AddWithValue("@u", userId);
				cmd.Parameters.AddWithValue("@s", songId);
				cmd.ExecuteNonQuery();
			}
		}
	}
}