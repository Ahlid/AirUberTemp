using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Extra : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        /// <summary>
        /// Identificador unívoco de um extra, sendo chave primária na base de dados.
        /// </summary>
        private int extraId;
        public int ExtraId
        {
            get { return extraId; }
            set
            {
                extraId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ExtraId"));
            }
        }


        /// <summary>
        /// Nome do extra
        /// </summary>
        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Nome"));
            }
        }
        /// <summary>
        /// Identificador unívoco do tipo de extra que o extra é.
        /// </summary>
        private int tipoExtraId;
        public int TipoExtraId
        {
            get { return tipoExtraId; }
            set
            {
                tipoExtraId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TipoExtraId"));
            }
        }
        /// <summary>
        /// Identificador unívoco da companhia a que o extra pertence.
        /// </summary>
        private int companhiaId;
        public int CompanhiaId
        {
            get { return companhiaId; }
            set
            {
                companhiaId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CompanhiaId"));
            }
        }

        /// <summary>
        /// Propriedade responsável por guardar o valor de um extra.
        /// </summary>
        private decimal valor;
        public decimal Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Valor"));
            }
        }

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o tipo de extra que o extra é.
        /// </summary>
        public TipoExtra TipoExtra { get; set; }
        /// <summary>
        /// Propriedade navegacional responsável por referenciar a companhia a que o extra pertence.
        /// </summary>
        public Companhia Companhia { get; set; }
    }
}
