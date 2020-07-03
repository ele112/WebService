using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CapaDTO;
using CapaNegocio;

namespace WebServices.Controllers
{
    public class CartasController : ApiController
    {

        [HttpGet]
        [Route("api/cartas")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ObtenerCartas()
        {
            try
            {
                CartasNegocio auxCarta = new CartasNegocio();
                List<Carta> cartas = auxCarta.ObtenerCartas();

                Response response = new Response
                {
                    Code = 200,
                    Message = "Cartas",
                    Data = cartas
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
        [Route("api/cartas/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarCarta([FromBody] Carta carta)
        {
            try
            {
                CartasNegocio auxCarta = new CartasNegocio();
                string mensaje = auxCarta.AgregarCarta(carta);

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


        [HttpGet]
        [Route("api/cartas/eliminar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult EliminarIngrediente(int id)
        {
            try
            {
                CartasNegocio auxCarta = new CartasNegocio();
                string mensaje = auxCarta.EliminarCarta(id);

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
        [Route("api/cartas/asignar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AsignarPromocion(Carta carta)
        {
            try
            {
                CartasNegocio auxCarta = new CartasNegocio();
                string mensaje = auxCarta.AsignarPromocion(carta);

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
