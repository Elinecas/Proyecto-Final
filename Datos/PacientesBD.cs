using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class PacientesBD
    {

        private SqlConnection _conexion;

        public PacientesBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public bool Agregar(Pacientes paciente)
        {

            SqlCommand comando = new SqlCommand("insert into Pacientes (Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias) values(@cedula, @nombre, @apellido, @telefono, @direccion, @nacimiento, @fumador, @alergias)", _conexion);

            comando.Parameters.AddWithValue("@cedula", paciente.Cedula);
            comando.Parameters.AddWithValue("@nombre", paciente.Nombre);
            comando.Parameters.AddWithValue("@apellido", paciente.Apellido);
            comando.Parameters.AddWithValue("@telefono", paciente.Telefono);
            comando.Parameters.AddWithValue("@direccion", paciente.Direccion);
            comando.Parameters.AddWithValue("@nacimiento", paciente.Fecha_Nacimiento);
            comando.Parameters.AddWithValue("@fumador", paciente.Fumador);
            comando.Parameters.AddWithValue("@alergias", paciente.Alergias);

            return EjecutarComando(comando);

        }

        public bool Editar(Pacientes paciente)
        {

            SqlCommand comando = new SqlCommand("update Pacientes set Cedula=@cedula, Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Direccion=@direccion, Fecha_Nacimiento=@nacimiento, Fumador=@fumador, Alergias=@alergias where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", paciente.Id);
            comando.Parameters.AddWithValue("@cedula", paciente.Cedula);
            comando.Parameters.AddWithValue("@nombre", paciente.Nombre);
            comando.Parameters.AddWithValue("@apellido", paciente.Apellido);
            comando.Parameters.AddWithValue("@telefono", paciente.Telefono);
            comando.Parameters.AddWithValue("@direccion", paciente.Direccion);
            comando.Parameters.AddWithValue("@nacimiento", paciente.Fecha_Nacimiento);
            comando.Parameters.AddWithValue("@fumador", paciente.Fumador);
            comando.Parameters.AddWithValue("@alergias", paciente.Alergias);

            return EjecutarComando(comando);

        }

        public bool Eliminar(int id)
        {

            SqlCommand comando = new SqlCommand("delete Pacientes where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", id);

            return EjecutarComando(comando);

        }

        public bool GuardarFoto(int id, string destino)
        {
            SqlCommand comando = new SqlCommand("update Pacientes set Foto=@foto where Id = @id", _conexion);

            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@foto", destino);

            return EjecutarComando(comando);
        }

        public int UltimoId()
        {
            int id = 0;

            _conexion.Open();

            SqlCommand comando = new SqlCommand("select max(Id) as Id from Pacientes", _conexion);

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
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias, Foto from Pacientes", _conexion);
            return CargarDatos(consulta);
        }

        public Pacientes ObtenerporID(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Cedula, Nombre, Apellido, Telefono, Direccion, Fecha_Nacimiento, Fumador, Alergias, Foto from Pacientes where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader lector = comando.ExecuteReader();

                Pacientes paciente = new Pacientes();

                while (lector.Read())
                {
                    paciente.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    paciente.Cedula = lector.IsDBNull(1) ? "" : lector.GetString(1);
                    paciente.Nombre = lector.IsDBNull(2) ? "" : lector.GetString(2);
                    paciente.Apellido = lector.IsDBNull(3) ? "" : lector.GetString(3);
                    paciente.Telefono = lector.IsDBNull(4) ? "" : lector.GetString(4);
                    paciente.Direccion = lector.IsDBNull(5) ? "" : lector.GetString(5);
                    paciente.Fecha_Nacimiento = lector.IsDBNull(6) ? DateTime.MinValue : lector.GetDateTime(6);
                    paciente.Fumador = lector.IsDBNull(7) ? "" : lector.GetString(7);
                    paciente.Alergias = lector.IsDBNull(8) ? "" : lector.GetString(8);
                    paciente.Foto = lector.IsDBNull(9) ? "" : lector.GetString(9);
                }

                lector.Close();

                lector.Dispose();

                _conexion.Close();

                return paciente;

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
