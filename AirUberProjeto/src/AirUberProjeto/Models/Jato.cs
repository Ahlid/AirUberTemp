using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AirUberProjeto.Models
{
    public class Jato
    {
        public int JatoId { get; set; }
        [Display (Name = "Jato")]
        [Required]  
        public string Nome { get; set; }
        [Display (Name = "Modelo")]
        [Required]
        public int ModeloId { get; set; }
        [Display(Name = "Companhia")]
        [Required]
        public int CompanhiaId { get; set; }
        public bool EmFuncionamento { get; set; }   
        
        // Atributos Virtuais
        public virtual Modelo Modelo { get; set; }
        public virtual Companhia Companhia { get; set; }



        public Jato()
        {
            //valor inicial a false, porque a companhia inicialmente nao esta activa
            EmFuncionamento = false;
        }
    }
}
