using LibraryManager.Core.Models;
using LibraryManager.Core.ViewModels;
using LibraryManager.Data.Data;
using System.Windows;

namespace LibraryManager.WPF
{
	public partial class MainWindow : Window
	{
		public MainWindow(User user)
		{
			InitializeComponent();

			var repo = RepositoryFactory.Create("sql");
			DataContext = new MainViewModel(repo, user);
		}

		private void AddFavorite_Click(object sender, RoutedEventArgs e)
		{
			((MainViewModel)DataContext).AddFavorite();
		}

		private void RemoveFavorite_Click(object sender, RoutedEventArgs e)
		{
			((MainViewModel)DataContext).RemoveFavorite();
		}

		private void FavoritesGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			
		}

		private void Search_Click(object sender, RoutedEventArgs e)
		{
			((MainViewModel)DataContext).ApplyFilter();
		}
	}
}