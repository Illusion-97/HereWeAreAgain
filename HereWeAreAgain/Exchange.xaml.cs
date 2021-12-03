using HereWeAreAgain.Models;
using HereWeAreAgain.Services;
using System;
using System.Collections.Generic;
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
            book = b; // Récupere le livre passé par la fenetre parent
        }

        private void Valid_Click(object sender, RoutedEventArgs e)
        {
            if(((User)NewOwnerCb.SelectedItem).Id != book.Owner.Id) // Si il y a changement de propriétaire
            {
                if (((Book.BookState)StateCb.SelectedItem) != book.State) // Si l'état a été modifié (détérioration ou remplacement par livre de meilleur qualité)
                {
                    Book.BookState nbs = (Book.BookState)StateCb.SelectedItem; // Récupération de l'état selectionné
                    book.PointsValue *= 1 + ((book.State - nbs) / 4); // Calcul de la nouvelle valeur en points du livre
                    book.State = nbs; //Mise a jour de l'état du livre
                }
                bookService.ValidateExchange(book, ((User)NewOwnerCb.SelectedItem).Id); // Persistance de l'échange
                mw.LoadBooks(); // Rechargement de la liste de la fenetre appelante 
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mw = (MainWindow)Owner; // Récupere la fenetre appelante
            TitleLbl.Content = book.Title; // Mise a jour du Label Titre
            OwnerLbl.Content = book.Owner.Name; // Mise a jour du propriétaire
            NewOwnerCb.ItemsSource = UserService.FindAll(); // Initialisation du contenu de la CB de sélection du nouveau propriétaire
            NewOwnerCb.SelectedIndex = book.Owner.Id -1; // Selectionne par défaut le propriétaire actuel
            StateCb.ItemsSource = Enum.GetValues(typeof(Book.BookState)).Cast<Book.BookState>(); // Initialisation du contenu de la CB de sélection de l'état
            StateCb.SelectedItem = book.State;// Initialise la sélection selon l'état actuel du livre
        }
    }
}
