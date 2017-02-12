using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirUberBackoffice
{
    public class AirUberDB
    {
        //localhost
        private string connectionString = ("Server=(localdb)\\mssqllocaldb;Database=aspnet-AirUberProjeto-66c8e40e-5470-4ac9-82b2-19ea78947f30;Trusted_Connection=True;MultipleActiveResultSets=true");

        // cons online
        //private string connectionString = ("Data Source=SQL5031.myASP.NET;Initial Catalog=DB_A1835C_AirUber;User Id=DB_A1835C_AirUber_admin;Password=12345678A");
        
            //NÃO
        //private string connectionString = ("Provider=SQLOLEDB;Data Source=SQL5031.myASP.NET;Initial Catalog=DB_A1835C_Airuberesw;User Id=DB_A1835C_Airuberesw_admin;Password=12345678A");

        public ObservableCollection<Pais> GetPaises()
        {
            ObservableCollection<Pais> paises = new ObservableCollection<Pais>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT * FROM Pais";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pais pais = new Pais();

                    pais.PaisId = (int)dr["PaisId"];
                    pais.Nome = (string)dr["Nome"];

                    paises.Add(pais);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return paises;
        }
        public ObservableCollection<Cidade> GetCidades()
        {
            ObservableCollection<Cidade> cidades = new ObservableCollection<Cidade>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT c.CidadeId AS 'CidadeId', p.PaisId As 'PaisId', p.Nome AS 'NomePais', c.Nome AS 'NomeCidade' " +
                         "FROM Cidade c, Pais p WHERE c.PaisId = p.PaisId";


            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cidade cidade = new Cidade();

                    cidade.CidadeId = (int)dr["CidadeId"];
                    cidade.Nome = (string)dr["NomeCidade"];
                    cidade.PaisId = (int)dr["PaisId"];
                    Pais pais = new Pais();
                    pais.PaisId = (int)dr["PaisId"];
                    pais.Nome = (string)dr["NomePais"];
                    cidade.Pais = pais;

                    cidades.Add(cidade);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return cidades;
        }
        public ObservableCollection<Aeroporto> GetAeroportos()
        {
            ObservableCollection<Aeroporto> aeroportos = new ObservableCollection<Aeroporto>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            //string sql = "SELECT * FROM Aeroporto";
            string sql = "SELECT a.AeroportoId AS 'AeroportoId', a.Nome AS 'Nome', a.Latitude AS 'Latitude', a.Longitude AS 'Longitude', " +
                         "c.Nome AS 'NomeCidade', c.CidadeId AS 'CidadeId'" +
                         "FROM Aeroporto a " +
                         "JOIN Cidade c ON a.CidadeId = c.CidadeId";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Aeroporto aeroporto = new Aeroporto();

                    aeroporto.AeroportoId = (int)dr["AeroportoId"];
                    Cidade cidade = new Cidade();
                    cidade.CidadeId = (int)dr["CidadeId"];
                    cidade.Nome = (string)dr["NomeCidade"];
                    aeroporto.Cidade = cidade;

                    aeroporto.CidadeId = (int)dr["CidadeId"];
                    aeroporto.Nome = (string)dr["Nome"];
                    aeroporto.Latitude = (double)dr["Latitude"];
                    aeroporto.Longitude = (double)dr["Longitude"];
                    
                    aeroportos.Add(aeroporto);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return aeroportos;
        }
        
        // Precisa de ser revisto!!!
        public ObservableCollection<Reserva> GetReservas()
        {
            ObservableCollection<Reserva> reservas = new ObservableCollection<Reserva>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            /*string sql = "SELECT * FROM Reserva JOIN Aeroporto " +
                         "ON Reserva.AeroportoPartidaId = Aeroporto.AeroportoId";// +
                         */
            //"ON Jato ";
            /*  string sql = "SELECT r.ReservaId AS 'ReservaId', r.DataPartida AS 'DataPartida', r.DataChegada AS 'DataChegada', " +
                           "a.AeroportoPartidaId AS 'APId', a.Nome AS 'AeroportoPartidaNome', " +
                           "a.AeroportoChegadaId AS 'ACId', a.Nome AS 'AeroportoChegadaNome', " +
                           "(SELECT Nome AS 'AeroportoPartidaNome" + 
                           "FROM Reserva r " +
                           "JOIN Aeroporto ON Reserva.AeroportoPartidaId = Aeroporto.AeroportoId " +
                           "";// +
  */
            string sql = "SELECT r.ReservaId AS 'ReservaId', r.DataPartida AS 'DataPartida', r.DataChegada AS 'DataChegada', " +
                                    "(SELECT Nome FROM Aeroporto a WHERE r.AeroportoPartidaId = a.AeroportoId) AS 'AeroportoPartidaNome', " +
                                    "(SELECT Nome FROM Aeroporto a WHERE r.AeroportoDestinoId = a.AeroportoId) AS 'AeroportoChegadaNome', " +
                                    "j.JatoId AS 'JatoId', j.Nome AS 'NomeJato', " +
                                    "c.CompanhiaId AS 'CompId', c.Nome AS 'CompNome', " +
                                    "r.Custo AS 'Custo', r.Avaliacao AS 'Avaliacao' " +
                                    //"u.Nome AS 'ClienteNome' " +
                                    "FROM Reserva r " +
                                    "JOIN Jato j ON r.JatoId = j.JatoId " +
                                    "JOIN Companhia c ON r.CompanhiaId = c.CompanhiaId "; //+
                                    //"JOIN AspNetUsers u ON r.Cliente.ApplicationUserId = u.Id";
                                    //FALTA CONSEGUIR IR BUSCAR O CLIENTE   

            //reserva.Cliente.ApplicationUserId

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Reserva reserva = new Reserva();

              //      reserva.ReservaId = (int)dr["ReservaId"];
               //     reserva.DataPartida = (DateTime)dr["DataPartida"];
               //     reserva.DataChegada = (DateTime)dr["DataChegada"];
                    Aeroporto aeroportoPartida = new Aeroporto();
                    //aeroportoPartida.AeroportoId = (int)dr["AeroportoPartidaId"];
                    aeroportoPartida.Nome = (string)dr["AeroportoPartidaNome"];
                    reserva.AeroportoPartida = aeroportoPartida;
                    
                    Aeroporto aeroportoChegada = new Aeroporto();
                    //aeroportoChegada.AeroportoId = (int)dr["AeroportoChegadaId"];
                    aeroportoChegada.Nome = (string)dr["AeroportoChegadaNome"];
                    reserva.AeroportoDestino = aeroportoChegada;
                    
                    Jato jato = new Jato();
                    jato.JatoId = (int)dr["JatoId"];
                    jato.Nome = (string)dr["NomeJato"];
                    reserva.Jato = jato;

                    Companhia companhia = new Companhia();
                    companhia.CompanhiaId = (int)dr["CompId"];
                    companhia.Nome = (string)dr["CompNome"];
                    reserva.Companhia = companhia;

                    reserva.Custo = (decimal)dr["Custo"];
                    reserva.Avaliacao = (int)dr["Avaliacao"];
                    /*
                                        Jato jato = new Jato();
                                        jato.JatoId = (int)dr["JatoId"];
                                        jato.Nome = (string)dr["NomeJato"];
                                        reserva.Jato = jato;

                                        Companhia companhia = new Companhia();
                                        companhia.CompanhiaId = (int)dr["CompanhiaId"];
                                        companhia.Nome = (string)dr["NomeComp"];
                                        reserva.Companhia = companhia;

                                        reserva.AeroportoPartidaId = (int)dr["AeroportoPartidaId"];
                                        reserva.AeroportoDestinoId = (int)dr["AeroportoDestinoId"];
                                        reserva.JatoId = (int)dr["JatoId"];
                                        reserva.ApplicationUserId = (string)dr["ApplicationUserId"];
                                        reserva.Custo = (decimal)dr["Custo"];
                                        reserva.Avaliacao = (int)dr["Avaliacao"];
                    */
                    //reserva.AeroportoPartida = aeroportoPartida;
                    reservas.Add(reserva);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return reservas;
        }
        public ObservableCollection<Cliente> GetClientes()
        {
            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            /*string sql = "SELECT c.CidadeId AS 'CidadeId', p.PaisId As 'PaisId', p.Nome AS 'NomePais', c.Nome AS 'NomeCidade' " +
                         "FROM Cidade c, Pais p WHERE c.PaisId = p.PaisId";
                         */
            string sql = "SELECT * FROM AspNetUsers";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cliente cliente = new Cliente();


                    // Verificar se os valores sºao válidos, se não indicar que nao esta definido!
                    
                    cliente.ApplicationUserId = (string)dr["Id"];
                    cliente.DataCriacao = (DateTime)dr["DataCriacao"];
                    cliente.Nome = (string)dr["Nome"];
                    cliente.Apelido = (string)dr["Apelido"];
                    cliente.Contacto = (string)dr["Contacto"];
                    cliente.Email = (string)dr["Email"];

                    clientes.Add(cliente);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return clientes;
        }
        public ObservableCollection<Companhia> GetCompanhias()
        {
            ObservableCollection<Companhia> companhias = new ObservableCollection<Companhia>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT c.CompanhiaId AS 'CompId', c.Nome AS 'NomeComp', c.Contact AS 'ContactoComp', " +
                            "c.DataCriacao AS 'DataCriacaoComp', c.Email AS 'EmailComp', c.Morada AS 'MoradaComp', " +
                            "c.Nif AS 'NifComp', c.RelativePathImagemPerfil AS 'CaminhoImagemComp', " +
                            "p.PaisId AS 'PaisId', p.Nome AS 'NomePais', " +
                            "e.EstadoId AS 'EstadoId', e.Nome AS 'NomeEstado', " +
                            "cc.ContaDeCreditosId AS 'ContaDeCreditosId', cc.JetCashActual AS 'Creditos' " +
                         "FROM Companhia c " +
                           "JOIN Pais p ON c.PaisId = p.PaisId " +
                           "JOIN ContaDeCreditoses cc ON c.ContaDeCreditosId = cc.ContaDeCreditosId " +
                           "JOIN Estado e ON c.EstadoId = e.EstadoId";


            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Companhia companhia = new Companhia();

                    companhia.CompanhiaId = (int)dr["CompId"];
                    companhia.Nome = (string)dr["NomeComp"];
                    companhia.Morada = (string)dr["MoradaComp"];
                    companhia.Contact = (string)dr["ContactoComp"];
                    companhia.PaisId = (int)dr["PaisId"];

                    Pais pais = new Pais();
                    pais.PaisId = (int)dr["PaisId"];
                    pais.Nome = (string)dr["NomePais"];
                    companhia.Pais = pais;

                    companhia.Nif = (string)dr["NifComp"];

                    ContaDeCreditos contaDeCreditos = new ContaDeCreditos();
                    contaDeCreditos.ContaDeCreditosId = (int)dr["ContaDeCreditosId"];
                    contaDeCreditos.JetCashActual = (decimal)dr["Creditos"];
                    companhia.ContaDeCreditos = contaDeCreditos;

                    companhia.Email = (string)dr["EmailComp"];
                    companhia.DataCriacao = (DateTime)dr["DataCriacaoComp"];

                    Estado estado = new Estado();
                    estado.EstadoId = (int)dr["EstadoId"];
                    estado.Nome = (string)dr["NomeEstado"];

                    companhia.Estado = estado;
                    companhia.RelativePathImagemPerfil = (string)dr["CaminhoImagemComp"];

                    companhias.Add(companhia);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return companhias;
        }
        public ObservableCollection<Colaborador> GetColaboradores(int companhiaId)
        {
            ObservableCollection<Colaborador> colaboradores = new ObservableCollection<Colaborador>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT u.Id AS 'userId', u.Nome AS 'NomeUser', u.Apelido AS 'ApelidoUser', " +
                         "u.IsAdministrador AS 'isAdmin', u.DataCriacao AS 'DataCriacao', u.Ativo AS 'Ativo', " +
                         "c.CompanhiaId AS 'CompId', c.Nome AS 'CompNome' " +
                         "FROM AspNetUsers u " +
                         "JOIN Companhia c ON u.CompanhiaId = " + companhiaId;

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Colaborador colaborador = new Colaborador();

                    colaborador.ApplicationUserId = (string)dr["userId"];
                    colaborador.Nome = (string)dr["NomeUser"];
               //     colaborador.Apelido = (string)dr["ApelidoUser"];
                    colaborador.IsAdministrador = (bool)dr["isAdmin"];
                    colaborador.DataCriacao = (DateTime)dr["DataCriacao"];
                    colaborador.Ativo = (bool)dr["Ativo"];

                    Companhia companhia = new Companhia();
                    companhia.CompanhiaId = (int)dr["CompId"];
                    companhia.Nome = (string)dr["CompNome"];

                    colaborador.Companhia = companhia;


                    colaboradores.Add(colaborador);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return colaboradores;
        }
        public ObservableCollection<Modelo> GetModelos()
        {
            ObservableCollection<Modelo> modelos = new ObservableCollection<Modelo>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT * FROM Modelo m " +
                         "JOIN TipoJato tj ON m.TipoJatoId = tj.TipoJatoId";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Modelo modelo = new Modelo();

                    modelo.ModeloId = (int)dr["ModeloId"];
                    modelo.TipoJatoId = (int)dr["TipoJatoId"];

                    TipoJato tipoJato = new TipoJato();
                    tipoJato.TipoJatoId = (int)dr["TipoJatoId"];
                    tipoJato.Nome = (string)dr["Nome"];
                    modelo.TipoJato = tipoJato;

                    modelo.Capacidade = (int)dr["Capacidade"];
                    modelo.Alcance = (decimal)dr["Alcance"];
                    modelo.VelocidadeMaxima = (decimal)dr["VelocidadeMaxima"];
                    modelo.PesoMaximaBagagens = (decimal)dr["PesoMaximaBagagens"];
                    modelo.NumeroMotores = (int)dr["NumeroMotores"];
                    modelo.AltitudeIdeal = (decimal)dr["AltitudeIdeal"];
                    modelo.AlturaCabine = (decimal)dr["AlturaCabine"];
                    modelo.LarguraCabine = (decimal)dr["LarguraCabine"];
                    modelo.ComprimentoCabine = (decimal)dr["ComprimentoCabine"];
                    modelo.Descricao = (string)dr["Descricao"];


                    modelos.Add(modelo);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return modelos;
        }
        public ObservableCollection<TipoJato> GetTipoJatos()
        {
            ObservableCollection<TipoJato> tipoJatos = new ObservableCollection<TipoJato>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT * FROM TipoJato";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TipoJato tipoJato = new TipoJato();

                    tipoJato.TipoJatoId = (int)dr["TipoJatoId"];
                    tipoJato.Nome = (string)dr["Nome"];


                    tipoJatos.Add(tipoJato);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return tipoJatos;
        }


        // getting error
        public ObservableCollection<Jato> GetJatos()
        {
            ObservableCollection<Jato> jatos = new ObservableCollection<Jato>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT j.JatoId AS 'JatoId', j.Nome AS 'NomeJato', j.EmFuncionamento AS 'EmFuncionamento', " +
                         "u.RelativePathImagemPerfil AS 'CaminhoFoto', m.ModeloId AS 'ModeloId', tm.Nome AS 'NomeModelo', " +
                         "c.CompanhiaId AS 'CompanhiaId', c.Nome AS 'NomeCompanhia', a.AeroportoId AS 'AeroportoId', " +
                         "a.Nome AS 'AeroportoNome' " +
                         "FROM Jato j " +
                         "JOIN Modelo m ON j.ModeloId = m.ModeloId " +
                         "JOIN TipoJato tm ON m.TipoJatoId = tm.TipoJatoId" +
                         "JOIN Companhia c ON j.CompanhiaId = c.CompanhiaId " +
                         "JOIN Aeroporto a ON j.AeroportoId = a.AeroportoId ";

                // join modelo
                // join companhia 
                // join aeroporto
            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Jato jato = new Jato();

                    jato.JatoId = (int)dr["JatoId"];
                    jato.Nome = (string)dr["NomeJato"];
                    jato.EmFuncionamento = (bool)dr["EmFuncionamento"];
                    jato.RelativePathImagemPerfil = (string)dr["CaminhoFoto"];

                    Modelo modelo = new Modelo();
                    modelo.ModeloId = (int)dr["ModeloId"];

                    TipoJato tipoJato = new TipoJato();
                    tipoJato.Nome = (string)dr["NomeModelo"];
                    modelo.TipoJato = tipoJato;

                    jato.Modelo = modelo;

                    Companhia companhia = new Companhia();
                    companhia.CompanhiaId = (int)dr["CompanhiaId"];
                    companhia.Nome = (string)dr["NomeCompanhia"];
                    jato.Companhia = companhia;

                    Aeroporto aeroporto = new Aeroporto();
                    aeroporto.AeroportoId = (int)dr["AeroportoId"];
                    aeroporto.Nome = (string)dr["AeroportoNome"];




                    jatos.Add(jato);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return jatos;
        }

        public ObservableCollection<TipoExtra> GetTipoExtras()
        {
            ObservableCollection<TipoExtra> tipoExtras = new ObservableCollection<TipoExtra>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT * FROM TipoExtra";

            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TipoExtra tipoExtra= new TipoExtra();

                    tipoExtra.TipoExtraId = (int)dr["TipoExtraId"];
                    tipoExtra.Nome = (string)dr["Nome"];


                    tipoExtras.Add(tipoExtra);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return tipoExtras;
        }

        public ObservableCollection<Extra> GetExtras()
        {
            ObservableCollection<Extra> extras = new ObservableCollection<Extra>();

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "SELECT e.ExtraId AS 'ExtraId', e.Nome AS 'NomeExtra', e.Valor AS 'Valor', " +
                         "te.TipoExtraId AS 'TipoExtraId', te.Nome AS 'TipoExtraNome', " +
                         "c.CompanhiaId AS 'CompId', c.Nome AS 'NomeComp' " +
                         "FROM Extra e " +
                         "JOIN TipoExtra te ON e.TipoExtraId = te.TipoExtraId " +
                         "JOIN Companhia c ON e.CompanhiaId = c.CompanhiaId";


            cmd.CommandText = sql;

            try
            {
                con.Open();

                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Extra extra = new Extra();
                    extra.ExtraId = (int)dr["ExtraId"];
                    extra.Nome = (string)dr["NomeExtra"];
                    extra.Valor = (decimal)dr["Valor"];

                    TipoExtra tipoExtra = new TipoExtra();
                    tipoExtra.TipoExtraId = (int)dr["TipoExtraId"];
                    tipoExtra.Nome = (string)dr["TipoExtraNome"];
                    extra.TipoExtraId = (int)dr["TipoExtraId"];
                    extra.TipoExtra = tipoExtra;

                    Companhia companhia = new Companhia();
                    companhia.CompanhiaId = (int)dr["CompId"];
                    companhia.Nome = (string)dr["NomeComp"];
                    extra.CompanhiaId = (int)dr["CompId"];
                    extra.Companhia = companhia;


                    extras.Add(extra);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            return extras;
        }

        ///
        /// Métodos Insert, Update e Delete
        ///
        public int InserirJato(Jato jato)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [Jato] ([Nome], [ModeloId], [CompanhiaId], [EmFuncionamento], [AeroportoId], [RelativePathImagemPerfil], ) VALUES ('" + jato.Nome + "', '" + jato.ModeloId.ToString() + "', '" + jato.CompanhiaId.ToString() + "', '" + jato.EmFuncionamento.ToString() + "', '" + jato.AeroportoId.ToString() + "', '" + jato.RelativePathImagemPerfil + "')";
            cmd.CommandText = sqlInsert;

            //string sqlSelect = "SELECT [Nome] FROM TipoJato WHERE (TipoJatoId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

                /*SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["TipoJatoId"];
                    */
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato adicionado (" + newId + ")");

            return newId;
        }
        public void EditarJato(Jato jato)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [Jato] " +
                "SET [Nome] ='" + jato.Nome + "' " +
                "SET [ModeloId] ='" + jato.ModeloId.ToString() + "' " +
                "SET [CompanhiaId] ='" + jato.CompanhiaId.ToString() + "' " +
                "SET [EmFuncionamento] ='" + jato.EmFuncionamento.ToString() + "' " +
                "SET [AeroportoId] ='" + jato.AeroportoId.ToString() + "' " +
                "SET [RelativePathImagemPerfil] ='" + jato.RelativePathImagemPerfil + "' " +
                "WHERE ([ModeloId] = " +
                jato.JatoId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato actualizado");
        }
        public void EliminarJato(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM [Modelo] WHERE ([ModeloId] = " + id.ToString() + ")";
            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " modelo apagado");
        }


        public int InserirTipoJato (TipoJato tipoJato)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [TipoJato] ([Nome]) VALUES ('" + tipoJato.Nome + "')";
            cmd.CommandText = sqlInsert;

            //string sqlSelect = "SELECT [Nome] FROM TipoJato WHERE (TipoJatoId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

                /*SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["TipoJatoId"];
                    */
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato adicionado (" + newId + ")");

            return newId;
        }
        public void EditarTipoJato (TipoJato tipoJato)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [TipoJato] SET [Nome] ='" +
                tipoJato.Nome + "' WHERE ([TipoJatoId] = " +
                tipoJato.TipoJatoId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato actualizado");
        }
        public void EliminarTipoJato (int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM [TipoJato] WHERE ([TipoJatoId] = " + id.ToString() + ")";
            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " produto apagado");
        }


        public int InserirModelo(Modelo modelo)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [Modelo] ([Capacidade], [Alcance], [VelocidadeMaxima], [PesoMaximaBagagens], [NumeroMotores], [AltitudeIdeal], [AlturaCabine], [LarguraCabine], [ComprimentoCabine], [Descricao], [TipoJatoId]) VALUES ('" + modelo.Capacidade.ToString() + "', '" + modelo.Alcance.ToString() + "', '" + modelo.VelocidadeMaxima.ToString() + "', '" + modelo.PesoMaximaBagagens.ToString() + "', '" + modelo.NumeroMotores.ToString() + "', '" + modelo.AltitudeIdeal.ToString() + "', '" + modelo.AlturaCabine.ToString() + "', '" + modelo.LarguraCabine.ToString() + "', '" + modelo.ComprimentoCabine.ToString() + "', '" + modelo.Descricao + "', '" + modelo.TipoJatoId.ToString() + "')";
            cmd.CommandText = sqlInsert;

            string sqlSelect = "SELECT [Capacidade] FROM Modelo WHERE (ModeloId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

              /*  SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["TipoJatoId"];
              */      
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

           // MessageBox.Show(regs + " tipo de jato adicionado (" + newId + ")");

            return newId;
        }
        public void EditarModelo(Modelo modelo)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [Modelo] " +
                "SET [Capacidade] ='" + modelo.Capacidade.ToString() + "' " +
                "SET [Alcance] ='" + modelo.Alcance.ToString() + "' " +
                "SET [VelocidadeMaxima] ='" + modelo.VelocidadeMaxima.ToString() + "' " +
                "SET [PesoMaximaBagagens] ='" + modelo.PesoMaximaBagagens.ToString() + "' " +
                "SET [NumeroMotores] ='" + modelo.NumeroMotores.ToString() + "' " +
                "SET [AltitudeIdeal] ='" + modelo.AltitudeIdeal.ToString() + "' " +
                "SET [AlturaCabine] ='" + modelo.AlturaCabine.ToString() + "' " +
                "SET [LarguraCabine] ='" + modelo.LarguraCabine.ToString() + "' " +
                "SET [ComprimentoCabine] ='" + modelo.ComprimentoCabine.ToString() + "' " +
                "SET [Descricao] ='" + modelo.Descricao + "' " +
                "SET [TipoJatoId] ='" + modelo.TipoJatoId.ToString() + "' " +
                "WHERE ([ModeloId] = " +
                modelo.ModeloId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            MessageBox.Show(regs + " modelo actualizado");
        }
        public void EliminarModelo(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM [Modelo] WHERE ([ModeloId] = " + id.ToString() + ")";
            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " modelo apagado");
        }


        /// Extras
         
        public int InserirTipoExtra(TipoExtra tipoExtra)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [TipoExtra] ([Nome]) VALUES ('" + tipoExtra.Nome + "')";
            cmd.CommandText = sqlInsert;

            //string sqlSelect = "SELECT [Nome] FROM TipoJato WHERE (TipoJatoId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

                /*SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["TipoJatoId"];
                    */
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato adicionado (" + newId + ")");

            return newId;
        }
        public void EditarTipoExtra(TipoExtra tipoExtra)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [TipoExtra] SET [Nome] ='" +
                tipoExtra.Nome + "' WHERE ([TipoExtraId] = " +
                tipoExtra.TipoExtraId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de extra actualizado");
        }
        public void EliminarTipoExtra(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM [TipoExtra] WHERE ([TipoExtraId] = " + id.ToString() + ")";
            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo extra apagado");
        }

        public int InserirExtra(Extra extra)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [Extra] ([Nome], [TipoExtraId], [CompanhiaId], [Valor]) VALUES ('" + extra.Nome + "', '" + extra.TipoExtraId.ToString() + "', '" + extra.CompanhiaId.ToString() + "', '" + extra.Valor + "')";
            cmd.CommandText = sqlInsert;

         //   string sqlSelect = "SELECT [Nome] FROM TipoJato WHERE (TipoJatoId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

