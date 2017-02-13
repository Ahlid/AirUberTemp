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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AirUberBackoffice.AlteracoesModelo;

namespace AirUberBackoffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //private Companhia companhiaActual;

        
        public MainWindow()
        {
            //companhiaActual = null;
            InitializeComponent();
        }


        /// 
        /// Menu Ficheiro
        /// 

        private void onGuardarClick(object sender, RoutedEventArgs e)
        {

        }

        private void onSairClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        ///
        /// Menu Editar
        /// 

        private void onNovoClick(object sender, RoutedEventArgs e)
        {

        }

        private void onEditarClick(object sender, RoutedEventArgs e)
        {

        }

        private void onEliminarClick(object sender, RoutedEventArgs e)
        {

        }


        ///
        /// Menu Navegar 
        ///

        private void onPrimeiroClick(object sender, RoutedEventArgs e)
        {

        }

        private void onAnteriorClick(object sender, RoutedEventArgs e)
        {

        }

        private void onSeguinteClick(object sender, RoutedEventArgs e)
        {

        }

        private void onUltimoClick(object sender, RoutedEventArgs e)
        {

        }


        ///
        /// Menu Viagens
        ///
        private void onVerTodasViagensClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;

            FormReservas.Visibility = Visibility.Visible;
            ListBoxReservas.ItemsSource = App.AirUberDB.GetReservas();
            ListBoxReservas.SelectedValuePath = "ReservaId";
           // ListBoxReservas.DisplayMemberPath = "DataChegada";   // mostrar mais!, conseguido com o to string
            ListBoxReservas.SelectedIndex = 0;
            ListBoxReservas.IsSynchronizedWithCurrentItem = true;
        }

        private void onVerTodosAeroportosClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;



            FormAeroportos.Visibility = Visibility.Visible;
            ListBoxAeroportos.ItemsSource = App.AirUberDB.GetAeroportos();
            ListBoxAeroportos.SelectedValuePath = "AeroportoId";
          //  ListBoxAeroportos.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxAeroportos.SelectedIndex = 0;
            ListBoxAeroportos.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarAeroportosClick(object sender, RoutedEventArgs e)
        {
            EditarAeroportosDialog aeroportosDialog = new EditarAeroportosDialog();
            aeroportosDialog.Title = "Novo Aeroporto";

            if (aeroportosDialog.ShowDialog() == true)
            {

                App.AirUberDB.InserirAeroporto(aeroportosDialog.Aeroporto);


                ListBoxAeroportos.Items.MoveCurrentToLast();
                onVerTodosAeroportosClick(null, null);
            }
        }
        private void onEditarAeroportosClick(object sender, RoutedEventArgs e)
        {

        }
        private void onEliminarAeroportosClick(object sender, RoutedEventArgs e)
        {

        }




        private void onVerTodosPaisesClick(object sender, RoutedEventArgs e)
        {
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed; 
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;

            FormPaises.Visibility = Visibility.Visible;
            ListBoxPaises.ItemsSource = App.AirUberDB.GetPaises();
            ListBoxPaises.SelectedValuePath = "PaisId";
           // ListBoxPaises.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxPaises.SelectedIndex = 0;
            ListBoxPaises.IsSynchronizedWithCurrentItem = true;
        }

        private void onVerTodasCidadesClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormCidades.Visibility = Visibility.Visible;
            ListBoxCidades.ItemsSource = App.AirUberDB.GetCidades();
            ListBoxCidades.SelectedValuePath = "CidadeId";
           // ListBoxCidades.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxCidades.SelectedIndex = 0;
            ListBoxCidades.IsSynchronizedWithCurrentItem = true;
        }

        ///
        /// Menu Clientes
        ///
        private void onVerTodosClientesClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormClientes.Visibility = Visibility.Visible;
            ListBoxClientes.ItemsSource = App.AirUberDB.GetClientes();
            ListBoxClientes.SelectedValuePath = "ApplicationUserId";
           // ListBoxClientes.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxClientes.SelectedIndex = 0;
            ListBoxClientes.IsSynchronizedWithCurrentItem = true;
        }
        private void onEditarClienteClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxClientes.SelectedItem == null)
                return;

            Cliente clienteActual = ListBoxClientes.SelectedItem as Cliente;
            if (clienteActual == null)
                return;

            Cliente cliente = new Cliente()
            {
                ApplicationUserId = clienteActual.ApplicationUserId,
                Nome = clienteActual.Nome,
                Apelido = clienteActual.Apelido,
                DataCriacao = clienteActual.DataCriacao,
                Ativo = clienteActual.Ativo,
                Contacto = clienteActual.Contacto,
                ContaDeCreditosId = clienteActual.ContaDeCreditosId
            };
           /*     Nome = clienteActual.Nome,
                CompanhiaId = clienteActual.CompanhiaId,
                TipoExtraId = extraActual.TipoExtraId,
                Valor = extraActual.Valor
            };
            */
            EditarClienteDialog clienteDialog = new EditarClienteDialog(cliente);
            clienteDialog.Title = "Editar Cliente";

            if (clienteDialog.ShowDialog() == true && clienteDialog.Cliente != clienteActual)
            {
                clienteActual.ApplicationUserId = clienteDialog.Cliente.ApplicationUserId;
                clienteActual.Nome = clienteDialog.Cliente.Nome;
                clienteActual.Apelido = clienteDialog.Cliente.Apelido;
                clienteActual.DataCriacao = clienteDialog.Cliente.DataCriacao;
                clienteActual.Contacto = clienteDialog.Cliente.Contacto;
                clienteActual.ContaDeCreditosId = clienteDialog.Cliente.ContaDeCreditosId;

                App.AirUberDB.EditarCliente(clienteActual);
            }
        }
        private void onEliminarClienteClick(object sender, RoutedEventArgs e)
        {

        }
        
        ///
        /// Menu Companhias
        ///
        private void onVerTodasCompanhiasClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            MenuItem_VerColaboradores.Visibility = Visibility.Visible;
            MenuItem_VerReservas.Visibility = Visibility.Visible;
            MenuItem_VerJatos.Visibility = Visibility.Visible;
            MenuItem_VerExtras.Visibility = Visibility.Visible;


            FormCompanhias.Visibility = Visibility.Visible;
            ListBoxCompanhias.ItemsSource = App.AirUberDB.GetCompanhias();
            ListBoxCompanhias.SelectedValuePath = "CidadeId";
           // ListBoxCompanhias.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxCompanhias.SelectedIndex = 0;
            ListBoxCompanhias.IsSynchronizedWithCurrentItem = true;

            //companhiaActual = (Companhia)ListBoxCompanhias.SelectedItem;
            //ListBoxCompanhias.SelectionChanged += onCompanhiaChanged;


            //MenuItem_Companhias.

            /*
        MenuItem newMenuItem1 = new MenuItem();
        newMenuItem1.Header = "Test 123";
        this.MainMenu.Items.Add(newMenuItem1);
        */
            /* MenuItem verColaboradores = new MenuItem();
             verColaboradores.Header = "Ver Colaboradores";
             verColaboradores.Click += onVerColaboradoresClick;
             MenuItem_Companhias.Items.Add(verColaboradores);
             */
            /*//Add to a sub item
            MenuItem newMenuItem2 = new MenuItem();
            MenuItem newExistMenuItem = (MenuItem)this.MainMenu.Items[0];
            newMenuItem2.Header = "Test 456";
            n*-ewExistMenuItem.Items.Add(newMenuItem2);*/
        }

      /*  private void onCompanhiaChanged (object sender, RoutedEventArgs e)
        {
            //working
            companhiaActual = (Companhia)ListBoxCompanhias.SelectedItem;

            //MenuItem_VerExtras.Visibility = Visibility.Collapsed;
            //MenuItem_VerExtras.Header = companhiaActual.Email;
        }*/

        private void onVerColaboradoresClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;

            FormColaboradores.Visibility = Visibility.Visible;

            Companhia companhiaActual = (Companhia)ListBoxCompanhias.SelectedItem;

            ListBoxColaboradores.ItemsSource = App.AirUberDB.GetColaboradores(companhiaActual.CompanhiaId);
            ListBoxColaboradores.SelectedValuePath = "ApplicationUserId";
           // ListBoxColaboradores.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxColaboradores.SelectedIndex = 0;
            ListBoxColaboradores.IsSynchronizedWithCurrentItem = true;
        }

        private void onVerReservasClick(object sender, RoutedEventArgs e)
        {
  
        }

        private void onVerJatosClick(object sender, RoutedEventArgs e)
        {

        }

        private void onVerExtrasClick(object sender, RoutedEventArgs e)
        {

        }


        ///
        /// Menu Extras
        ///

        private void onVerTodosExtrasClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;


            FormExtras.Visibility = Visibility.Visible;

            ListBoxExtras.ItemsSource = App.AirUberDB.GetExtras();
            ListBoxExtras.SelectedValuePath = "ExtrasId";
           // ListBoxExtras.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxExtras.SelectedIndex = 0;
            ListBoxExtras.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarExtrasClick(object sender, RoutedEventArgs e)
        {
            EditarExtraDialog extraDialog = new EditarExtraDialog();
            extraDialog.Title = "Novo Extra";

            if (extraDialog.ShowDialog() == true)
            {

                App.AirUberDB.InserirExtra(extraDialog.Extra);


                ListBoxExtras.Items.MoveCurrentToLast();
                onVerTodosExtrasClick(null, null);
            }
        }
        private void onEditarExtrasClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxExtras.SelectedItem == null)
                return;

            Extra extraActual = ListBoxExtras.SelectedItem as Extra;
            if (extraActual == null)
                return;

            Extra extra = new Extra() { Nome = extraActual.Nome, CompanhiaId = extraActual.CompanhiaId, TipoExtraId = extraActual.TipoExtraId, Valor = extraActual.Valor };

            EditarExtraDialog extraDialog = new EditarExtraDialog(extra);
            extraDialog.Title = "Editar Extra";

            if (extraDialog.ShowDialog() == true && extraDialog.Extra != extraActual)
            {
                extraActual.Nome = extraDialog.Extra.Nome;
                extraActual.CompanhiaId = extraDialog.Extra.CompanhiaId;
                extraActual.ExtraId = extraDialog.Extra.ExtraId;
                extraActual.TipoExtraId = extraDialog.Extra.TipoExtraId;
                extraActual.Valor = extraDialog.Extra.Valor;

                App.AirUberDB.EditarExtra(extraActual);
            }
        }
        private void onEliminarExtrasClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxExtras.SelectedItem == null)
                return;

            Extra extraActual = ListBoxExtras.SelectedItem as Extra;
            if (extraActual == null)
                return;

            if (MessageBox.Show("Quer apagar o ExtraActual?", "Apagar o extra?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                App.AirUberDB.EliminarExtra(extraActual.TipoExtraId);
                ListBoxExtras.Items.MoveCurrentToFirst();
                onVerTodosExtrasClick(null, null);
            }
        }


        private void onVerTipoExtrasClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormTipoExtras.Visibility = Visibility.Visible;

            ListBoxTipoExtras.ItemsSource = App.AirUberDB.GetTipoExtras();
            ListBoxTipoExtras.SelectedValuePath = "TipoExtrasId";
            //ListBoxTipoExtras.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxTipoExtras.SelectedIndex = 0;
            ListBoxTipoExtras.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarTipoExtrasClick(object sender, RoutedEventArgs e)
        {
            EditarTipoExtraDialog tipoExtraDialog = new EditarTipoExtraDialog();
            tipoExtraDialog.Title = "Novo Tipo de Extra";

            if (tipoExtraDialog.ShowDialog() == true)
            {

                App.AirUberDB.InserirTipoExtra(tipoExtraDialog.TipoExtra);


                ListBoxTipoExtras.Items.MoveCurrentToLast();
                onVerTipoExtrasClick(null, null);
            }
        }
        private void onEditarTipoExtrasClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxTipoExtras.SelectedItem == null)
                return;

            TipoExtra tipoExtraActual = ListBoxTipoExtras.SelectedItem as TipoExtra;
            if (tipoExtraActual == null)
                return;

            EditarTipoExtraDialog tipoExtraDialog = new EditarTipoExtraDialog(new TipoExtra() { Nome = tipoExtraActual.Nome });
            tipoExtraDialog.Title = "Editar Tipo Jato";

            if (tipoExtraDialog.ShowDialog() == true && tipoExtraDialog.TipoExtra != tipoExtraActual)
            {
                tipoExtraActual.Nome = tipoExtraDialog.TipoExtra.Nome;

                App.AirUberDB.EditarTipoExtra(tipoExtraActual);
            }
        }
        private void onEliminarTipoExtrasClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxTipoExtras.SelectedItem == null)
                return;

            TipoExtra tipoExtraActual = ListBoxTipoExtras.SelectedItem as TipoExtra;
            if (tipoExtraActual == null)
                return;

            if (MessageBox.Show("Quer apagar o tipoExtraActual?", "Apagar tipo de extra?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                App.AirUberDB.EliminarTipoExtra(tipoExtraActual.TipoExtraId);
                ListBoxTipoExtras.Items.MoveCurrentToFirst();
                onVerTipoExtrasClick(null, null);
            }
        }

        ///
        /// Menu Modelos de Jatos
        ///

        private void onVerTodosJatosClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormJatos.Visibility = Visibility.Visible;

            ListBoxTodosJatos.ItemsSource = App.AirUberDB.GetJatos();
            ListBoxTodosJatos.SelectedValuePath = "JatoId";
           // ListBoxTodosJatos.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxTodosJatos.SelectedIndex = 0;
            ListBoxTodosJatos.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarJatoClick(object sender, RoutedEventArgs e)
        {

        }
        private void onEditarJatoClick(object sender, RoutedEventArgs e)
        {

        }
        private void onEliminarJatoClick(object sender, RoutedEventArgs e)
        {

        }


        private void onVerTodosModelosJatosClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormModelos.Visibility = Visibility.Visible;

            ListBoxModelos.ItemsSource = App.AirUberDB.GetModelos();
            ListBoxModelos.SelectedValuePath = "ModeloId";
           // ListBoxModelos.DisplayMemberPath = "TipoJato.Nome";   // mostrar mais!, conseguido com o to string
            ListBoxModelos.SelectedIndex = 0;
            ListBoxModelos.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarModeloJatoClick(object sender, RoutedEventArgs e)
        {
            EditarModeloDialog modeloDialog = new EditarModeloDialog();
            modeloDialog.Title = "Novo Modelo";

            if (modeloDialog.ShowDialog() == true)
            {

                App.AirUberDB.InserirModelo(modeloDialog.Modelo);


                ListBoxModelos.Items.MoveCurrentToLast();
                onVerTodosModelosJatosClick(null, null);
            }
        }
        private void onEditarModeloJatoClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxModelos.SelectedItem == null)
                return;

            Modelo modeloActual = ListBoxModelos.SelectedItem as Modelo;
            if (modeloActual == null)
                return;
            Modelo newModelo = new Modelo() { };
            newModelo.Capacidade = modeloActual.Capacidade;
            newModelo.Alcance = modeloActual.Alcance;
            newModelo.VelocidadeMaxima = modeloActual.VelocidadeMaxima;
            newModelo.PesoMaximaBagagens = modeloActual.PesoMaximaBagagens;
            newModelo.NumeroMotores = modeloActual.NumeroMotores;
            newModelo.AltitudeIdeal = modeloActual.AltitudeIdeal;
            newModelo.AlturaCabine = modeloActual.AlturaCabine;
            newModelo.LarguraCabine = modeloActual.LarguraCabine;
            newModelo.ComprimentoCabine = modeloActual.ComprimentoCabine;
            newModelo.Descricao = modeloActual.Descricao;
            newModelo.TipoJatoId = modeloActual.TipoJatoId;


            EditarModeloDialog modeloDialog = new EditarModeloDialog(newModelo);
            modeloDialog.Title = "Editar Modelo";

            if (modeloDialog.ShowDialog() == true && modeloDialog.Modelo != modeloActual)
            {
                modeloActual.Capacidade = modeloDialog.Modelo.Capacidade;
                modeloActual.Alcance = modeloDialog.Modelo.Alcance;
                modeloActual.VelocidadeMaxima = modeloDialog.Modelo.VelocidadeMaxima;
                modeloActual.PesoMaximaBagagens = modeloDialog.Modelo.PesoMaximaBagagens;
                modeloActual.NumeroMotores = modeloDialog.Modelo.NumeroMotores;
                modeloActual.AltitudeIdeal = modeloDialog.Modelo.AltitudeIdeal;
                modeloActual.AlturaCabine = modeloDialog.Modelo.AlturaCabine;
                modeloActual.LarguraCabine = modeloDialog.Modelo.LarguraCabine;
                modeloActual.ComprimentoCabine = modeloDialog.Modelo.ComprimentoCabine;
                modeloActual.Descricao = modeloDialog.Modelo.Descricao;
                modeloActual.TipoJatoId = modeloDialog.Modelo.TipoJatoId;

                App.AirUberDB.EditarModelo(modeloActual);
            }
        }
        private void onEliminarModeloJatoClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxModelos.SelectedItem == null)
                return;

            Modelo modeloActual = ListBoxModelos.SelectedItem as Modelo;
            if (modeloActual == null)
                return;

            if (MessageBox.Show("Quer apagar o modelo?", "Apagar modelo?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                App.AirUberDB.EliminarModelo(modeloActual.ModeloId);
                ListBoxModelos.Items.MoveCurrentToFirst();

                onVerTodosModelosJatosClick(null, null);
            }
        }


        private void onVerTipoDeJatosClick(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;


            FormTipoJatos.Visibility = Visibility.Visible;

            ListBoxTipoJatos.ItemsSource = App.AirUberDB.GetTipoJatos();
            ListBoxTipoJatos.SelectedValuePath = "TipoModeloId";
           // ListBoxTipoJatos.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
            ListBoxTipoJatos.SelectedIndex = 0;
            ListBoxTipoJatos.IsSynchronizedWithCurrentItem = true;
        }
        private void onCriarTipoDeJatosClick(object sender, RoutedEventArgs e)
        {
            EditarTipoJatoDialog tipoJatoDialog = new EditarTipoJatoDialog();
            tipoJatoDialog.Title = "Novo Tipo de Jato";

            if (tipoJatoDialog.ShowDialog() == true)
            {

                App.AirUberDB.InserirTipoJato(tipoJatoDialog.TipoJato);


                ListBoxTipoJatos.Items.MoveCurrentToLast();
                onVerTipoDeJatosClick(null, null);
            }
        }
        private void onEditarTipoDeJatosClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxTipoJatos.SelectedItem == null)
                return;

            TipoJato tipoJatoActual = ListBoxTipoJatos.SelectedItem as TipoJato;
            if (tipoJatoActual == null)
                return;

            EditarTipoJatoDialog tipoJatoDialog = new EditarTipoJatoDialog(new TipoJato() {Nome = tipoJatoActual.Nome });
            tipoJatoDialog.Title = "Editar Tipo Jato";

            if (tipoJatoDialog.ShowDialog() == true && tipoJatoDialog.TipoJato != tipoJatoActual)
            {
                tipoJatoActual.Nome = tipoJatoDialog.TipoJato.Nome;

                App.AirUberDB.EditarTipoJato(tipoJatoActual);
            }
        }
        private void onEliminarTipoDeJatosClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxTipoJatos.SelectedItem == null)
                return;

            TipoJato tipoJatoActual = ListBoxTipoJatos.SelectedItem as TipoJato;
            if (tipoJatoActual == null)
                return;

            if (MessageBox.Show("Quer apagar o tipoJatoActual?", "Apagar tipo de jato?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                App.AirUberDB.EliminarTipoJato(tipoJatoActual.TipoJatoId);
                ListBoxTipoJatos.Items.MoveCurrentToFirst();
                onVerTipoDeJatosClick(null, null);
            }
        }












    ///
    /// Menu Ajuda
    ///

        private void onAjudarClick(object sender, RoutedEventArgs e)
        {

        }

        private void onSobreClick(object sender, RoutedEventArgs e)
        {

        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FormPaises.Visibility = Visibility.Collapsed;
            FormCidades.Visibility = Visibility.Collapsed;
            FormAeroportos.Visibility = Visibility.Collapsed;
            FormReservas.Visibility = Visibility.Collapsed;
            FormClientes.Visibility = Visibility.Collapsed;
            FormCompanhias.Visibility = Visibility.Collapsed;
            FormColaboradores.Visibility = Visibility.Collapsed;
            FormModelos.Visibility = Visibility.Collapsed;
            FormTipoJatos.Visibility = Visibility.Collapsed;
            FormJatos.Visibility = Visibility.Collapsed;
            FormTipoExtras.Visibility = Visibility.Collapsed;
            FormExtras.Visibility = Visibility.Collapsed;

            MenuItem_VerColaboradores.Visibility = Visibility.Collapsed;
            MenuItem_VerReservas.Visibility = Visibility.Collapsed;
            MenuItem_VerJatos.Visibility = Visibility.Collapsed;
            MenuItem_VerExtras.Visibility = Visibility.Collapsed;





            //.Produtos.Load();
            /*   ListBoxPaises.ItemsSource = App.AirUberDB.GetPaises();
               ListBoxPaises.SelectedValuePath = "PaisId";
               ListBoxPaises.DisplayMemberPath = "Nome";   // mostrar mais!, conseguido com o to string
               ListBoxPaises.SelectedIndex = 0;
               ListBoxPaises.IsSynchronizedWithCurrentItem = true;
            */
            //            DataGridPaises.ItemsSource = App.BDProdutos.GetProdutos();

        }
    }
}
