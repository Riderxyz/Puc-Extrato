using Cors.ConfigProfiles;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [Route("api/projetos/GetProjetosAtualizar/codigo/{codigo}/codprojeto/{codprojeto}/nome/{nome}/descricao/{descricao}/inicio/{inicio}/coordenador/{coordenador}/contaPrincipal/{contaPrincipal}/tipo_Projeto/{tipo_Projeto}/")]
        public IEnumerable<string> GetProjetosAtualizar(int codigo, string codprojeto, string nome, string descricao, DateTime inicio, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosAtualizar(codigo, codprojeto, nome, descricao, inicio, coordenador, contaPrincipal, tipo_Projeto);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/projetos/GetProjetosIncluir/codprojeto/{codprojeto}/nome/{nome}/descricao/{descricao}/inicio/{inicio}/coordenador/{coordenador}/contaPrincipal/{contaPrincipal}/tipo_Projeto/{tipo_Projeto}/")]
        public IEnumerable<string> GetProjetosIncluir(string codprojeto, string nome, string descricao, DateTime inicio, int coordenador, string contaPrincipal, string tipo_Projeto)
        {
            projetoNegocios projeto = new projetoNegocios();
            yield return projeto.GetProjetosIncluir(codprojeto, nome, descricao, inicio, coordenador, contaPrincipal, tipo_Projeto);
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
