using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using CapaDTO;

namespace WebServices.Controllers
{
    public class EstadosController : ApiController
    {

        [HttpGet]
        [Route("api/estado")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ObteneEstado()
        {
            try
            {
                EstadoNegocio auxEstado = new EstadoNegocio();
                List<Estado> estados = auxEstado.ObtenerEstados();

                Response response = new Response
                {
                    Code = 200,
                    Message = "Estados",
                    Data = estados
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                Response response = new Response
                {
                    Code = 400,
                    Message = ex.Message
                };

                return Ok(response);
            }
        }

        [HttpPost]
        [Route("api/estado/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarEstado([FromBody] Estado estado)
        {
            try
            {
                EstadoNegocio auxEstado = new EstadoNegocio();
                string mensaje = auxEstado.AgregarEstado(estado);

                Response response = new Response
                {
                    Code = 200,
                    Message = mensaje
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                Response response = new Response
                {
                    Code = 400,
                    Message = ex.Message
                };

                return Ok(response);
            }
        }

        [HttpPost]
        [Route("api/estado/actualizar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ActualizarEstado(Estado estado)
        {
            try
            {
                EstadoNegocio auxIngrediente = new EstadoNegocio();
                string message = auxIngrediente.ActualizarEstado(estado);

                Response response = new Response
                {
                    Code = 200,
                    Message = message
                };
                return Ok(response);

            }
            catch (Exception ex)
            {
                Response response = new Response
                {
                    Code = 400,
                    Message = ex.Message
                };

                return Ok(response);
            }
        }

        [HttpGet]
        [Route("api/estado/eliminar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult EliminarEstado(int id)
        {
            try
            {
               EstadoNegocio auxEstado = new EstadoNegocio();
                string mensaje = auxEstado.EliminarEstado(id);

                Response response = new Response
                {
                    Code = 200,
                    Message = mensaje
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                Response response = new Response
                {
                    Code = 400,
                    Message = ex.Message
                };

                return Ok(response);
            }
        }

    }
}
