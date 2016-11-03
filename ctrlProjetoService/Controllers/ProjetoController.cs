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
    public class ProjetoController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("api/projetos/GetListaProjetosCoordenador/coordenador/{coordenador}")]
        public IEnumerable<string> GetListaProjetosCoordenador(int coordenador)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetListaProjetosCoordenador(coordenador);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/projetos/GetProjetosLista/nome/{nome?}")]
        public IEnumerable<string> GetProjetosLista(string nome = "")
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/projetos/GetProjetosExcluir/codigo/{codigo}")]
        public IEnumerable<string> GetProjetosExcluir(int codigo)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosExcluir(codigo);
        }
        //codigo As Integer, 

        [EnableCors("*", "*", "*")]
        [Route("api/projetos/GetProjetosAtualizar/codigo/{codigo}/projeto/{projeto}/descricao/{descricao}/coordenador/{coordenador}/contaPrincipal/{contaPrincipal}/tipo_Projeto/{tipo_Projeto}/")]
        public IEnumerable<string> GetProjetosAtualizar(int codigo, string projeto, [FromUri] string descricao, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projeto = projeto.Replace("!2", "%2");
            string a = HttpContext.Current.Server.UrlDecode(projeto);
            projetoNegocios objprojeto = new projetoNegocios();
            yield return objprojeto.GetProjetosAtualizar(codigo, projeto, descricao, coordenador, contaPrincipal, tipo_Projeto);
        }

        [EnableCors("*", "*", "*")]
        [System.Web.Http.Route("api/projetos/GetProjetosIncluir/nome/{nome}/descricao/{descricao}/inicio/{inicio}/coordenador/{coordenador}/contaPrincipal/{contaPrincipal}/tipo_Projeto/{tipo_Projeto}/")]
        public IEnumerable<string> GetProjetosIncluir(string nome, string descricao, DateTime inicio, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosIncluir(nome, descricao, inicio, coordenador, contaPrincipal, tipo_Projeto);
        }
        // GET: api/Projeto 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Projeto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Projeto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Projeto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Projeto/5
        public void Delete(int id)
        {
        }
    }
}
