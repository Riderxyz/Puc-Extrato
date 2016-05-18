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
        public IEnumerable<string> Get(int projeto, string di, string df, short pagina, short pagina_tamanho)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetExtrato(projeto, di, df, pagina, pagina_tamanho);
        }

        public IEnumerable<string> getExtratoExcel(int projeto, string di, string df)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetExtratoExcel(projeto, di, df);
        }

        public IEnumerable<string> Get(int projeto, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoProjeto(projeto, data);
        }

        public IEnumerable<string> GetSaldosContas(int coordenador, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetSaldoContas(coordenador, data);
        }

        public IEnumerable<string> GetAnaliseContas(int coordenador, string conta, string data)
        {
            ExtratoNegocios extratos = new ExtratoNegocios();
            yield return extratos.GetAnaliseContas(coordenador, conta, data);
        }

        // GET: api/Projetos/5

        // POST: api/Projetos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Projetos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Projetos/5
        public void Delete(int id)
        {
        }
    }
}
