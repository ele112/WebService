using System;
using System.Data.SqlClient;
using System.Data;

namespace CapaConexion
{
    public class Conexion
    {
        /// Variables de instancia
        private readonly String cadenaConexion = "Data Source=DESKTOP-UJ30CAJ\\SQLEXPRESS;Initial Catalog=GoGo_DB;Integrated Security=True";

        /// <summary>
        /// Ejecuta la query recibida y retorna un boolena 
        /// si hubieron filas afectadas
        /// </summary>
        /// <param name="query"></param>
        /// <returns>bool</returns>
        public bool EjecutarQuery(string query)
        {
            try
            {
                SqlConnection dbConexion = Conectar();
                SqlCommand variableSQL = new SqlCommand(query, dbConexion);
                int rowsAffected = variableSQL.ExecuteNonQuery();

                if (dbConexion.State == System.Data.ConnectionState.Open)
                    dbConexion.Close();

                if (rowsAffected > 0) return true;
                else return false;

            }catch(Exception ex)
            {
                throw new Exception("Error al ejecutar select " + ex);
            }
        }

        /// <summary>
        /// Ejecuta una query de tipo select y retorna un 
        /// DataSet con la informacion obtenida
        /// </summary>
        /// <param name="query"></param>
        /// <returns>DataSet</returns>
        public DataSet EjecutarSelect(string query)
        {
            try {


                DataSet dataset = new DataSet(); 
                SqlConnection dbConexion = Conectar();

                SqlDataAdapter adapter = new SqlDataAdapter(query, dbConexion);

                adapter.Fill(dataset, "u");

                if (dbConexion.State == System.Data.ConnectionState.Open)
                    dbConexion.Close();


                return dataset;


            } catch (Exception ex)
            {
                throw new Exception("Error al ejecutar select " + ex);
            }
        }

        /// <summary>
        /// Ejecuta una INSERT y retorna el id de la columna insertada
        /// El query debe llevar al final SELECT SCOPE_IDENTITY() as id;
        /// </summary>
        /// <param name="query"></param>
        /// <returns>int</returns>
        public int EjecutarQueryAndReturnId(string query)
        {
            try
            {
                SqlConnection dbConexion = Conectar();
                SqlCommand variableSQL = new SqlCommand(query, dbConexion);

                int modified = Convert.ToInt32(variableSQL.ExecuteScalar());

                if (dbConexion.State == System.Data.ConnectionState.Open) 
                    dbConexion.Close();
               
                return modified;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar select " + ex);
            }
        }

        /// <summary>
        /// Abre una conexion en la cadena de conexion
        /// declarada
        /// </summary>
        /// <returns>SqlConnection</returns>
        private SqlConnection Conectar()
        {
            try
            {
                Boolean valido = this.ValidarConexion(); // Se validan los datos para la conexion
                if (!valido) {
                    string message = "Faltan datos para la conexion.";
                    throw new Exception(message); // Se genera un nueva excepcion
                }

                SqlConnection cnn = new SqlConnection(this.cadenaConexion); // Se crea una conexion
                cnn.Open(); // Se abre la conexion
                return cnn;
            }
            catch (Exception ex)
            {
                throw new Exception("No se logro realizar la conexion. Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Valida los Paramtros necesarios para la conexoin
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean ValidarConexion()
        {  

            if (this.cadenaConexion.Length == 0)
            {
                 System.Diagnostics.Debug.WriteLine("Falta cadena Cadena conexion");
                return false;
            }

            return true;
        }
    }
}
