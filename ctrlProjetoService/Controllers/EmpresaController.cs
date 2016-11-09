using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Negocios_C;
using Cors.ConfigProfiles;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix ("Empresas")]
    public class EmpresaController : ApiController
    {
        [EnableCors("*", "*", "*")]
        [Route("Listar")]
        [HttpGet]
        public IEnumerable<string> Listar(string nome = "")
        {
            Negocios_C.Empresas EmpresaNegocio = new Empresas();
            yield return EmpresaNegocio.Listar(nome);
        }

        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        [HttpGet]
        public IEnumerable<string> Excluir(int id)
        {
            Negocios_C.Empresas EmpresaNegocio = new Empresas();
            yield return EmpresaNegocio.Excluir (id);
        }

        [EnableCors("*", "*", "*")]
        [Route("Atualizar")]
        [HttpGet]
        public IEnumerable<string> Atualizar(int id,string nome,  Int32 banco_num =0, string CNPJ = null,  string Agencia = null, string conta = null, string optanteSimples = null, string Observacao = null, string ISS = null, string Cidade = null)
        {
            Negocios_C.Empresas EmpresaNegocio = new Empresas();
            yield return EmpresaNegocio.Atualizar (id,nome,CNPJ,banco_num ,Agencia,conta,optanteSimples,Observacao,ISS ,Cidade );
        }

        [EnableCors("*", "*", "*")]
        [Route("Incluir")]
        [HttpGet]
        public IEnumerable<string> Incluir (string nome, Int32 banco_num, string CNPJ = null,  string Agencia = null, string conta = null, string optanteSimples = null, string Observacao = null, string ISS = null, string Cidade = null)
        {
            Negocios_C.Empresas EmpresaNegocio = new Empresas();
            yield return EmpresaNegocio.Incluir (nome, banco_num, CNPJ,  Agencia, conta, optanteSimples, Observacao, ISS, Cidade);
        }
    }
}
