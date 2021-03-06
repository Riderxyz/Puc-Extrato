﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Cors.ConfigProfiles;

namespace ctrlProjetoService.Controllers
{
    [RoutePrefix("Coordenador")]
    public class CoordenadorController : ApiController
    {

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Listar")]
        public IEnumerable<string> Listar(string nome = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorLista(nome);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GerarSenhaCoordenador")]
        public IEnumerable<string> GerarSenhaCoordenador(Int32 Coord)
        {
            Negocios_C.projetos projeto = new Negocios_C.projetos();
            yield return projeto.GerarSenhaCoordenador(Coord);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Coordenador_ProjetosListar")]
        public IEnumerable<string> Coordenador_ProjetosListar(string nome = "")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.Coordenador_ProjetosListar(nome);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("GetCoordenadorById")]
        public IEnumerable<string> GetCoordenadorById(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorById(id);
        }
               
        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Incluir")]
        public IEnumerable<string> Incluir(string nome, string email="",string senha="")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorIncluir(nome, email,senha);
        }

        [EnableCors("*", "*", "*")]
        [HttpGet]
        [Route("Atualizar")]
        public IEnumerable<string> Atualizar(int id, string nome = "", string email = "", string senha="")
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.GetCoordenadorUpdate(id, nome, email,senha);
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        [Route("Excluir")]
        public IEnumerable<string> Excluir(int id)
        {
            coordenadorNegocio coordenador = new coordenadorNegocio();
            yield return coordenador.CoordenadorExcluir(id);
        }
    }
}
