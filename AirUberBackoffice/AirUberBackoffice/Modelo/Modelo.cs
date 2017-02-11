using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirUberBackoffice
{
    public class Modelo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private int modeloId;
        /// <summary>
        /// Identificador unívoco de um modelo, sendo chave primária na base de dados.
        /// </summary>
        public int ModeloId
        {
            get { return modeloId; }
            set
            {
                modeloId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModeloId"));
            }
        }

        private int capacidade;
        /// <summary>
        /// Propriedade responsável por guardar o número máximo de pessoas que um modelo de jato suporta.
        /// </summary>
        public int Capacidade 
        {
            get { return capacidade; }
            set
            {
                capacidade = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Capacidade"));
            }
        }

        private decimal alcance;
        /// <summary>
        /// Propriedade responsável por guardar valor do alcance máximo, em Km, de um modelo.
        /// </summary>
        public decimal Alcance    // em Km
        {
            get { return alcance; }
            set
            {
                alcance = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Alcance"));
            }
        }

        private decimal velocidadeMaxima;
        /// <summary>
        /// Propriedade responsável por guardar o valor da velocidade máxima, em Km/H, de um modelo.
        /// </summary>
        public decimal VelocidadeMaxima     // Km/h
        {
            get { return velocidadeMaxima; }
            set
            {
                velocidadeMaxima = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VelocidadeMaxima"));
            }
        }

        private decimal pesoMaximaBagagens;
        /// <summary>
        /// Propriedade responsável por guardar o valor do peso máximo em bagagens, em Kg, de um modelo.
        /// </summary>
        public decimal PesoMaximaBagagens    // Kg
        {
            get { return pesoMaximaBagagens; }
            set
            {
                pesoMaximaBagagens = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PesoMaximaBagagens"));
            }
        }

        private int numeroMotores;
        /// <summary>
        /// Propriedade responsável por guardar o número de motores de um modelo.
        /// </summary>
        public int NumeroMotores 
        {
            get { return numeroMotores; }
            set
            {
                numeroMotores = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NumeroMotores"));
            }
        }

        private decimal altitudeIdeal;
        /// <summary>
        /// Propriedade responsável por guardar o valor da altura ideal, em Metros, de um modelo.
        /// </summary>
        public decimal AltitudeIdeal      // Metros
        {
            get { return altitudeIdeal; }
            set
            {
                altitudeIdeal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AltitudeIdeal"));
            }
        }

        private decimal alturaCabine;
        /// <summary>
        /// Propriedade responsável por guardar o valor da altura da cabine, em Metros, de um modelo.
        /// </summary>
        public decimal AlturaCabine       // Metros
        {
            get { return alturaCabine; }
            set
            {
                alturaCabine = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AlturaCabine"));
            }
        }

        private decimal larguraCabine;
        /// <summary>
        /// Propriedade responsável por guardar o valor da largura da cabine, em Metros, de um modelo.
        /// </summary>
        public decimal LarguraCabine       // Metros
        {
            get { return larguraCabine; }
            set
            {
                larguraCabine = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LarguraCabine"));
            }
        }

        private decimal comprimentoCabine;
        /// <summary>
        /// Propriedade responsável por guardar o valor do comprimento da cabine, em Metros, de um modelo.
        /// </summary>
        public decimal ComprimentoCabine      // Metros
        {
            get { return comprimentoCabine; }
            set
            {
                comprimentoCabine = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ComprimentoCabine"));
            }
        }

        private string descricao;
        /// <summary>
        /// Propriedade responsável por guardar a descrição de um modelo.
        /// </summary>
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Descricao"));
            }
        }

        private int tipoJatoId;
        /// <summary>
        /// Identificador unívoco do tipo de jato que o modelo é.
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

        // Propriedades Virtuais
        /// <summary>
        /// Propriedade navegacional responsável por referenciar o tipo de jato que o modelo é.
        /// </summary>
        public TipoJato TipoJato { get; set; }
    }
}
