using System;
using System.Collections.Generic;
using System.Linq;
using CapaDTO;
using CapaConexion;
using System.Data;

namespace CapaNegocio
{
    public class PromocionesNegocio
    {
        private string table;
        public PromocionesNegocio() { Table = "Promociones"; }

        public string Table { get => table; set => table = value; }

        /// <summary>
        /// Lista todas las proomociones
        /// </summary>
        /// <returns></returns>
        public List<Promocion> ObtenerPromociones()
        {
            try
            {
                string Query = $"SELECT id, nombre, descripcion, porc_descuento," +
                    $" max_descuento, created_at, expired_at" +
                    $" FROM {Table} ORDER BY created_at DESC";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Promocion> promo = ResultDT.DataTableToList<Promocion>();

                return promo;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Agrega una nueva promocion
        /// </summary>
        /// <param name="promocion"></param>
        /// <returns></returns>
        public string AgregarPromociones(Promocion promocion)
        {
            try
            {
                bool valido = ValidaPromo(promocion);
                if (!valido)
                    throw new Exception("Campos invalidos, favor verifique la informacion");


                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query = $"INSERT INTO {Table} (" +
                    $"nombre, descripcion, porc_descuento, max_descuento, created_at) VALUES (" +
                    $"'{promocion.Nombre}', " +
                    $"'{promocion.Descripcion}', " +
                    $"{promocion.Porc_descuento}," +
                    $"{promocion.Max_descuento}, " +
                    $"'{hoy}')";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro crear la promocion, por favor reintente.");
                }

                return "Promocion creada";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Actualiza un promocion
        /// </summary>
        /// <param name="promo"></param>
        /// <returns></returns>
        public string ActualizarPromocion(Promocion promo)   
        {
            try
            {
                if (promo.Id.Equals(null))
                    throw new Exception("Debe ingresar el id de la promocion");

                if(!ValidaPromo(promo))
                    throw new Exception("Campos invalidos, favor verifique la informacion");


                bool existe = ExistePromo(promo.Id);
                if (!existe) { throw new Exception("Promocion no existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre = '{promo.Nombre}', " +
                    $"descripcion = '{promo.Descripcion}', " +
                    $"porc_descuento = {promo.Porc_descuento}, " +
                    $"max_descuento = {promo.Max_descuento} " +
                    $"WHERE id = {promo.Id}";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro actualizar la promocion, por favor reintente.");
                }

                return "Promocion Actualizada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Elimina una promocion segun id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string EliminarPromocion(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    throw new Exception("Debe ingresar el id de la promocion a eliminar!");
                }
                bool existe = ExistePromo(id);
                if (!existe)
                {
                    throw new Exception("Promocion no existe!");
                }

                string Query = $"DELETE FROM {Table} WHERE id = {id}";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito)
                {
                    throw new Exception("No se logro eliminar la promocion, por favor reintente.");
                }

                return "Promocion eliminada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }

        /// <summary>
        /// Obtiene una promocion especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Promocion> ObtenerPromocion(int id)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    throw new Exception("Debe ingresar el id de la promocion a eliminar!");
                }

                string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Promocion> promo = ResultDT.DataTableToList<Promocion>();

                return promo;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }
        

        // Valida si la promocion es valida
        private bool ValidaPromo(Promocion promo)
        {

            try
            {
                if (string.IsNullOrEmpty(promo.Nombre) ||  promo.Nombre.Trim() == "" ) 
                    return false;

                if (string.IsNullOrEmpty(promo.Descripcion) || promo.Descripcion.Trim() == "")
                    return false;

                if (string.IsNullOrEmpty(promo.Porc_descuento.ToString()) || promo.Porc_descuento == 0 )
                    return false;

                return true;
            }
            catch { return false; }
        }

        // Valida si esite una carta
        private bool ExistePromo(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }


    }
}
