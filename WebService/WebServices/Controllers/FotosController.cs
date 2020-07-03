using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CapaDTO;
using CapaNegocio;
using Newtonsoft.Json.Linq;

namespace WebServices.Controllers
{
    public class FotosController : ApiController
    {

        [Route("api/fotos/agregar")]
        [ResponseType(typeof(Response))]
        public IHttpActionResult AgregarFoto([FromBody] Fotos foto)
        {
            try
            {
                FotosNegocio auxFoto = new FotosNegocio();
                int id = auxFoto.AgregaFoto(foto);

                Response response = new Response
                {
                    Code = 200,
                    Message = "Imagen guardada!",
                    Data = JObject.Parse("{id: "+id+"}")

                };

                return Ok(response);

            }
            catch(Exception ex)
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
