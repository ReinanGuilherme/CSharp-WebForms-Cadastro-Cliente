using Cadastro_Cliente_WebForms.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cadastro_Cliente_WebForms
{
    public partial class Default : System.Web.UI.Page
    {
        //declarando lista
        List<Cliente> Clientes;

        //função chamada toda vez que a pagina atualiza
        protected void Page_Load(object sender, EventArgs e)
        {
            //instanciando a lista sempre que a pagina recarregar
            Clientes = new List<Cliente>();

            //verifica se a SESSION está vazia.
            if (Session["GuardarLista"] != null)
            {
                //adicionando a lista que foi armazenada na SESSION
                //OBS: a SESSION mesmo com o refresh da pagina não perde o conteudo guardado nela.
                Clientes = (List<Cliente>)Session["GuardarLista"];

                //atualizando a gridview ("tabela")
                gvClientes.DataSource = Session["GuardarLista"];
                gvClientes.DataBind();
            }

            lbMsgError.Text = "";
            lbMsgError.Visible = false;
            lbMsgSucess.Text = "";
            lbMsgSucess.Visible = false;
        }

        #region Evento Click

        //evento click do botão
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposObrigatorios())
            {
                int contador = Session["Contador"] == null ? 1 : (int)Session["contador"];
                Session["Contador"] = contador + 1;

                SalvarCliente(contador);
                LimparCampos();
                

            }
        }

        //evento click do botão
        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCodigo.Text.Equals("") || tbCodigo.Text == null)
                {
                    throw new Exception("Insira um código.");
                }

                ExcluirCliente(int.Parse(tbCodigo.Text));
            }
            catch (Exception error)
            {
                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }

        }

        //evento click do botão
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCodigo.Text.Equals("") || tbCodigo.Text == null)
                {
                    throw new Exception("Insira um código.");
                }
                ConsultarCliente(int.Parse(tbCodigo.Text));
            }
            catch (Exception error)
            {
                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }
        }

        //evento click do botão
        protected void btnOrdenar_Click(object sender, EventArgs e)
        {
            OrdenarPorLimiteCreditoClientes();
        }

        //evento click do botão
        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                if(Clientes.Count == 0)
                {
                    throw new Exception("Para que seja possivel gerar o relatorio é necessario que a lista possua no minimo 1 (um) cliente");
                }

                if (tbLimiteRelatorio.Text.Equals(String.Empty))
                {
                    throw new Exception("Adicione o valor no qual o relatório irá utilizar como base para gerar o relatório.");
                }                

                string formatarLimite = tbLimiteRelatorio.Text.ToString().Replace(".", "");

                double limite = double.Parse(formatarLimite);

                //Gerar relatorio
                var ClienteRelatorio = Clientes.Where(c => c.Limite_Credito >= limite).Select(i => i);

                var getResult = ClienteRelatorio.ToList().Count;
                var getResult2 = ClienteRelatorio.Any();

                if (!ClienteRelatorio.Any())
                {
                    lbMsgSucess.Text = $"Relatório gerado com sucesso, mas não temos nenhum cliente com o salário maior que R${tbLimiteRelatorio.Text}!";
                    lbMsgSucess.Visible = true;
                    lbMsgError.Visible = false;
                    gvRelatorio.Visible = false;
                    gvClientes.Visible = true;
                    return;
                }

                //atualizando a gridview relatorio ("tabela")
                gvRelatorio.DataSource = new List<Cliente>(ClienteRelatorio);
                gvRelatorio.DataBind();

                gvRelatorio.Visible = true;
                gvClientes.Visible = false;

                lbMsgSucess.Text = $"Relatório gerado com sucesso! Clientes com limite maior ou igual a R${tbLimiteRelatorio.Text}. ";
                lbMsgSucess.Visible = true;
                lbMsgError.Visible = false;
            }

            catch (Exception error)
            {
                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }
        }

        #endregion

        //INCLUSÃO
        //salva o cliente a lista
        private void SalvarCliente(int codigo)
        {

            Clientes.Add(new Cliente(codigo, tbNome.Text, DateTime.Parse(tbNascimento.Text), tbLogradouro.Text, int.Parse(tbNumero.Text), tbBairro.Text, tbCidade.Text, tbUF.Text, tbCEP.Text, tbTelefone.Text, (double.Parse(tbLimiteCredito.Text))));

            //salvando a lista em uma SESSION
            Session["GuardarLista"] = Clientes;

            //atualizando a gridview ("tabela")
            gvClientes.DataSource = Session["GuardarLista"];
            gvClientes.DataBind();

            gvClientes.Visible = true;
            gvRelatorio.Visible = false;

            lbMsgSucess.Text = "Cliente cadastrado com sucesso!";
            lbMsgSucess.Visible = true;
            lbMsgError.Visible = false;
        }

        //CONSULTA
        private void ConsultarCliente(int codigo)
        {
            try
            {
                if (Clientes != null)
                {
                    //buscando cliente atraves do codigo dentro da lista
                    var ClienteConsulta = Clientes.Where(c => c.Codigo == codigo).Select(c => c);

                    //variavel para converter
                    Cliente ConverterClienteConsulta = new Cliente();

                    //capturando o item e adicionando a variavel
                    foreach (Cliente item in ClienteConsulta)
                    {
                        ConverterClienteConsulta = item;
                    }


                    if (ConverterClienteConsulta.Codigo != codigo)
                    {
                        throw new Exception("Nenhum cliente encontrado para o código informado.");
                    }

                    tbNome.Text = ConverterClienteConsulta.Nome;
                    tbNascimento.Text = ConverterClienteConsulta.Data_Nascimento.ToString();
                    tbCEP.Text = ConverterClienteConsulta.CEP;
                    tbLogradouro.Text = ConverterClienteConsulta.Logradouro;
                    tbNumero.Text = ConverterClienteConsulta.Numero.ToString();
                    tbBairro.Text = ConverterClienteConsulta.Bairro;
                    tbCidade.Text = ConverterClienteConsulta.Cidade;
                    tbUF.Text = ConverterClienteConsulta.UF;
                    tbTelefone.Text = ConverterClienteConsulta.Telefone;
                    tbLimiteCredito.Text = ConverterClienteConsulta.Limite_Credito.ToString("F2");

                    lbMsgSucess.Text = "Consulta retornada com sucesso!";
                    lbMsgSucess.Visible = true;
                    lbMsgError.Visible = false;
                }
            } 
            catch(Exception error)
            {
                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }
        }

        //EXCLUSÃO
        private void ExcluirCliente(int codigo)
        {
            try
            {
                if (Clientes.Count != 0)
                {
                    //buscando cliente atraves do codigo dentro da lista
                    var ClienteConsulta = Clientes.Where(c => c.Codigo == codigo).Select(c => c);

                    //variavel para converter
                    Cliente ConverterClienteConsulta = new Cliente();

                    //capturando o item e adicionando a variavel
                    foreach (Cliente item in ClienteConsulta)
                    {
                        ConverterClienteConsulta = item;
                    }


                    if (ConverterClienteConsulta.Codigo != codigo)
                    {
                        throw new Exception("Nenhum cliente encontrado para o código informado.");
                    }

                    //como ja tenho o cliente capturado, posso adicionar ele a função de remoção e remover esse cliente por parametro
                    Clientes.Remove(ConverterClienteConsulta);
                        
                    //salvando a lista em uma SESSION
                    Session["GuardarLista"] = Clientes;

                    //atualizando a gridview ("tabela")
                    gvClientes.DataSource = Session["GuardarLista"];
                    gvClientes.DataBind();

                    lbMsgSucess.Text = "Cliente foi excluido com sucesso.";
                    lbMsgSucess.Visible = true;
                    lbMsgError.Visible = false;

                    gvClientes.Visible = true;
                    gvRelatorio.Visible = false;
                }

                var verificarCodigo = Clientes.Where(c => c.Codigo == Convert.ToInt32(tbCodigo.Text)).Select(c => c);
            }

            catch (Exception error)
            {
                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }
        }

        //ORDENAR
        private void OrdenarPorLimiteCreditoClientes()
        {
            try
            {
                if (Clientes.Count <= 1)
                {
                    throw new Exception("Para que seja possivel realizar a ordenação é necessario que a lista possua mais de 1 (um) cliente");
                }
                //ordenando a lista
                var ClienteConsulta = Clientes.OrderBy(c => c.Limite_Credito);

                Clientes = new List<Cliente>(ClienteConsulta);

                //salvando a lista em uma SESSION
                Session["GuardarLista"] = Clientes;

                //atualizando a gridview ("tabela")
                gvClientes.DataSource = Session["GuardarLista"];
                gvClientes.DataBind();

                gvClientes.Visible = true;
                gvRelatorio.Visible = false;

                lbMsgSucess.Text = "Ordenação concluida com sucesso!";
                lbMsgSucess.Visible = true;
                lbMsgError.Visible = false;
            }
            catch (Exception error)
            {

                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
            }
        }

        private bool ValidarCamposObrigatorios()
        {
            try
            {

                if (tbNome.Text.Equals(String.Empty))
                {
                    throw new Exception("Campo (Nome) é obrigatorio.");
                }
                else if (tbNome.Text.Length <= 3)
                {
                    throw new Exception("Campo (Nome) deve ter no minimo 4 caracteres.");
                } 
                else if (tbNascimento.Text.Equals(String.Empty))
                {
                    throw new Exception("Campo (Data Nascimento) é obrigatorio.");
                }
                else if (tbNascimento.Text.Length != 10)
                {
                    throw new Exception("Campo (Data Nascimento) não está completo ou em um formato valido.");
                }
                else if (tbNumero.Text.Equals(String.Empty))
                {
                    throw new Exception("Campo (Numero) é obrigatorio.");
                }
                else if (tbLimiteCredito.Text.Equals(String.Empty))
                {
                    throw new Exception("Campo (Limite de Credito) é obrigatorio.");
                }

                return true;
            }
            catch (Exception error)
            {

                lbMsgError.Text = error.Message;
                lbMsgError.Visible = true;
                lbMsgSucess.Visible = false;
                return false;
            }
        }

        private void LimparCampos()
        {
            tbNome.Text = "";
            tbNascimento.Text = "";
            tbCEP.Text = "";
            tbLogradouro.Text = "";
            tbNumero.Text = "";
            tbBairro.Text = "";
            tbCidade.Text = "";
            tbUF.Text = "";
            tbTelefone.Text = "";
            tbLimiteCredito.Text = "";
        }

    }
}