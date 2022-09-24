<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cadastro_Cliente_WebForms.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estrutura de Dados</title>

    <!--Style BootStrap-->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />

    <style>
        h1 {
            text-align: center;
            margin: 25px 0 15px 0;
        }

        .btn, #tbLimiteRelatorio, #tbCodigo, #lbLimiteRelatorio {
            margin-top: 25px;
        }

        .separar {
            border-bottom: 1px solid black;
            padding: 10px;
            margin-bottom: 25px;
        }

        #gvClientes {
            margin-top: 10px;
        }
    </style>

    <script src="Scripts/ValidacaoInputs.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            mascaraTelefone(document.getElementById('tbTelefone'));
        }     

        //mascara CEP
        //bloqueador de caracteres sem (-)
        function BlockRemover(field) {
            field = document.getElementById(field);
            let Letters = /[qwertyuiopasdfghjklçzxcvbnm\b]/g
            let Symbols = /[[\]\/{}()*+?%&!?¨:.,;'"<>~=\\^$|#\b]/g
            document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Letters, "");
            document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Symbols, "");

            MaskCEP(field)
        }

        function MaskCEP(field) {

            let fieldFormat = document.getElementById(field.id).value + ""
            if (fieldFormat.length == 5) {
                document.getElementById(field.id).value = fieldFormat + "-"
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Cadastro de Clientes</h1>

            <div class="container">
                <div class="row separar">
                    <section class="col-2 mb-3">
                        <asp:TextBox ID="tbCodigo" type="Number" runat="server" CssClass="form-control" placeholder="Adicione um código" onKeyUp="return BlockChar(this.id)"></asp:TextBox>
                    </section>

                    <section class="col-2 mb-3">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-primary form-control" OnClick="btnConsultar_Click" />
                    </section>

                    <section class="col-2 mb-3">                        
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-danger form-control" OnClick="btnExcluir_Click" />
                    </section>

                    <section class="col-2 mb-3">                        
                        <asp:Button ID="btnOrdenar" runat="server" Text="Ordenar por Crédito" CssClass="btn btn-secondary form-control" OnClick="btnOrdenar_Click" />
                    </section>

                    <section class="col-2 mb-3">
                        <div class="input-group">                            
                            <asp:Label ID="lbLimiteRelatorio" runat="server" Text="R$:" CssClass="input-group-text"></asp:Label>
                            <asp:TextBox ID="tbLimiteRelatorio" runat="server" CssClass="form-control" placeholder="1800.00" onKeyUp="formatarMoeda(this.id)"></asp:TextBox>
                        </div>
                    </section>

                    <section class="col-2 mb-3">                        
                        <asp:Button ID="btnRelatorio" runat="server" Text="Relatório" CssClass="btn btn-secondary form-control" OnClick="btnRelatorio_Click" />
                    </section>
                    
                    <asp:Label ID="lbMsgError" runat="server" Text="" Visible="false" CssClass="alert alert-danger" role="alert"></asp:Label>
                    <asp:Label ID="lbMsgSucess" runat="server" Text="" Visible="false" CssClass="alert alert-primary" role="alert"></asp:Label>
                </div>
                <div class="row">
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label1" runat="server" Text="Nome:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbNome" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label2" runat="server" Text="Data Nascimento:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbNascimento" runat="server" CssClass="form-control" maxlength="10" onkeypress="mascaraData(this)"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label8" runat="server" Text="CEP:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbCEP" runat="server" CssClass="form-control" MaxLength="9" onKeyUp="return BlockRemover(this.id)"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label3" runat="server" Text="Logradouro:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbLogradouro" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label4" runat="server" Text="Numero:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbNumero" runat="server" type="Number" onkeyup="BlockChar(this.id)" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label5" runat="server" Text="Bairro:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbBairro" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label6" runat="server" Text="Cidade:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbCidade" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label7" runat="server" Text="UF:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbUF" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label9" runat="server" Text="Telefone:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbTelefone" runat="server" CssClass="form-control"></asp:TextBox>
                    </section>
                    <section class="col-4 mb-3">
                        <asp:Label ID="Label10" runat="server" Text="Limite de Credito:" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tbLimiteCredito" runat="server" CssClass="form-control" onKeyUp="formatarMoeda(this.id)"></asp:TextBox>
                    </section>

                    <section class="col-4 mb-3">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-primary form-control" />
                    </section>
                </div>
            </div>

            <div>
                <asp:GridView ID="gvClientes" runat="server" CssClass="table table-primary table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                        <asp:BoundField DataField="Data_Nascimento" HeaderText="Data de Nascimento" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Logradouro" HeaderText="Logradouro" />
                        <asp:BoundField DataField="Numero" HeaderText="Numero" />
                        <asp:BoundField DataField="Bairro" HeaderText="Bairro" />
                        <asp:BoundField DataField="Cidade" HeaderText="Cidade" />
                        <asp:BoundField DataField="UF" HeaderText="UF" />
                        <asp:BoundField DataField="CEP" HeaderText="CEP" />
                        <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                        <asp:BoundField DataField="Limite_Credito" HeaderText="Limite de Credito" DataFormatString="{0:C}"/>
                    </Columns>
                </asp:GridView>
            </div>

            <div>
                <asp:GridView ID="gvRelatorio" runat="server" CssClass="table table-secondary table-hover" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                        <asp:BoundField DataField="Data_Nascimento" HeaderText="Data de Nascimento" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Logradouro" HeaderText="Logradouro" />
                        <asp:BoundField DataField="Numero" HeaderText="Numero" />
                        <asp:BoundField DataField="Bairro" HeaderText="Bairro" />
                        <asp:BoundField DataField="Cidade" HeaderText="Cidade" />
                        <asp:BoundField DataField="UF" HeaderText="UF" />
                        <asp:BoundField DataField="CEP" HeaderText="CEP" />
                        <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                        <asp:BoundField DataField="Limite_Credito" HeaderText="Limite de Credito" DataFormatString="{0:C}"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
