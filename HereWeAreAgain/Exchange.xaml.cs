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
using System.Windows.Shapes;

namespace HereWeAreAgain
{
    /// <summary>
    /// Logique d'interaction pour Exchange.xaml
    /// </summary>
    public partial class Exchange : Window
    {
        public MainWindow mw;
        private IUserService UserService = new UserService();
        private IBookService bookService = new BookService();
        private Book book;
        public Exchange(Book b)
        {
            InitializeComponent();
            book = b;
        }

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            if((User)NewOwnerCb.SelectedItem != book.Owner)
            {
                bookService.ValidateExchange(book, ((User)NewOwnerCb.SelectedItem).Id);
                mw.LoadBooks();
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mw = (MainWindow)Owner;
            TitleLbl.Content = book.Title;
            OwnerLbl.Content = book.Owner.Name;
            NewOwnerCb.ItemsSource = UserService.FindAll();
            NewOwnerCb.SelectedIndex = book.Owner.Id - 1;
        }
    }
}
