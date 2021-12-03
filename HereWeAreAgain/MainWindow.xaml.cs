using HereWeAreAgain.Models;
using HereWeAreAgain.Services;
using System;
using System.Collections.Generic;
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

namespace HereWeAreAgain
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBookService bookService = new BookService();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadBooks()
        {
            DataContext = bookService.FindAll();
        }

        private void ListView_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            GridView _GridView = BookList.View as GridView;
            var _ActualWidth = BookList.ActualWidth - SystemParameters.VerticalScrollBarWidth - 15;
            for (int i = 0; i < _GridView.Columns.Count; i++)
            {
                _GridView.Columns[i].Width = _ActualWidth / _GridView.Columns.Count;
            }

        }

        private void Exchange_Click(object sender, RoutedEventArgs e)
        {
            if(BookList.SelectedIndex != -1)
            {
                _ = new Exchange((Book)BookList.SelectedItem) { Owner = this }.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez Selectionner un livre.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }
    }
}
