using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventiObnova
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ObservableCollection<Osoba> listaOsoba = new ObservableCollection<Osoba>();
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new Osoba();
			BindingGroup = new BindingGroup();

			cb.ItemsSource = listaOsoba;
			cb.DisplayMemberPath = "ImeIPrezime";


			Osoba o = new Osoba();
			if (o is INotifyPropertyChanged interfejs)
			{
				interfejs.PropertyChanged += FooBar;
			}
		}

		private void FooBar(object posiljaoc, PropertyChangedEventArgs argumenti)
		{
			MessageBox.Show($"Promeni se {argumenti.PropertyName} unutar {posiljaoc}:D");
		}

		private void Unos(object sender, RoutedEventArgs e)
		{
			//BindingOperations.GetBindingExpression(txtIme, TextBox.TextProperty).UpdateSource();
			//BindingOperations.GetBindingExpression(txtPrezime, TextBox.TextProperty).UpdateSource();
			BindingGroup.CommitEdit();
			if (DataContext is Osoba o)
				listaOsoba.Add(o);
		}

		private void IzgubioFokus(object sender, RoutedEventArgs e)
		{
			BindingGroup.CommitEdit();
		}
	}

	public class Osoba : INotifyPropertyChanged
	{
		private string _ime;
		public string Ime 
		{
			get => _ime;
			set
			{
				_ime = value;

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ime"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImeIPrezime"));
			}
		}
		private string _prezime;
		public string Prezime
		{
			get => _prezime;
			set
			{
				_prezime = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Prezime"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImeIPrezime"));
			}
		}

		public string ImeIPrezime { get => $"{Ime} {Prezime}"; }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
