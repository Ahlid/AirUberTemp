using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class TipoExtra : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }


        private int tipoExtraId;
        /// <summary>
        /// Identificador unívoco de um tipo de extra, sendo chave primária na base de dados.
        /// </summary>
        public int TipoExtraId
        {
            get { return tipoExtraId; }
            set
            {
                tipoExtraId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TipoExtraId"));
            }
        }


        private string nome;
        /// <summary>
        /// Propriedade responsável por guardar o nome do tipo de extra.
        /// </summary>
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
