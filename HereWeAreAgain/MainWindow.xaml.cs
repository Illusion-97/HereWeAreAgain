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

        public void LoadBooks() // Charge la liste des livres pour le Binding
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

        } // Gere la taille des colonnes selon la taille de l'écran

        private void Exchange_Click(object sender, RoutedEventArgs e)
        {
            if(BookList.SelectedIndex != -1)
            {
                Book b = (Book)BookList.SelectedItem; // Récupere le livre sélectionné
                b.PropertyChanged += MBS; // Met en place le systeme de notification
                new Exchange(b) { Owner = this }.ShowDialog(); // Ouvre la fenetre d'échange en lui passant le livre selectionné
            }
            else
            {
                MessageBox.Show("Veuillez Selectionner un livre."); // Message d'erreur si aucun livre n'est sélectionné
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // Charge les livre au chargement de la fenetre
        {
            LoadBooks();
        }

        private void MBS(object sender, PropertyChangedEventArgs e)
        {
            string message = $"Livre *{sender}* Modifié"; // Message initial
            foreach (string property in e.PropertyName.Split(';').Reverse()) // Récupere les propriétés modifiées (ordre inversé pour obtenir le propriétaire en premier)
            {
                dynamic newValue = sender.GetType().GetProperty(property).GetValue(sender); // Reflexion afin d'obtenir la valeur en cours (modifiée) de l'objet
                switch (property)
                {
                    case "Owner":
                        message += $"\nNouveau Propriétaire : ";
                        break;
                    case "State":
                        message += $"\nNouvel Etat : ";
                        break;
                    case "PointsValue":
                        message += $"\nNouvelle Valeur en points : ";
                        break;
                    default:
                        message += $"\nProperty : ";
                        break;
                }
                message += newValue;
            }
            MessageBox.Show(message); // Affichage du message
        } // MessageBox de notification
    }
}
