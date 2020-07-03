using System;
using System.Collections.Generic;
using System.Linq;
using CapaDTO;
using CapaConexion;
using System.Data;

namespace CapaNegocio
{
    public class CartasNegocio
    {
        private string table;

        public string Table { get => table; set => table = value; }

        public CartasNegocio() { Table = "Cartas"; }

        /// <summary>
        /// Listar todas las cartas
        /// </summary>
        /// <returns></returns>
        public List<Carta> ObtenerCartas()
        {
            try
            {

                string Query =
                    $"SELECT " +
                        $"CA.id, " +
                        $"CA.nombre, " +
                        $"CA.descripcion, " +
                        $"CA.precio_unidad, " +
                        $"CA.id_foto," +
                        $"FO.url as UrlFoto, " +
                        $"CA.id_promocion " +
                    $"FROM Cartas CA " +
                    $"INNER JOIN Fotos FO on CA.id_foto = FO.id " +
                    $"ORDER BY CA.created_at DESC;";

                DataSet result = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = result.Tables[0];
                List<Carta> cartas = ResultDT.DataTableToList<Carta>();



                List<Carta> nuevo_cartas = new List<Carta>();
                // Por cada carta se obtiene los infredientes y su promocion
                foreach (Carta _carta in cartas)
                {
                    //Promociones por carta
                    string QueryPromociones =
                        $"SELECT " +
                            $"id, " +
                            $"nombre, " +
                            $"descripcion, " +
                            $"porc_descuento, " +
                            $"max_descuento " +
                        $"FROM Promociones " +
                        $"WHERE id = {_carta.Id}; ";

                    DataSet PromoDS = new Conexion().EjecutarSelect(QueryPromociones);
                    DataTable PromoDT = PromoDS.Tables[0];
                    List<Promocion> promo = PromoDT.DataTableToList<Promocion>();


                    // Ingredientes por carta
                    string QueryIngrediente =
                       $"SELECT " +
                           $"ING.id, " +
                           $"ING.nombre, " +
                           $"ING.descripcion " +
                      $"FROM Carta_ingredientes CI " +
                      $"INNER JOIN Ingredientes ING on CI.id_ingrediente = ING.id " +
                      $"WHERE CI.id_carta = {_carta.Id}; ";
                    DataSet dt = new Conexion().EjecutarSelect(QueryIngrediente);
                    DataTable dataTable = dt.Tables[0];
                    List<Ingrediente> ingrediente = dataTable.DataTableToList<Ingrediente>();


                    //Seteando valores en nueva lista
                    _carta.Promocion    = promo;
                    _carta.Ingredientes = ingrediente;


                    nuevo_cartas.Add(_carta);
                }

                return nuevo_cartas;
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Agregar una carta
        /// </summary>
        /// <param name="carta"></param>
        /// <returns></returns>
        public string AgregarCarta(Carta carta)
        {
            try
            {
                if (!ValidarCarta(carta))
                    throw new Exception("Existen campos vacios o invalidos, favor reintente");

                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query =
                    $"INSERT INTO {Table} (" +
                    $"nombre, descripcion, precio_unidad, created_at, id_foto) VALUES (" +
                    $"'{carta.Nombre}', " +
                    $"'{carta.Descripcion}', " +
                    $"'{carta.Precio_unidad}'," +
                    $"'{hoy}', " +
                    $"'{carta.Id_foto}'); SELECT SCOPE_IDENTITY() as id;";

                int id = new Conexion().EjecutarQueryAndReturnId(Query);

                if (id.Equals(null))
                    throw new Exception("No se logro agregar la carta, reintente");

                int count = 1;
                int max   = carta.Ingredientes.Count();

                string QueryInsert = "INSERT INTO Carta_ingredientes(id_carta, id_ingrediente) VALUES";
                foreach (var _ingrediente in carta.Ingredientes)
                {
                    QueryInsert += $"({id}, {_ingrediente.Id})" ;
                    QueryInsert += count < max ? ", " : "";

                    count++;
                }


                bool exito = new Conexion().EjecutarQuery(QueryInsert);

                if (!exito)
                    throw new Exception("No se logro agregar la carta, reintente");



                return "Carta creada";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Actualizar una Carta
        /// </summary>
        /// <param name="carta"></param>
        /// <returns></returns>
        public string ActualizarCarta(Carta carta)
        {
            try
            {
                if (carta.Id.Equals(null)) {
                    throw new Exception("Carta no existe!");
                }
                bool existe = ExisteCarta(carta.Id);
                if (!existe) { throw new Exception("Carta no existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre = '{carta.Nombre}', " +
                    $"descripcion = '{carta.Descripcion}', " +
                    $"precio_unidad = {carta.Precio_unidad}, " +
                    $"created_at = '{carta.Created_at}', " +
                    $"id_foto = {carta.Id_foto}, " +
                    $"id_promocion = {carta.Id_promocion}" +
                    $"WHERE id = {carta.Id}";

                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro actualizar la carta, por favor reintente.");
                }

                return "Carta Actualizada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        /// <summary>
        /// Eliminar una carta segun id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string EliminarCarta(int id)
        {
            try
            {
                if (id.Equals(null))
                    throw new Exception("Debe ingresar el id de la Carta eliminar!");

                if (!ExisteCarta(id)) 
                    throw new Exception("Carta no existe!");

                string Query1 = $"DELETE FROM Carta_ingredientes where id_carta = {id}";
                string Query2 = $"DELETE FROM {Table} WHERE id = {id}";

                Conexion cnn = new Conexion();
                bool exito1 = cnn.EjecutarQuery(Query1);
                bool exito2 = cnn.EjecutarQuery(Query2);

                if (!exito1 || !exito2)
                    throw new Exception("No se logro eliminar la carta, por favor reintente.");
                

                return "Carta eliminada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }

        /// <summary>
        /// Asigna una promocion a una carta
        /// </summary>
        /// <param name="carta"></param>
        /// <returns></returns>
        public string AsignarPromocion(Carta carta)
        {
            try
            {
                if (carta.Id.Equals(null))
                    throw new Exception("Debe ingresar el id de la Carta eliminar!");

                int? id = 1;
                if (carta.Id_promocion != 0)
                    id = carta.Id_promocion;

                string Query = $"UPDATE Cartas SET id_promocion = {id} WHERE id = {carta.Id};";

                bool exito = new Conexion().EjecutarQuery(Query);

                if (!exito)
                    throw new Exception("No se logro eliminar la carta, por favor reintente.");


                return "Promocion asignada";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }


        }

        /// <summary>
        /// Obtiene una carta segun id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Carta> ObtenerCarta (int id)
        {
            try
            {

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    throw new Exception("Debe ingresar el id de la carta a eliminar!");
                }

                string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Carta> carta = ResultDT.DataTableToList<Carta>();

                return carta;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }

        /// <summary>
        /// Valida campos necesarios de la carta
        /// </summary>
        /// <param name="carta"></param>
        /// <returns></returns>
        private bool ValidarCarta(Carta carta)
        {
            if (string.IsNullOrEmpty(carta.Nombre))
                return false;
            if (string.IsNullOrEmpty(carta.Descripcion))
                return false;
            if (string.IsNullOrEmpty(carta.Precio_unidad))
                return false;
            if (carta.Ingredientes.Equals(null))
                return false;

            return true;
        }


        // Valida si esite una carta
        private bool ExisteCarta(int id)
        {
            string Query = $"SELECT * FROM {Table} WHERE id = '{id}'; ";
            DataSet dt = new Conexion().EjecutarSelect(Query);
            int count = dt.Tables[0].Rows.Count;

            if (count > 0) { return true; }
            else { return false; }
        }


        //Convierte una tabla en una Lista del modelo pasado
    
    }
}
