using Cors.ConfigProfiles;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Projetos")]
    public class ProjetoController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("ListaProjetosCoordenador")]
        [HttpGet]
        public IEnumerable<string> ListaProjetosCoordenador(int coordenador)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetos(coordenador);
        }

        [EnableCors("*", "*", "*")]
        [Route("Listar")]
        [HttpGet]
        public IEnumerable<string> Listar(string nome = "")
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        [HttpGet]
        public IEnumerable<string> Excluir(int codigo)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosExcluir(codigo);
        }
        //codigo As Integer, 

        [EnableCors("*", "*", "*")]
        [Route("Atualizar")]
        [HttpGet]
        public IEnumerable<string> Atualizar(int codigo, string projeto, [FromUri] string descricao, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projeto = projeto.Replace("!2", "%2");
            string a = HttpContext.Current.Server.UrlDecode(projeto);
            projetoNegocios objprojeto = new projetoNegocios();
            yield return objprojeto.GetProjetosAtualizar(codigo, projeto, descricao, coordenador, contaPrincipal, tipo_Projeto);
        }

        [EnableCors("*", "*", "*")]
        [System.Web.Http.Route("Incluir")]
        [HttpGet]
        public IEnumerable<string> Incluir(string nome, string descricao, DateTime inicio, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosIncluir(nome, descricao, inicio, coordenador, contaPrincipal, tipo_Projeto);
        }
    }
}
