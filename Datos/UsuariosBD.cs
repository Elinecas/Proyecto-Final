using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class UsuariosBD
    {
        private SqlConnection _conexion;

        public UsuariosBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public bool Agregar(Usuarios usuario)
        {
            
            SqlCommand comando = new SqlCommand("insert into Usuarios (Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol) values(@nombre, @apellido, @correo, @usuario, @contra, @rol)", _conexion);

            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@correo", usuario.Correo);
            comando.Parameters.AddWithValue("@usuario", usuario.Usuario);
            comando.Parameters.AddWithValue("@contra", usuario.Contra);
            comando.Parameters.AddWithValue("@rol", usuario.Rol);

            return EjecutarComando(comando);

        }

        public bool Editar(Usuarios usuario)
        {
            
            SqlCommand comando = new SqlCommand("update Usuarios set Nombre=@nombre, Apellido=@apellido, Correo=@correo, Nombre_Usuario=@usuario, Contra=@contra, Rol=@rol where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", usuario.Id);
            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@correo", usuario.Correo);
            comando.Parameters.AddWithValue("@usuario", usuario.Usuario);
            comando.Parameters.AddWithValue("@contra", usuario.Contra);
            comando.Parameters.AddWithValue("@rol", usuario.Rol);

            return EjecutarComando(comando);

        }

        public bool Eliminar(int id)
        {

            SqlCommand comando = new SqlCommand("delete Usuarios where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", id);

            return EjecutarComando(comando);

        }

        public DataTable CargarDatos(SqlDataAdapter consulta)
        {
            
            try
            {

                DataTable datos = new DataTable();

                _conexion.Open();

                consulta.Fill(datos);

                _conexion.Close();

                return datos;

            }

            catch (Exception e)
            {

                return null;

            }

        }

        public DataTable ObtenerTodo()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol  from Usuarios", _conexion);
            return CargarDatos(consulta);
        }

        public DataTable ValidarLogin(string usuario, string contra)
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select count(*) as Resultado from Usuarios where Nombre_Usuario=@usuario and Contra=@contra", _conexion);

            consulta.SelectCommand.Parameters.AddWithValue("@usuario", usuario);
            consulta.SelectCommand.Parameters.AddWithValue("@contra", contra);

            return CargarDatos(consulta);
        }

        public DataTable ValidarRol(string usuario)
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select Rol from Usuarios where Nombre_Usuario=@usuario", _conexion);

            consulta.SelectCommand.Parameters.AddWithValue("@usuario", usuario);

            return CargarDatos(consulta);
        }

        public Usuarios ObtenerporID(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Nombre, Apellido, Correo, Nombre_Usuario, Contra, Rol  from Usuarios where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader lector = comando.ExecuteReader();

                Usuarios usuario = new Usuarios();

                while (lector.Read())
                {
                    usuario.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    usuario.Nombre = lector.IsDBNull(1) ? "" : lector.GetString(1);
                    usuario.Apellido = lector.IsDBNull(2) ? "" : lector.GetString(2);
                    usuario.Correo = lector.IsDBNull(3) ? "" : lector.GetString(3);
                    usuario.Usuario = lector.IsDBNull(4) ? "" : lector.GetString(4);
                    usuario.Contra = lector.IsDBNull(5) ? "" : lector.GetString(5);
                    usuario.Rol = lector.IsDBNull(6) ? "" : lector.GetString(6);
                }

                lector.Close();

                lector.Dispose();

                _conexion.Close();

                return usuario;

            }

            catch (Exception e)
            {

                return null;

            }
        }

        public bool EjecutarComando(SqlCommand consulta)
        {            
            try
            {

                _conexion.Open();

                consulta.ExecuteNonQuery();

                _conexion.Close();

                return true;

            }

            catch (Exception e)
            {

                return false;

            }
        }
    }
}
