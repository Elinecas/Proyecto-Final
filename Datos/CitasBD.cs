using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class CitasBD
    {
        private SqlConnection _conexion;

        public CitasBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }


        public DataTable ValidarCedula(Pacientes paciente)
        {
            SqlDataAdapter comando = new SqlDataAdapter("Select Cedula From Pacientes WHERE Cedula=@cedula", _conexion);
            comando.SelectCommand.Parameters.AddWithValue("@cedula", paciente.Cedula);
            return CargarDatos(comando);
        }

        public DataTable ValidarCedulaMedico(Medicos medico)
        {
            SqlDataAdapter comando = new SqlDataAdapter("Select Cedula From Medicos WHERE Cedula=@cedula", _conexion);
            comando.SelectCommand.Parameters.AddWithValue("@cedula", medico.Cedula);
            return CargarDatos(comando);
        }


        public DataTable ObtenerTodoJoin()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("SELECT Citas.Id, Pacientes.Nombre as 'Paciente', Medicos.Nombre as 'Doctor', Citas.Fecha_Cita as 'Fecha', Citas.Causa_Cita as 'Causa', Citas.Estado_Cita as 'Estado' FROM ((Citas INNER JOIN Pacientes ON Citas.Id_Paciente = Pacientes.Id) INNER JOIN Medicos ON Citas.Id_Medico = Medicos.Id)", _conexion);
            return CargarDatos(consulta);
        }

        public DataTable ObtenerTodoPacientes()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("SELECT * from Pacientes", _conexion);
            return CargarDatos(consulta);
        }

        public DataTable FiltrarPacientes(string cedula)
        {
            SqlDataAdapter consulta = new SqlDataAdapter("Select * from Pacientes WHERE Cedula =@cedula order by Cedula Desc", _conexion);
            consulta.SelectCommand.Parameters.AddWithValue("@cedula", cedula);
            return CargarDatos(consulta);
        }

        public DataTable ObtenerTodoMedicos()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Cedula, Nombre, Apellido, Correo, Telefono, Foto  from Medicos", _conexion);
            return CargarDatos(consulta);
        }

        public DataTable FiltrarMedicos(string cedula)
        {
            SqlDataAdapter consulta = new SqlDataAdapter("Select * from Medicos WHERE Cedula =@cedula order by Cedula Desc", _conexion);
            consulta.SelectCommand.Parameters.AddWithValue("@cedula", cedula);
            return CargarDatos(consulta);
        }

        public bool AgregarCitas(Citas cita)
        {

            SqlCommand comando = new SqlCommand("INSERT INTO Citas (Id_Paciente, Id_Medico, Fecha_Cita, Causa_Cita, Estado_Cita) values(@paciente, @medico, @fecha, @causa,  @estado)", _conexion);

            comando.Parameters.AddWithValue("@paciente", cita.Id_Paciente);
            comando.Parameters.AddWithValue("@medico", cita.Id_Medico);
            comando.Parameters.AddWithValue("@fecha", cita.Fecha_Cita);
            comando.Parameters.AddWithValue("@causa", cita.Causa_Cita);
            comando.Parameters.AddWithValue("@estado", cita.Estado_Cita);


            return EjecutarComando(comando);

        }

        public DataTable ObtenerTodoPruebas()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Nombre from Pruebas_Laboratorio", _conexion);
            return CargarDatos(consulta);
        }

        public bool AgregarResultados(Resultados_Laboratorio resultado)
        {

            SqlCommand comando = new SqlCommand("insert into Resultados_Laboratorio (Id_Paciente, Id_Cita, Id_PruebaLaboratorio, Id_Medico, Resultado_Prueba, Estado_Resultado) values(@paciente, @cita, @prueba, @medico, @resultado, @estado)", _conexion);

            comando.Parameters.AddWithValue("@paciente", resultado.Id_Paciente);
            comando.Parameters.AddWithValue("@fecha", resultado.Id_Cita);
            comando.Parameters.AddWithValue("@causa", resultado.Id_PruebaLaboratorio);
            comando.Parameters.AddWithValue("@medico", resultado.Id_Medico);
            comando.Parameters.AddWithValue("@estado", resultado.Resultado_Prueba);
            comando.Parameters.AddWithValue("@estado", resultado.Estado_Resultado);

            return EjecutarComando(comando);

        }

        public DataTable ObtenerResultados()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Resultado_Prueba, Estado_Resultado from Resultados_Laboratorio", _conexion);
            return CargarDatos(consulta);
        }

        public Pacientes ObtenerporID1(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Nombre from Pacientes where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader lector = comando.ExecuteReader();

                Pacientes paciente = new Pacientes();

                while (lector.Read())
                {
                    paciente.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    paciente.Nombre = lector.IsDBNull(1) ? "" : lector.GetString(1);
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
        public Medicos ObtenerporID2(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Nombre from Medicos where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);
                SqlDataReader lector = comando.ExecuteReader();
                Medicos medicos = new Medicos();

                while (lector.Read())
                {
                    medicos.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    medicos.Nombre = lector.IsDBNull(1) ? "" : lector.GetString(1);
                }

                lector.Close();

                lector.Dispose();

                _conexion.Close();

                return medicos;

            }

            catch (Exception e)
            {

                return null;

            }
        }


        public bool ActualizarEstado(int id)
        {

            SqlCommand comando = new SqlCommand("update Resultados_Laboratorio set Estado_Resultado='Completada' where Id=@id", _conexion);

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
        public bool Eliminar(int id)
        {

            SqlCommand comando = new SqlCommand("delete Citas where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", id);

            return EjecutarComando(comando);

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
