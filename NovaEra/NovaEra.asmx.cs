using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NovaEraPortais.banco;
using System.Data;
using NovaEraPortais.fplf.bases;
using NovaEraPortais.Autenticar;
using NovaEraPortais.Movimentos;
using NovaEraPortais.Projetos;
using NovaEraPortais.SaldoProjetos;
using NovaEraPortais.projetos2;
using NovaEraPortais.Excel;


namespace NovaEraPortais.Servico
{
    /// <summary>
    /// Summary description f Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public List<NovaEraPortais.Autenticar.basecamposcoordenadores> ValidarCoordenador(string usuario, string senha)
        {
            NovaEraPortais.Autenticar.Autenticar Conexao = new Autenticar.Autenticar();
            return Conexao.Conectar(usuario, senha);
        }

        [WebMethod]
        public List<NovaEraPortais.fplf.bases.basecamposcoordenadoresprojetos> GetProjetosCoordenadores()
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.fplf.bases.CoordenadorProjetos coordenador = new fplf.bases.CoordenadorProjetos();
            coordenador.listaProjetos();
            return coordenador.Linhas;
        }

        [WebMethod]
        public List<NovaEraPortais.Projetos.basecampos_Projetos> LIstarProjetos(string Coordenador)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.Projetos.class_Projetos projetos = new NovaEraPortais.Projetos.class_Projetos();
            projetos.ListaProjetos(Coordenador);
            return projetos.Linhas;
        }

        [WebMethod]
        public List<NovaEraPortais.Projetos.basecampos_Projetos> LIstarProjetosFull(string Coordenador)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.Projetos.class_Projetos projetos = new NovaEraPortais.Projetos.class_Projetos();
            projetos.ListaProjetos(Coordenador);
            return projetos.Linhas;
        }

        [WebMethod]
        public List<NovaEraPortais.Movimentos.basecampos_view_extrato> ListarMovimentos(string parmCoordenador, string projeto, string inicio, string fim, String NomePLanilha, String titulos)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.Movimentos.class_view_extrato extrato = new class_view_extrato();
            extrato.ListaView_extrato(parmCoordenador , projeto, inicio, fim, titulos);
            return extrato.Linhas;
        }


        [WebMethod]
        public List<NovaEraPortais.SaldoProjetos.basecampos_vw_int_saldosProjetos> SaldoProjetosCoordenador(string coordenador, string inicio)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.SaldoProjetos.class_vw_int_saldosProjetos saldoProjetos = new class_vw_int_saldosProjetos();
            saldoProjetos.ListaVw_int_saldosprojetos(coordenador, inicio);

            return saldoProjetos.Linhas;
        }

        [WebMethod]
        public void TesteGravar(basecampos_projetos campo)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.projetos2.class_projetos grupos = new NovaEraPortais.projetos2.class_projetos();
            grupos.inserirRegistro(campo);
            //return projetos.GravarProjeto(texto);
        }

        [WebMethod]
        public List<NovaEraPortais.vw_contamae.basecampos_vw_contamae> GetListOf_Vw_contamae(String parm_coordenador)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.vw_contamae.class_vw_contamae vw_contamae = new NovaEraPortais.vw_contamae.class_vw_contamae();
            vw_contamae.GetListOf_Vw_contamae(parm_coordenador);
            return vw_contamae.Linhas;
        }


        [WebMethod]
        public List<NovaEraPortais.vw_contamae.basecampos_vw_contamae> GetRangeOf_vw_contamae(String parm_coordenador, String parm_chave, String inicio, String final)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.vw_contamae.class_vw_contamae vw_contamae = new NovaEraPortais.vw_contamae.class_vw_contamae();
            vw_contamae.GetRangeOf_Vw_contamae(parm_coordenador, parm_chave, inicio, final);
            return vw_contamae.Linhas;
        }


        [WebMethod]
        public List<NovaEraPortais.vw_contamae.basecampos_vw_contamae> GetUnique_Vw_contamae(String parm_coordenador, String parm_chave)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.vw_contamae.class_vw_contamae vw_contamae = new NovaEraPortais.vw_contamae.class_vw_contamae();
            vw_contamae.GetUnique_Vw_contamae(parm_coordenador, parm_chave);
            return vw_contamae.Linhas;
        }

        [WebMethod]
        public List<NovaEraPortais.View_Extrato2.basecampos_View_Extrato2> GetListOf_View_extrato2(String parm_coordenador)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.View_Extrato2.class_View_Extrato2 View_Extrato2 = new NovaEraPortais.View_Extrato2.class_View_Extrato2();
            View_Extrato2.GetListOf_View_extrato2(parm_coordenador);
            return View_Extrato2.Linhas;
        }


        [WebMethod]
        public List<NovaEraPortais.View_Extrato2.basecampos_View_Extrato2> GetRangeOf_View_extrato2(String parm_coordenador, String parm_chave, String inicio, String final, String titulos)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.View_Extrato2.class_View_Extrato2 View_Extrato2 = new NovaEraPortais.View_Extrato2.class_View_Extrato2();
            View_Extrato2.GetRangeOf_View_extrato2(parm_coordenador, parm_chave, inicio, final, titulos);
            return View_Extrato2.Linhas;
        }


        [WebMethod]
        public List<NovaEraPortais.View_Extrato2.basecampos_View_Extrato2> GetUnique_View_extrato2(String parm_coordenador, String parm_chave, String titulos)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.View_Extrato2.class_View_Extrato2 View_Extrato2 = new NovaEraPortais.View_Extrato2.class_View_Extrato2();
            View_Extrato2.GetUnique_View_extrato2(parm_coordenador, parm_chave,titulos);
            return View_Extrato2.Linhas;
        }

        [WebMethod]
        public String GetExcelOf_View_extrato2(String parm_coordenador, String _filtro, String NomePlanilha, String _titulos)
        {
            NovaEraPortais.banco.DB Banco = new DB();
            NovaEraPortais.Movimentos.class_view_extrato view_extrato = new NovaEraPortais.Movimentos.class_view_extrato();
            view_extrato._GetExcelOf_View_extrato2(parm_coordenador, _filtro, NomePlanilha, _titulos);
            return "ok";
        }

        [WebMethod]
        public String Palavra()
        {
            List<String> _evangelho = new List<string>();
            _evangelho.Add("A memória de Nossa Senhora Rainha que celebramos hoje, foi instituída por Pio XII, em 1955. Ela é Rainha porque Mãe do Rei, de um Reino que é de amor e justiça. Biblicamente, justiça é amor de Deus para todos.");
            _evangelho.Add("Quando Isabel estava no sexto mês de gravidez, Deus enviou o anjo Gabriel a uma cidade da Galiléia chamada Nazaré. O anjo levava uma mensagem para uma virgem que tinha casamento contratado com um homem chamado José, descendente do rei Davi. Ela se chamava Maria. O anjo veio e disse:");
            _evangelho.Add("Que a paz esteja com você, Maria! Você é muito abençoada. O Senhor está com você. Porém Maria, quando ouviu o que o anjo disse, ficou sem saber o que pensar. E, admirada, ficou pensando no que ele queria dizer. Então o anjo continuou:");
            return _evangelho.ToString();
        }
    }
}