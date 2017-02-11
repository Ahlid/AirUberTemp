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
    /// Interaction logic for EditarExtraDialog.xaml
    /// </summary>
    public partial class EditarExtraDialog : Window
    {

        public Extra Extra { get; set; }


        public EditarExtraDialog(Extra extra = null)
        {
            InitializeComponent();

            Extra = new Extra();

            if (extra != null)
            {
                Extra.Nome = extra.Nome;
                Extra.CompanhiaId = extra.CompanhiaId;
                Extra.ExtraId = extra.ExtraId;
                Extra.TipoExtraId = extra.TipoExtraId;
                Extra.Valor = extra.Valor;
            }

            GridFormExtra.DataContext = Extra;
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
            GetErrors(sb, GridFormExtra);
            message = sb.ToString();

            /*if (TextBoxNome == null || TextBoxNome.Text == "")
            {
                message += "\nNecessário indicar o nome";
            }*/
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
