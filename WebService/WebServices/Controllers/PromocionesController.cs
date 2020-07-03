using CapaDTO;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebServices.Controllers
{
    public class PromocionesController : ApiController
    {
        [HttpPost]
        [Route("api/promociones/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarPromo([FromBody] Promocion promo)
        {
            try
            {
                PromocionesNegocio auxPromo = new PromocionesNegocio();
                string mensaje = auxPromo.AgregarPromociones(promo);

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
        [Route("api/promociones")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ObtenerPromociones()
        {
            try
            {
                PromocionesNegocio auxPromo = new PromocionesNegocio();
                List<Promocion> promo = auxPromo.ObtenerPromociones();

                Response response = new Response
                {
                    Code = 200,
                    Message = "Promociones",
                    Data = promo
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
        [Route("api/promociones/actualizar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ActualizarPromocion(Promocion promocion)
        {
            try
            {
                PromocionesNegocio auxPromo = new PromocionesNegocio();
                string message = auxPromo.ActualizarPromocion(promocion);

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
        [Route("api/promociones/eliminar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult EliminarPromocion(int id)
        {
            try
            {
                PromocionesNegocio auxPromo = new PromocionesNegocio();
                string mensaje = auxPromo.EliminarPromocion(id);

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
