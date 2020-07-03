using System;
using System.Collections.Generic;
using System.Linq;
using CapaDTO;
using CapaConexion;
using System.Data;

namespace CapaNegocio
{
    public class FotosNegocio
    {
        private string table;

        public FotosNegocio() { Table = "Fotos"; }

        public string Table { get => table; set => table = value; }

        /// <summary>
        /// Agrega registro de una foto y devuelve el id de la foto ingresada
        /// Recibe el Url de la foto subida en firebase
        /// </summary>
        /// <param name="foto"></param>
        /// <returns></returns>
        public int AgregaFoto(Fotos foto)
        {
            try
            {
                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query = $"INSERT INTO {Table} (" +
                    $"nombre, url, created_at) VALUES (" +
                    $"'{foto.Nombre}', " +
                    $"'{foto.Url}', " +
                    $"'{hoy}'); SELECT SCOPE_IDENTITY() as id;";

                // Id Ultima foto subida
                int id = new Conexion().EjecutarQueryAndReturnId(Query);


                if (id.Equals(null))
                    throw new Exception("No se logro agregar la carta, reintente");


                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

    }
}
