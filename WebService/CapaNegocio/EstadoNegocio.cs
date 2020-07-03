using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaConexion;
using CapaDTO;

namespace CapaNegocio
{
    public class EstadoNegocio
    {

        private string table;
        public EstadoNegocio() { Table = "Estados"; }

        public string Table { get => table; set => table = value; }

        public List<Estado> ObtenerEstados()
        {
            try
            {
                string Query = $"select id, nombre FROM {Table} order by created_at desc;";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Estado> estados = ResultDT.DataTableToList<Estado>();

                return estados;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string AgregarEstado(Estado estado)
        {
            try
            {
                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query = $"INSERT INTO {Table} (nombre, created_at) VALUES (" +
                    $"'{estado.Nombre}', '{hoy}')";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro crear el estado, por favor reintente.");
                }

                return "estado creado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string ActualizarEstado(Estado estado)
        {
            try
            {
                if (estado.Id.Equals(null))
                {
                    throw new Exception("Debe ingresar el id del estado");
                }
                bool existe = ExisteEstado(estado.Id);
                if (!existe) { throw new Exception("Estado no existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre = '{estado.Nombre}' " +
                    $"WHERE id = {estado.Id}";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro actualizar el estado, por favor reintente.");
                }

                return "Estado Actualizado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string EliminarEstado(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    throw new Exception("Debe ingresar el id del estado a eliminar!");
                }
                bool existe = ExisteEstado(id);
                if (!existe)
                {
                    throw new Exception("Estado no existe!");
                }

                string Query = $"DELETE FROM {Table} WHERE id = '{id}' ";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro eliminar el estado, por favor reintente.");
                }

                return "Estado eliminado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }

        public List<Estado> ObteneEstado(int id)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    throw new Exception("Debe ingresar el id del estado a eliminar!");
                }

                string Query = $"SELECT id, nombre FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Estado> estado = ResultDT.DataTableToList<Estado>();

                return estado;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }


        // Valida si esite una carta
        private bool ExisteEstado(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }


    }
}
