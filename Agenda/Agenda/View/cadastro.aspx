<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cadastro.aspx.cs" Inherits="CadastroProdutos.view.cadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="cadastroEstilo.css" rel="stylesheet" />
    <title>Agenda</title>
</head>
<body>
    <hr/>
        <h1 class="tabTitulo">Agenda</h1>
     <hr />
    <form id="form1" runat="server">
        <section>
            <asp:Label  ID="Label1" runat="server" Text="ID Contato" Width="100px"></asp:Label>
            <asp:TextBox class="posCamposPrimeiraCol" ID="txtIdContato" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <br />

            <asp:Label ID="Label2" runat="server" Text="Nome" Width="200px"></asp:Label>
            <asp:TextBox class="posCamposPrimeiraCol" ID="txtNome" Width="465px" runat="server" MaxLength="100" ></asp:TextBox>
            <br />
            <br />

            <asp:Label ID="Label3" runat="server" Text="Telefone" Width="100px"></asp:Label>
            <asp:TextBox type="text" class="posCamposPrimeiraCol" ID="txtTelefone" runat="server"></asp:TextBox>

            <asp:Label class="posLabelSegundaCol" ID="Label4" runat="server" Text="E-mail" Width="100px"></asp:Label>
            <asp:TextBox type="text" class="posCamposSegundaCol" ID="txtEmail"  runat="server"></asp:TextBox>
         
            <br />
            <br />
            
            <div class="centralizar">
                <asp:Button ID="btnLimpar" runat="server" Text="Limpar"
                    OnClick="btnLimpar_Click" />
                &nbsp; &nbsp; 

                <asp:Button ID="btnInserir" runat="server" Text="Inserir"
                    OnClick="btnInserir_Click" />
                &nbsp; &nbsp;                
                
                <asp:Button ID="btnAlterar" runat="server" Text="Alterar"
                    Enabled="False" OnClick="btnAlterar_Click" />
                &nbsp; &nbsp; 
                
                <asp:Button ID="btnExcluir" runat="server" Text="Excluir"
                    Enabled="False" OnClick="btnExcluir_Click" />
             <br />
             <br /> 
            </div>
           
             <hr class="linhaSep"/>
 
            <div class="msgErro"> 
                  <asp:Label ID="lblMsgErro" runat="server"
                    ForeColor="Red"></asp:Label>
            </div>

            <br />
            <div class="tabTitulo"> Contatos </div>
            <br />

            <div class="centralizarElementos">

                <asp:GridView class="tabGeral" ID="gvProduto" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="700px" OnRowDataBound="gvProduto_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                    <Columns>
                        <asp:TemplateField HeaderText="ID" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="colIDContato" Text='<%# Eval("idcontato") %>' runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle CssClass="gvItemCentro" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Nome" ItemStyle-Width="400px">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Nome") %>' runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Telefone" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("telefone") %>' runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle CssClass="gvItemDireita" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:Label ID="colEmail" Text='<%# Eval("email") %>' runat="server" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle CssClass="gvItemCentro" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Alterar" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/View/Pictures/edit.png" runat="server" 
                                    OnClick="ImgEditar_Click" CommandArgument='<%# Eval("idcontato")  %>' 
                                    ToolTip="Editar" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle CssClass="gvItemCentro" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/View/Pictures/delete.png" runat="server"
                                    OnClick="ImgExcluir_Click" CommandArgument='<%# Eval("idcontato")  %>'
                                    ToolTip="Excluir" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="gvHeader" />
                            <ItemStyle CssClass="gvItemCentro" />
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>           

        </section>
    </form>
</body>
</html>