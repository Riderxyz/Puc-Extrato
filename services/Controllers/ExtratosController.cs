using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Cors.ConfigProfiles;

namespace services.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ExtratosController : ApiController
    {
        // GET: api/Projetos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/Projetos
        #region Extratos
        [EnableCors("*", "*", "*")]
        [Route("api/extratos/getExtrato/projeto/{projeto}/di/{di}/df/{df}/pagina/{pagina}/pagina_tamanho/{pagina_tamanho}")]
        public IEnumerable<string> Get(int projeto, string di, string df, short pagina, short pagina_tamanho)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetExtrato(projeto, di, df, pagina, pagina_tamanho);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/extratos/getExtratoExcel/projeto/{projeto}/di/{di}/df/{df}/modo/{modo}")]
        public IEnumerable<string> getExtratoExcel(int projeto, string di, string df, char modo)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetExtratoExcel(projeto, di, df);
        }
        #endregion

        [EnableCors("*", "*", "*")]
        public IEnumerable<string> Get(int projeto, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoProjeto(projeto, data);
        }
        #region GetSaldoProjetosExcel
        [EnableCors("*", "*", "*")]
        [Route("api/extratos/GetSaldoProjeto/projeto/{projeto}/di/{di}/df/{df}/pagina/{pagina}/pagina_tamanho/{pagina_tamanho}")]
        public IEnumerable<string> GetSaldoProjetoExcel(int coordenador, string data, string conta, bool modo)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoProjetoExcel(coordenador, data, conta);
        }
        #endregion

        #region Get Saldo das Contas - Utilizada em analise projeto
        [EnableCors("*", "*", "*")]
        [Route("api/extratos/GetSaldosContas/coordenador/{coordenador}/data/{data}")]
        public IEnumerable<string> GetSaldosContas(int coordenador, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoContas(coordenador, data);
        }

        [EnableCors("*", "*", "*")]
        [Route("api/extratos/GetSaldosContasExcel/coordenador/{coordenador}/data/{data}")]
        public IEnumerable<string> GetSaldosContasExcel(int coordenador, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoContasExcel(coordenador, data);
        }
        #endregion

        #region Analise Contas - Listar projetos com saldos por conta
        [EnableCors("*", "*", "*")]
        [Route("api/extratos/GetAnaliseContas/coordenador/{coordenador}/conta/{conta}/data/{data}")]
        public IEnumerable<string> GetAnaliseContas(int coordenador, string conta, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetAnaliseContas(coordenador, conta, data);
        }
        [Route("api/extratos/GetAnaliseContasExcel/coordenador/{coordenador}/conta/{conta}/data/{data}")]
        public IEnumerable<string> GetAnaliseContasExcel(int coordenador, string conta, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetAnaliseContasExcel(coordenador, conta, data);
        }
        #endregion

        //// POST: api/Projetos
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Projetos/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Projetos/5
        //public void Delete(int id)
        //{
        //}
    }
}
