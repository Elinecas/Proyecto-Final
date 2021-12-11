using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class MedicosBD
    {

        private SqlConnection _conexion;

        public MedicosBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public bool Agregar(Medicos medico)
        {

            SqlCommand comando = new SqlCommand("insert into Medicos (Cedula, Nombre, Apellido, Correo, Telefono) values(@cedula, @nombre, @apellido, @correo, @telefono)", _conexion);

            comando.Parameters.AddWithValue("@cedula", medico.Cedula);
            comando.Parameters.AddWithValue("@nombre", medico.Nombre);
            comando.Parameters.AddWithValue("@apellido", medico.Apellido);
            comando.Parameters.AddWithValue("@correo", medico.Correo);
            comando.Parameters.AddWithValue("@telefono", medico.Telefono);

            return EjecutarComando(comando);

        }

        public bool Editar(Medicos medico)
        {

            SqlCommand comando = new SqlCommand("update Medicos set Cedula=@cedula, Nombre=@nombre, Apellido=@apellido, Correo=@correo, Telefono=@telefono where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", medico.Id);
            comando.Parameters.AddWithValue("@cedula", medico.Cedula);
            comando.Parameters.AddWithValue("@nombre", medico.Nombre);
            comando.Parameters.AddWithValue("@apellido", medico.Apellido);
            comando.Parameters.AddWithValue("@correo", medico.Correo);
            comando.Parameters.AddWithValue("@telefono", medico.Telefono);

            return EjecutarComando(comando);

        }

        public bool Eliminar(int id)
        {

            SqlCommand comando = new SqlCommand("delete Medicos where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", id);

            return EjecutarComando(comando);

        }

        public bool GuardarFoto(int id, string destino)
        {
            SqlCommand comando = new SqlCommand("update Medicos set Foto=@foto where Id = @id", _conexion);

            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@foto", destino);

            return EjecutarComando(comando);
        }

        public int UltimoId()
        {
            int id = 0;

            _conexion.Open();

            SqlCommand comando = new SqlCommand("select max(Id) as Id from Medicos", _conexion);

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
            }

            lector.Close();
            lector.Dispose();


            _conexion.Close();

            return id;

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
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Cedula, Nombre, Apellido, Correo, Telefono, Foto  from Medicos", _conexion);
            return CargarDatos(consulta);
        }

        public Medicos ObtenerporID(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Cedula, Nombre, Apellido, Correo, Telefono, Foto  from Medicos where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader lector = comando.ExecuteReader();

                Medicos medico = new Medicos();

                while (lector.Read())
                {
                    medico.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    medico.Cedula = lector.IsDBNull(1) ? "" : lector.GetString(1);
                    medico.Nombre = lector.IsDBNull(2) ? "" : lector.GetString(2);
                    medico.Apellido = lector.IsDBNull(3) ? "" : lector.GetString(3);
                    medico.Correo = lector.IsDBNull(4) ? "" : lector.GetString(4);
                    medico.Telefono = lector.IsDBNull(5) ? "" : lector.GetString(5);
                    medico.Foto = lector.IsDBNull(6) ? "" : lector.GetString(6);
                }

                lector.Close();

                lector.Dispose();

                _conexion.Close();

                return medico;

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
