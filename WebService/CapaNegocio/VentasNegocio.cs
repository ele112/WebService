using System;
using System.Collections.Generic;
using System.Data;
using CapaDTO;
using CapaConexion;
using System.Linq;

namespace CapaNegocio
{
    public class VentasNegocio
    {
        private string table;

        public string Table { get => table; set => table = value; }

        public VentasNegocio() { Table = "Venta"; }

        public List<Venta> ObtenerVentas()
        {
            try
            {
                string Query = $"SELECT * FROM {Table}";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Venta> ventas = ConvertToList<Venta>(ResultDT);

                return ventas;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string AgregarVenta(Venta venta)
        {
            try
            {
                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query = $"INSERT INTO {Table} (" +
                    $"id_carrito, fecha, sub_total, total, propina, id_usuario) VALUES (" +
                    $"{venta.IdCarrito}, " +
                    $"'{hoy}', " +
                    $"{venta.SubTotal}, " +
                    $"{venta.Total}, " +
                    $"{venta.Propina}, " +
                    $"{venta.IdUsuario})";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro crear la venta, por favor reintente.");
                }

                return "Venta Creada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string ActualizarVenta(Venta venta)
        {
            try
            {
                if(venta.Id.Equals(null)){
                    throw new Exception("Venta no existe!");
                }
                bool existe = ExisteVenta(venta.Id);
                if (!existe) { throw new Exception("Venta no existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"sub_total = {venta.SubTotal}, " +
                    $"total = {venta.Total}, " +
                    $"propina = {venta.Propina}, " +
                    $"id_usuario = {venta.IdUsuario}" +
                    $"WHERE id = {venta.Id}";
                   
                bool exito = new Conexion().EjecutarQuery(Query);

                if (!exito) {
                    throw new Exception("No se logro actualizar la venta, por favor reintente.");
                }

                return "Venta Actualizada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string EliminarVenta(int id)
        {
            try
            {
                if (id.Equals(null)){
                    throw new Exception("Debe ingresar el id de la venta eliminar!");
                }
                bool existe = ExisteVenta(id);
                if (!existe){
                    throw new Exception("Venta No existe!");
                }

                string Query = $"DELETE FROM {Table} WHERE rut = '{id}' ";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro eliminar la venta, por favor reintente.");
                }

                return "Venta eliminada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }


        public List<Venta> ObtenerVenta(int id)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()) ){
                    throw new Exception("Debe ingresar el id de la venta a eliminar!");
                }

                string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Venta> venta = ConvertToList<Venta>(ResultDT);

                return venta;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }


        // Valida si esite una venta
        private bool ExisteVenta(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }

        //Convierte una tabla en una Lista del modelo pasado
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower().Trim()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row => {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                }
                return objT;
            }).ToList();
        }

    }
}
