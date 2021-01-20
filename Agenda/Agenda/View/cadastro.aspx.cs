using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using CadastroProdutos.Controller;
using CadastroProdutos.Model;

namespace CadastroProdutos.view
{
    public partial class cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    obterContato();
                }
            }
            catch (Exception erro)
            {
                lblMsgErro.Text = "Erro: " + erro.Message;
                lblMsgErro.ForeColor = System.Drawing.Color.Red;
                Console.WriteLine("Erro: " + erro.Message);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            try
            {
                limparCampos();

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                Contato contato = new Contato();

                //---------------------------------------------------
                // Checagem dos dados
                //---------------------------------------------------
                // Valida Nome
                if (txtNome.Text.ToString().Trim() == "")
                {
                    lblMsgErro.Text = "Por favor informe o nome do Contato!";
                    txtNome.Focus();
                    return;
                }

                //---------------------------------------------------
                // Insere no banco
                //---------------------------------------------------
                contato.Nome = txtNome.Text;
                contato.Telefone = txtTelefone.Text;
                contato.Email = txtEmail.Text;
                Conexao conexao = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(3); // inserir os dados do Contato
                conexao.inserirParametros(conBanco, strComando, contato);

                lblMsgErro.Text = "Inserção efetuada com sucesso!";
                lblMsgErro.ForeColor = System.Drawing.Color.Blue;
                //lblMsgErro.Attributes.Add("style", "color: green;" );

                //Atualiza o Grid View
                obterContato();

                // Limpa os campos para inserir um novo produto
                limparCampos();

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                int idContato = 0;
                String nome = "";
                String telefone = "";
                String email = "";

                //--------------------------------------------------------
                // Otem os dados da página e faz os ajustes necessários
                //--------------------------------------------------------
                // Obtem o ID Contato
                idContato = int.Parse(txtIdContato.Text.ToString());

                // Valida Nome
                if (txtNome.Text.ToString().Trim() == "")
                {
                    lblMsgErro.Text = "Por favor informe o nome do Contato!";
                    txtNome.Focus();
                    return;
                }
                else
                {
                    nome = txtNome.Text.ToString();
                }

                // Valida Preço
                if (txtTelefone.Text.ToString().Trim() != "")
                {
                    telefone = txtTelefone.Text;
                    email = txtEmail.Text;
                }

                // Valida Qtde Estoque
                if (txtEmail.Text.ToString().Trim() != "")
                {
                    email = txtEmail.Text;
                }

                //--------------------------------------------------------
                // Atualiza os dados no banco de dados
                //--------------------------------------------------------
                Contato contato = new Contato();
                contato.Idcontato = idContato;
                contato.Nome = nome;
                contato.Telefone = telefone;
                contato.Email = email;
                Conexao conexao = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(4); // Atualizar os dados do Contato
                conexao.inserirParametros(conBanco, strComando, contato);

                obterContato();

                limparCampos();

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int idContato;
                idContato = int.Parse(txtIdContato.Text.ToString());

                Contato contato = new Contato();
                contato.Idcontato = idContato;
                Conexao conexao  = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(5); // excluir o Contato pelo ID
                conexao.inserirParametros(conBanco, strComando, contato);

                obterContato();

                limparCampos();

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void ImgEditar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Recupera o IDProduto do Grid View
                int idContato;
                idContato = Convert.ToInt32((sender as ImageButton).CommandArgument);

                // Chama o método para retornar os dados do produto
                Contato contato = new Contato();
                contato.Idcontato = idContato;
                DataTable dtContato;
                Conexao conexao = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(2); // selecionar o produto pelo seu ID
                dtContato = conexao.obterProduto(conBanco, strComando, contato);

                // Atribui os valores para os campos da tela
                txtIdContato.Text = dtContato.Rows[0]["idcontato"].ToString();
                txtNome.Text = dtContato.Rows[0]["nome"].ToString();

                // Ajusta valor por causa do controle
                String strPreco = "";
                strPreco = dtContato.Rows[0]["telefone"].ToString();
                txtTelefone.Text = strPreco.Replace(',', '.');

                txtEmail.Text = dtContato.Rows[0]["email"].ToString();

                // Ajustar os botões
                btnInserir.Enabled = false;
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = false;
                lblMsgErro.Text = "";
            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void ImgExcluir_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // Recupera o IDProduto do Grid View
                int idContato;
                idContato = Convert.ToInt32((sender as ImageButton).CommandArgument);

                // Chama o método para retornar os dados do produto
                Contato contato = new Contato();
                contato.Idcontato = idContato;

                DataTable dtContato;
                Conexao conexao = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(2); // selecionar o produto pelo seu ID
                dtContato = conexao.obterProduto(conBanco, strComando, contato);

                // Atribui os valores para os campos da tela
                txtIdContato.Text = dtContato.Rows[0]["idcontato"].ToString();
                txtNome.Text = dtContato.Rows[0]["nome"].ToString();

                // Ajusta valor por causa do controle
                String strPreco = "";
                strPreco = dtContato.Rows[0]["telefone"].ToString();
                txtTelefone.Text = strPreco.Replace(',', '.');

                txtEmail.Text = dtContato.Rows[0]["email"].ToString();

                // Ajustar os botões
                btnInserir.Enabled = false;
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = true;
                lblMsgErro.Text = "";
            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void gvProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        private void obterContato()
        {
            try
            {
                Contato contato = new Contato();
                DataTable dtContato;
                Conexao conexao = new Conexao();
                NpgsqlConnection conBanco = conexao.conectar();
                string strComando = conexao.sqlStatement(1); // selecionar todos os produtos
                dtContato = conexao.obterProduto(conBanco, strComando, contato);

                gvProduto.DataSource = dtContato;
                gvProduto.DataBind();

            }
            catch (Exception ex)
            {
                lblMsgErro.Text = "Erro: " + ex.Message;
            }
        }

        protected void limparCampos()
        {
            txtIdContato.Text = "";
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            lblMsgErro.Text = "";

            btnInserir.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
        }
    }
}