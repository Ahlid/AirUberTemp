using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    /// <summary>
    /// Classe responsável por toda a informação de uma conta de créditos.
    /// </summary>
    public class ContaDeCreditos
    {
        /// <summary>
        /// Identificador unívoco de uma conta de créditos, sendo chave primária na base de dados.
        /// </summary>
        public int ContaDeCreditosId { get; set; }
        /// <summary>
        /// Propriedade responsável por guardar o número de créditos existentes numa conta.
        /// </summary>
        [Display (Name = "Créditos")]
        public decimal JetCashActual { get; set; }


        //verificar se faz sentido este atributo em baixo
        //public int numeroConta { get; set; }

        
        //historico -> adicionar numa fase posterior



        /// <summary>
        /// Constructor da classe ContaDeCreditos, é responsável por inicializar o número de jetcash a 0.
        /// </summary>
        public ContaDeCreditos()
        {
            JetCashActual = 0.00m;
        }
    }
}
