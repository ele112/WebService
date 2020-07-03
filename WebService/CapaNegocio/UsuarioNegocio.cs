using CapaConexion;
using System.Collections.Generic;
using CapaDTO;
using System.Data;
using System.Linq;
using System;
using System.Web.Http;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        private string table;

        public string Table { get => table; set => table = value; }

        public UsuarioNegocio() { Table = "usuario"; }

        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                string Query = $"SELECT * FROM {Table}";

                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Usuario> usuarios = ConvertToList<Usuario>(ResultDT);

                return usuarios;

            }
            catch(Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> "+ex.Message);
            }

        }


        public string CrearUsuario(Usuario usuario)
        {
            try
            {
                bool existe = ExisteUsuario(usuario.Rut);
                if (existe) { throw new Exception("Usuario ya existe!"); }

                string hoy = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string Query = $"INSERT INTO {Table} " +
                    "(rut ,dv, nombre, apellidos, direccion, email, celular," +
                    "password, created_at, deleted_at)" +
                    "VALUES (" +
                    $" '{usuario.Rut}', " +
                    $" '{usuario.Dv}', " +
                    $" '{usuario.Nombre}', " +
                    $" '{usuario.Apellidos}', " +
                    $" '{usuario.Direccion}', " +
                    $" '{usuario.Email}'," +
                    $" '{usuario.Celular}'," +
                    $" '{usuario.Password}'," +
                    $" '{hoy}'," +
                    " null)";

                bool exito = new Conexion().EjecutarQuery(Query);

                if (!exito)
                {
                    throw new Exception("No se logro crear el usuario, por favor reintente.");
                }

                return "Usuario Creado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string ActualizarUsuario(Usuario usuario)
        {
            try
            {
                bool existe = ExisteUsuario(usuario.Rut);
                if (!existe) { throw new Exception("Usuario No existe!"); }

                string Query =
                    $"UPDATE {Table} SET " +
                    $"nombre = '{usuario.Nombre}', " +
                    $"apellidos = '{usuario.Apellidos}', " +
                    $"direccion = '{usuario.Direccion}', " +
                    $"Email = '{usuario.Email}', " +
                    $"celular = '{usuario.Celular}' " +
                    $"WHERE rut = '{usuario.Rut}' ";

                bool exito = new Conexion().EjecutarQuery(Query);

                if (!exito){
                    throw new Exception("No se logro actualizar el usuario, por favor reintente.");
                }

                return "Usuario Actualizado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }

        }

        public string EliminarUsuario(string rut)
        {
            try
            {
                if (string.IsNullOrEmpty(rut)) {
                    throw new Exception("Debe ingresar el rut de la persona eliminar!");
                }
                bool existe = ExisteUsuario(rut);
                if (!existe) {
                    throw new Exception("Usuario No existe!");
                }

                string Query = $"DELETE FROM {Table} WHERE rut = '{rut}' ";
                bool exito = new Conexion().EjecutarQuery(Query);
                if (!exito){
                    throw new Exception("No se logro eliminar el usuario, por favor reintente.");
                }

                return "Usuario Actualizado";
            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }




        }

        public List<Usuario> ObtenerUsuario(string rut)
        {
            try
            {

                if (string.IsNullOrEmpty(rut)) {
                    throw new Exception("Debe ingresar el rut de la persona eliminar!");
                }

                string Query = $"SELECT * FROM {Table} WHERE rut = '{rut}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                DataTable ResultDT = dt.Tables[0];
                List<Usuario> usuario = ConvertToList<Usuario>(ResultDT);

                return usuario;

            }
            catch (Exception ex)
            {
                throw new Exception("Algo salio mal. Exception -> " + ex.Message);
            }



        }


        // Valida si esite un usuario
        private bool ExisteUsuario(string rut)
        {
                string Query = $"SELECT * FROM {Table} WHERE rut = '{rut}'; ";
                DataSet dt = new Conexion().EjecutarSelect(Query);
                int count = dt.Tables[0].Rows.Count;

                if(count > 0) { return true; }
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
