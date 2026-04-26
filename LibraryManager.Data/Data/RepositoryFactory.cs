using LibraryManager.Core.Data;
using LibraryManager.Data;
using LibraryManager.Data.Data;

namespace LibraryManager.Data.Data
{
	public static class RepositoryFactory
	{
		public static IMusicRepository Create(string type)
		{
			switch (type.ToLower())
			{
				case "sqlite":
					return new SqliteMusicRepository();

				case "sql":
				case "sqlserver":
					return new SqlMusicRepository();

				default:
					throw new System.Exception("Unknown repository type!");
			}
		}
	}
}