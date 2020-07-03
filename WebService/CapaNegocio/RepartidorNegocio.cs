using CapaConexion;
using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RepartidorNegocio
    {


        private string table;
        public RepartidorNegocio() { Table = "Repartidor"; }

        public string Table { get => table; set => table = value; }

        /// <summary>
        /// Obtiene una lista de repartidores
        /// </summary>
        /// <returns>List<Repartidor></returns>
        public List<Repartidor> ObtenerRepartidores()
        {
            try
            {
                string Query = 
                    $"SELECT id, nombre, apellidos, celular, rut, dv, email " +
                    $"FROM {Table} ORDER BY id DESC;";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Repartidor> repartidor = ResultDT.DataTableToList<Repartidor>();

                return repartidor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Agrega un nuevo repartidor
        /// </summary>
        /// <param name="repartidor"></param>
        /// <returns>string</returns>
        public string AgregarRepartidor(Repartidor repartidor)
        {
            try
            {
                string msg = validaRepartidor(repartidor, false);

                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                string Query = 
                    $"INSERT INTO {Table} (nombre, apellidos, celular, rut, dv, email) VALUES (" +
                    $"'{repartidor.Nombre}', " +
                    $"'{repartidor.Apellidos}', " +
                    $"'{repartidor.Celular}', " +
                    $"'{repartidor.Rut}', " +
                    $"'{repartidor.Dv}', " +
                    $"'{repartidor.Email}' )";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                    throw new Exception("No se logro crear el repartidor, por favor reintente.");
                

                return "Repartidor creado!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Actualiza un repartidor
        /// </summary>
        /// <param name="repartidor"></param>
        /// <returns>string</returns>
        public string ActualizarRepartidor(Repartidor repartidor)
        {
            try
            {
                string msg = validaRepartidor(repartidor, true);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre    = '{repartidor.Nombre}', " +
                    $"apellidos = '{repartidor.Apellidos}', " +
                    $"celular   = '{repartidor.Celular}', " +
                    $"rut       = '{repartidor.Rut}', " +
                    $"dv        = '{repartidor.Dv}', " +
                    $"email     = '{repartidor.Email}' " +
                    $"WHERE id  =  {repartidor.Id}";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                    throw new Exception("No se logro actualizar el repartidor, por favor reintente.");

                return "Repartidor Actualizado";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Elimina un repartidor
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string EliminarRepartidor(int id)
        {
            try
            {
                if (id.Equals(null))
                    throw new Exception("Debe ingresar el id del repartidor a eliminar!");
                
                if (!ExisteRepartidor(id))
                    throw new Exception("Repartidor no existe!");

                string Query = $"DELETE FROM {Table} WHERE id = '{id}' ";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                    throw new Exception("No se logro eliminar el repartidor, por favor reintente.");



                return "Repartidor eliminado";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }




        }

        /// <summary>
        /// Obtiene un repartidor es espacifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns> List<Repartidor> </returns>
        public List<Repartidor> ObteneRepartidor(int id)
        {
            try
            {
                if (id.Equals(null))
                    throw new Exception("Debe ingresar el id del repartidor a eliminar!");

                string Query = $"SELECT id, nombre FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Repartidor> repartdor = ResultDT.DataTableToList<Repartidor>();

                return repartdor;
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }


        /// <summary>
        /// Valida si tiene los campos requeridos
        /// </summary>
        /// <param name="repartidor"></param>
        /// <param name="validoExiste"></param>
        /// <returns>string</returns>
        private string validaRepartidor(Repartidor repartidor, bool validoExiste)
        {
            string message = "Existen campos vacios o invalidos, favor reintente";

            if (string.IsNullOrEmpty(repartidor.Nombre))
                return message;

            if (string.IsNullOrEmpty(repartidor.Apellidos))
                return message;

            if (string.IsNullOrEmpty(repartidor.Celular))
                return message;

            if (string.IsNullOrEmpty(repartidor.Rut))
                return message;

            if (string.IsNullOrEmpty(repartidor.Dv))
                return message;

            if (repartidor.Id.Equals(null))
                return "Repartidor No existe";

            if (validoExiste && !ExisteRepartidor(repartidor.Id))
                return "Repartidor no existe!";

            if (!string.IsNullOrEmpty(repartidor.Email) && !validaEmail(repartidor.Email))
                return "Formato de Email no valido";
            string rut = repartidor.Rut.ToString() + '-' + repartidor.Dv.ToString();
            if (!Helper.ValidaRut(rut))
                return "Formato de rut no valido";

            return null;
        }

        /// <summary>
        /// Valida un email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        private bool validaEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        /// <summary>
        /// Valida si esite una carta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        private bool ExisteRepartidor(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }


    }
}
