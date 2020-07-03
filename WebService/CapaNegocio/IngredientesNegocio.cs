using System;
using System.Collections.Generic;
using System.Linq;
using CapaDTO;
using CapaConexion;
using System.Data;

namespace CapaNegocio
{
    public class IngredientesNegocio
    {
        private string table;
        public IngredientesNegocio() { Table = "Ingredientes";  }

        public string Table { get => table; set => table = value; }

        /// <summary>
        /// Lista los ingredientes
        /// </summary>
        /// <returns></returns>
        public List<Ingrediente> ObtenerIngredientes()
        {
            try
            {
                string Query = $"select id, nombre, descripcion FROM {Table} order by id desc;";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Ingrediente> ingredientes = ResultDT.DataTableToList<Ingrediente>();

                return ingredientes;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Agrega un nuevo ingrediente
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        public string AgregarIngrediente(Ingrediente ingrediente)
        {
            try
            {
                string Query = $"INSERT INTO {Table} (" +
                    $"nombre, descripcion) VALUES (" +
                    $"'{ingrediente.Nombre}', " +
                    $"'{ingrediente.Descripcion}')";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro crear el ingrediente, por favor reintente.");
                }

                return "Ingrediente creado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Actualiza un ingrediente segun id
        /// </summary>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        public string ActualizarIngrediente(Ingrediente ingrediente)
        {
            try
            {
                if (ingrediente.Id.Equals(null))
                {
                    throw new Exception("Debe ingresar el id del ingrediente");
                }
                bool existe = ExisteIngrediente(ingrediente.Id);
                if (!existe) { throw new Exception("Ingrediente no existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre = '{ingrediente.Nombre}', " +
                    $"descripcion = '{ingrediente.Descripcion}'" +
                    $"WHERE id = {ingrediente.Id}";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro actualizar el ingrediente, por favor reintente.");
                }

                return "Ingrediente Actualizado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Elimina un ingrediente segun Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string EliminarIngrediente(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    throw new Exception("Debe ingresar el id del ingrediente a eliminar!");
                }
                bool existe = ExisteIngrediente(id);
                if (!existe)
                {
                    throw new Exception("Ingrediente no existe!");
                }

                string Query = $"DELETE FROM {Table} WHERE id = '{id}' ";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro eliminar el ingrediente, por favor reintente.");
                }

                return "Ingrediente eliminado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }

        /// <summary>
        /// Obtiene un ingrediente en especificico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Ingrediente> ObteneIngrediente(int id)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    throw new Exception("Debe ingresar el id del ingrediente a eliminar!");
                }

                string Query = $"SELECT id, nombre, descripcion FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Ingrediente> ingrediente = ResultDT.DataTableToList<Ingrediente>();

                return ingrediente;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }


        // Valida si esite una carta
        private bool ExisteIngrediente(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }



    }
}
