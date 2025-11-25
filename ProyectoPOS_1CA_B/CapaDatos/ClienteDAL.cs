using ProyectoPOS_1CA_B.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPOS_1CA_B.CapaDatos
{
    public class ClienteDAL
    {
        //TRAER TODOS LOS REGISTROS DE LA TABLA CLIENTE
        public DataTable Listar()
        {
            DataTable dt = new DataTable();//TABLA EN MEMORIA
            using(SqlConnection cn = new SqlConnection(Conexion.Cadena))
            //SqlConnection: crea una conexion a la base de datos
            {
                string sql = "SELECT Id, Nombre, Dui, Telefono, Correo, Estado FROM Cliente";
                //sentencia sql para traer los datos de la tabla cliente
                using(SqlCommand cmd = new SqlCommand(sql, cn))
                //SqlCommand: sentencia SQL que devuelve todos los registros
                {
                    cn.Open();//abrir la conexion
                    new SqlDataAdapter(cmd).Fill(dt);
                    //SqlDataAdapter: ejecuta el comando y llena la tabla en memoria
                }
            }
            return dt;//retorna la tabla con los registros
        }
        //INSERTAR UN NUEVO  REGISTRO DE LA TABLA CLIENTE
        public int Insertar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            //SqlConnection: crea una conexion a la base de datos
            {
                string sql = "INSERT INTO Cliente (Nombre,Dui,Telefono,Correo,Estado) " +
                    "VALUES (@nombre,@dui,@telefono,@correo,@estado);SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@dui", c.Dui);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);
                    cn.Open();//abrir la conexion
                    return Convert.ToInt32(cmd.ExecuteScalar());
                    //ExecuteScalar: ejecuta la sentencia y devuelve el
                    //primer valor de la primera fila
                }
            }
        }

        //ACTUALIZAR UN REGISTRO DE LA TABLA CLIENTE
        public bool Actualizar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            //SqlConnection: crea una conexion a la base de datos
            {
                string sql = "UPDATE Cliente SET Nombre=@nombre, Dui=@dui, " +
                    "Telefono=@telefono, Correo=@correo, Estado=@estado " +
                    "WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", c.Id);
                    cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@dui", c.Dui);
                    cmd.Parameters.AddWithValue("@telefono", c.Telefono);
                    cmd.Parameters.AddWithValue("@correo", c.Correo);
                    cmd.Parameters.AddWithValue("@estado", c.Estado);
                    cn.Open();//abrir la conexion
                    return cmd.ExecuteNonQuery() > 0;
                    //ExecuteNonQuery: ejecuta la sentencia y devuelve
                    //el numero de filas afectadas
                }
            }
        }
        //ELIMINAR UN REGISTRO DE LA TABLA CLIENTE
        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = "DELETE FROM Cliente WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();//abrir la conexion
                    return cmd.ExecuteNonQuery() > 0;
                    //ExecuteNonQuery: ejecuta la sentencia y devuelve
                    //el numero de filas afectadas
                }
            }
        }
        //FILTRAR REGISTROS POR NOMBRE Y TELEFONO
        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();//TABLA EN MEMORIA
            using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
            {
                string sql = @"SELECT Id, Nombre, Dui, Telefono, Correo, Estado 
                               FROM Cliente
                               WHERE Nombre LIKE @filtro OR Telefono LIKE @filtro";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    cn.Open();//abrir la conexion
                    new SqlDataAdapter(cmd).Fill(dt);
                    //SqlDataAdapter: ejecuta el comando y llena la tabla en memoria
                }
            } return dt;//retorna la tabla con los registros  
        }
    }
}
