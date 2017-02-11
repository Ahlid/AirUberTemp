using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class TipoJato : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private int tipoJatoId;
        /// <summary>
        /// Identificador unívoco de um tipo de jato, sendo chave primária na base de dados.
        /// </summary>
        public int TipoJatoId
        {
            get { return tipoJatoId; }
            set
            {
                tipoJatoId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TipoJatoId"));
            }
        }

        public string nome { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o nome do tipo de jato.
        /// </summary>
//[Required]
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nome"));
            }
        }
    }
}
