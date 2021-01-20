using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using CadastroProdutos.Controller;


namespace CadastroProdutos.Model
{

    public class Conexao
    {

        public Conexao()
        {
        }

        public NpgsqlConnection conectar()
        {
            try
            {
                NpgsqlConnection conBanco;
                String strConexao;

                strConexao = "Server=52.154.73.19;User Id=postgres; Port=5432; " +
                             "Password=cabala; Database=postgres";

                conBanco = new NpgsqlConnection(strConexao);

                return conBanco;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contato.Conectar");
            }
        }
        public string sqlStatement(int escolha)
        {
            string strComando = "";
            switch (escolha)
            {
                case 1:
                    strComando = "SELECT * FROM contato ORDER BY idcontato";
                    break;

                case 2:
                    strComando = "SELECT * FROM contato WHERE idcontato = @idcontato";
                    break;

                case 3:
                    strComando = "INSERT INTO contato(nome, telefone, email) " +
                     "VALUES(@nome, @telefone, @email)";
                    break;

                case 4:
                    strComando = "UPDATE contato SET nome = @nome, telefone = @telefone, " +
                              "email = @email where idcontato = @idcontato";
                    break;

                case 5:
                    strComando = "DELETE FROM contato WHERE idcontato = @idcontato";
                    break;

                default:
                    break;
            }
            return strComando;
        }
        public void inserirParametros(NpgsqlConnection conBanco, string strComando, Contato contato)
        {
            try
            {
                NpgsqlCommand cmdSQL;
                cmdSQL = new NpgsqlCommand(strComando, conBanco);
                NpgsqlParameter o_Parametro = new NpgsqlParameter();

                if (!strComando.Contains("DELETE"))
                {

                    o_Parametro.ParameterName = "@nome";
                    o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                    o_Parametro.Value = contato.Nome;
                    o_Parametro.Size = 100;
                    cmdSQL.Parameters.Add(o_Parametro);

                    o_Parametro = new NpgsqlParameter();
                    o_Parametro.ParameterName = "@telefone";
                    o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                    o_Parametro.Value = contato.Telefone;
                    cmdSQL.Parameters.Add(o_Parametro);

                    o_Parametro = new NpgsqlParameter();
                    o_Parametro.ParameterName = "@email";
                    o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                    o_Parametro.Value = contato.Email;
                    cmdSQL.Parameters.Add(o_Parametro);
                }

                if (strComando.Contains("UPDATE") || strComando.Contains("DELETE"))
                {
                    o_Parametro = new NpgsqlParameter();
                    o_Parametro.ParameterName = "@idcontato";
                    o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                    o_Parametro.Value = contato.Idcontato;
                    cmdSQL.Parameters.Add(o_Parametro);
                }
                //-----------------------------------------------------------
                // Executando as operações
                //-----------------------------------------------------------
                conBanco.Open();

                // Prepare() é obrigatório ser chamada para configurar
                // internamente o comando SQL e os parametros informados.                
                cmdSQL.Prepare();
                cmdSQL.ExecuteNonQuery();

                conBanco.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contato.Inserir Parâmetros");
            }
        }

        public DataTable obterProduto(NpgsqlConnection conBanco, string strComando, Contato contato)
        {
            try
            {
                DataTable dtContato = new DataTable();
                //-----------------------------------------------------------
                // Preparando o comando SQL
                //-----------------------------------------------------------
                NpgsqlDataAdapter daPesquisa;
                daPesquisa = new NpgsqlDataAdapter(strComando, conBanco);
                if (strComando.Contains("WHERE"))
                {
                    NpgsqlParameter o_Parametro;
                    o_Parametro = new NpgsqlParameter();
                    o_Parametro.ParameterName = "@idcontato";
                    o_Parametro.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                    o_Parametro.Value = contato.Idcontato;
                    daPesquisa.SelectCommand.Parameters.Add(o_Parametro);
                }
                //-----------------------------------------------------------
                // Executando as operações
                //-----------------------------------------------------------
                conBanco.Open();

                daPesquisa.Fill(dtContato);

                conBanco.Close();

                return dtContato;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Contato.Obter Contato");
            }
        }
    }
}