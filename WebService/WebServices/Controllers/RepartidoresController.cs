using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using CapaDTO;

namespace WebServices.Controllers
{
    public class RepartidoresController : ApiController
    {
        [HttpGet]
        [Route("api/repartidor")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ObteneRepartidores()
        {
            try
            {
                RepartidorNegocio auxRepartidor = new RepartidorNegocio();
                List<Repartidor> repartidors = auxRepartidor.ObtenerRepartidores();

                Response response = new Response
                {
                    Code = 200,
                    Message = "Repartidores",
                    Data = repartidors
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
        [Route("api/repartidor/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarRepartidor([FromBody] Repartidor repartidor)
        {
            try
            {
                RepartidorNegocio auxRepartidor = new RepartidorNegocio();
                string mensaje = auxRepartidor.AgregarRepartidor(repartidor);

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
        [Route("api/repartidor/actualizar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ActualizarRepartidor(Repartidor repartidor)
        {
            try
            {
                RepartidorNegocio auxIngrediente = new RepartidorNegocio();
                string message = auxIngrediente.ActualizarRepartidor(repartidor);

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
        [Route("api/repartidor/eliminar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult EliminarRepartidor(int id)
        {
            try
            {
                RepartidorNegocio auxRepartidor = new RepartidorNegocio();
                string mensaje = auxRepartidor.EliminarRepartidor(id);

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
