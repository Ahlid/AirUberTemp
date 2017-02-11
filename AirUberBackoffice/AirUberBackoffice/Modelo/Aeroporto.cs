using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Aeroporto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        /// <summary>
        /// Identificador unívoco de um aeroporto, sendo chave primária na base de dados.
        /// </summary>
        private int aeroportoId;
        public int AeroportoId
        {
            get { return aeroportoId; }
            set
            {
                aeroportoId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AeroportoId"));
            }
        }


        /// <summary>
        /// Identificador unívoco da cidade a que o aeroporto pertence.
        /// </summary>
        private int cidadeId;
        public int CidadeId
        {
            get { return cidadeId; }
            set
            {
                cidadeId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CidadeId"));
            }
        }


        /// <summary>
        /// Propriedade responsável por guardar o nome do aeroporto.
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
        /// Propriedade responsável por guardar a latitude do aeroporto.
        /// </summary>
        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Latitude"));
            }
        }

        /// <summary>
        /// Propriedade responsável por guardar a longitude do aeroporto.
        /// </summary>
        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Longitude"));
            }
        }

        // Propriedades Virtuais

        /// <summary>
        /// Propriedade navegacional responsável por referenciar a cidade do aeroporto.
        /// </summary>
        public Cidade Cidade { get; set; }
        // Necessário para conseguir referenciar a partir da classe Viagens o aeroporto de partida e chegada


        public ICollection<Reserva> Partidas { get; set; }

        public ICollection<Reserva> Chegadas { get; set; }



        /// <summary>
        /// Constructor da classe Aeroporto, é responsável por inicializar as listas de partidas
        /// e chegadas.
        /// </summary>
        public Aeroporto()
        {
            Partidas = new List<Reserva>();
            Chegadas = new List<Reserva>();
        }
    }
}
