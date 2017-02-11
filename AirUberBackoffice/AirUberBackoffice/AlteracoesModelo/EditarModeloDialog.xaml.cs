using System;
using System.Collections.Generic;
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

namespace AirUberBackoffice.AlteracoesModelo
{
    /// <summary>
    /// Interaction logic for EditarModeloDialog.xaml
    /// </summary>
    public partial class EditarModeloDialog : Window
    {

        public Modelo Modelo { get; set; }

        public EditarModeloDialog(Modelo modelo = null)
        {
            InitializeComponent();

            Modelo = new Modelo();

            if (modelo != null)
            {
                //Modelo.Nome = modelo.Nome;
                Modelo.Capacidade = modelo.Capacidade;
                Modelo.Alcance = modelo.Alcance;
                Modelo.VelocidadeMaxima = modelo.VelocidadeMaxima;
                Modelo.PesoMaximaBagagens = modelo.PesoMaximaBagagens;
                Modelo.NumeroMotores = modelo.NumeroMotores;
                Modelo.AltitudeIdeal = modelo.AltitudeIdeal;
                Modelo.AlturaCabine = modelo.AlturaCabine;
                Modelo.LarguraCabine = modelo.LarguraCabine;
                Modelo.ComprimentoCabine = modelo.ComprimentoCabine;
                Modelo.Descricao = modelo.Descricao;
                Modelo.TipoJatoId = modelo.TipoJatoId;

            }

            GridFormModelo.DataContext = Modelo;
        }



        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            string message;

            if (this.DialogResult == true && FormHasErrors(out message))
            {
                // Errors still exist.
                MessageBox.Show(message);

                e.Cancel = true;
            }
        }

        private bool FormHasErrors(out string message)
        {
            StringBuilder sb = new StringBuilder();
            GetErrors(sb, GridFormModelo);
            message = sb.ToString();

           if (TextBoxCapacidade == null || TextBoxCapacidade.Text == "" || Convert.ToInt32(TextBoxCapacidade.Text) == 0)
           {
                message += "\nNecessário indicar a capacidade";
           }else if (TextBoxAlcance == null || TextBoxAlcance.Text == "" || Convert.ToDecimal(TextBoxAlcance.Text) == 0)
            {
                message += "\nNecessário indicar o alcance";
            }
            else if (TextBoxVelocidadeMaxima == null || TextBoxVelocidadeMaxima.Text == "" || Convert.ToDecimal(TextBoxVelocidadeMaxima.Text) == 0)
            {
                message += "\nNecessário indicar a velocidade maxima";
            }
            else if (TextBoxPesoMaximaBagagens == null || TextBoxPesoMaximaBagagens.Text == "" || Convert.ToDecimal(TextBoxPesoMaximaBagagens.Text) == 0)
            {
                message += "\nNecessário indicar o peso máximo das bagagens";
            }
            else if (TextBoxNumeroMotores == null || TextBoxNumeroMotores.Text == "" || Convert.ToInt32(TextBoxNumeroMotores.Text) == 0)
            {
                message += "\nNecessário indicar o número de motores";
            }
            else if (TextBoxAlturaIdeal == null || TextBoxAlturaIdeal.Text == "" || Convert.ToDecimal(TextBoxAlturaIdeal.Text) == 0)
            {
                message += "\nNecessário indicar a altura ideal";
            }
            
            return message != "";
        }

        private void GetErrors(StringBuilder sb, DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;
                if (Validation.GetHasError(element))
                {
                    sb.Append(element.Text + " has errors:\r\n");
                    foreach (ValidationError error in Validation.GetErrors(element))
                    {
                        sb.Append(" " + error.ErrorContent.ToString());
                        sb.Append("\r\n");
                    }
                }

                // Check the children of this object for errors.
                GetErrors(sb, element);
            }
        }
    }
}
