using CapaDTO;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebServices.Controllers
{
    public class IngredientesController : ApiController
    {

        [HttpGet]
        [Route("api/ingrediente")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ObteneIngredientes()
        {
            try
            {
                IngredientesNegocio auxIngrediente = new IngredientesNegocio();
                List<Ingrediente> ingrediente = auxIngrediente.ObtenerIngredientes();

                Response response = new Response
                {
                    Code = 200,
                    Message = "Ingredientes",
                    Data = ingrediente
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
        [Route("api/ingrediente/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarIngrediente([FromBody] Ingrediente ingrediente)
        {
            try
            {
                IngredientesNegocio auxIngrediente = new IngredientesNegocio();
                string mensaje = auxIngrediente.AgregarIngrediente(ingrediente);

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
        [Route("api/ingrediente/actualizar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult ActualizarIngrediente(Ingrediente ingrediente)
        {
            try
            {
                IngredientesNegocio auxIngrediente = new IngredientesNegocio();
                string message = auxIngrediente.ActualizarIngrediente(ingrediente);

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
        [Route("api/ingrediente/eliminar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult EliminarIngrediente(int id)
        {
            try
            {
                IngredientesNegocio auxIngrediente = new IngredientesNegocio();
                string mensaje = auxIngrediente.EliminarIngrediente(id);

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
