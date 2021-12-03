using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HereWeAreAgain.Models
{
    public class Book : BaseEntity, INotifyPropertyChanged
    {

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public DateTime EditionDate { get; set; }
        public enum BookState { VERY_GOOD, GOOD, MEDIOCRE, WORN };
        public BookState state;
        public BookState State
        {
            get { return state; }
            set
            {
                state = value;
                MyPropertyChanged("State");
            }
        }
        private User owner;
        public User Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                MyPropertyChanged("Owner");
            }
        } //Propriétaire du livre
        [NotMapped]
        public int OwnerId { get; set; }
        public int pointsValue;
        public int PointsValue
        {
            get { return pointsValue; }
            set
            {
                pointsValue = value;
                MyPropertyChanged("PointsValue");
            }
        }
        public override string ToString()
        {
            return Title;
        }

        #region Notifications
        public event PropertyChangedEventHandler PropertyChanged;
        [NotMapped]
        private Task T;
        [NotMapped]
        private string ChangedProperties;
        private void MyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // Si l'evenement a été renseigné
            {
                // Récupération du nom de la propriété modifiée ou concatenation avec ';'
                ChangedProperties = string.IsNullOrEmpty(ChangedProperties) ? propertyName : ChangedProperties + $";{propertyName}";
                if (T == null) // Si la tache n'a pas encore été initialisée
                {
                    T = Task.Run(async () => // Crée une tache qui patiente le temps d'effectuer les modifications
                    {
                        await Task.Delay(500); // Patiente une demie seconde (toute les modifications se faisant a la suite et sans attente)
                        PropertyChanged(this, new PropertyChangedEventArgs(ChangedProperties)); // Déclenche l'EventHandler
                        //Reinitialisation des variables de notification
                        ChangedProperties = null;
                        T = null;
                    });
                }
            }
        }
        #endregion
    }
}