/*                SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["ExtraId"];
  */                  
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " extra adicionado (" + newId + ")");

            return newId;
        }

        //problem..
        public void EditarExtra(Extra extra)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [Extra] " +
                "SET [Nome] ='" + extra.Nome + "' " +
                "SET [TipoExtraId] ='" + extra.TipoExtraId.ToString() + "' " +
                "SET [CompanhiaId] ='" + extra.CompanhiaId.ToString() + "' " +
                "SET [Valor] ='" + extra.Valor.ToString() + "' " +
                "WHERE (ExtraId] = " +
                extra.ExtraId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de extra actualizado");
        }
        public void EliminarExtra(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sql = "DELETE FROM [Extra] WHERE ([ExtraId] = " + id.ToString() + ")";
            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " extra apagado");
        }


        // Clientes
        public void EditarCliente(Cliente cliente)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;


            string sql = "UPDATE [Cliente] " +
                "SET [Nome] ='" + cliente.Nome + "' " +
                "SET [Apelido] ='" + cliente.Apelido + "' " +
                "SET [Ativo] ='" + cliente.Ativo.ToString() + "' " +
                "SET [Contacto] ='" + cliente.Contacto + "' " +
                "WHERE ([ApplicationUserId] = " +
                cliente.ApplicationUserId.ToString() + ")";

            cmd.CommandText = sql;

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

//            MessageBox.Show(regs + " modelo actualizado");
        }


        // Viagens
        public int InserirAeroporto(Aeroporto aeroporto)
        {
            int newId = -1;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string sqlInsert = "INSERT INTO [Aeroporto] ([CidadeId], [Nome], [Latitude], [Longitude] ) VALUES ('" + aeroporto.CidadeId + "', '" + aeroporto.Nome + "', '" + aeroporto.Latitude + "', '" + aeroporto.Longitude + "')";
            cmd.CommandText = sqlInsert;

            //string sqlSelect = "SELECT [Nome] FROM TipoJato WHERE (TipoJatoId = SCOPE_IDENTITY())";

            int regs = 0;

            try
            {
                con.Open();

                regs = cmd.ExecuteNonQuery();

                /*SqlDataReader dr;
                cmd.CommandText = sqlSelect;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    newId = (int)dr["TipoJatoId"];
                    */
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();
            }

            //MessageBox.Show(regs + " tipo de jato adicionado (" + newId + ")");

            return newId;
        }
    }
}
