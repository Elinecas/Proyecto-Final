    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class Resultados_LaboratorioBD
    {

        private SqlConnection _conexion;

        public Resultados_LaboratorioBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public DataTable ObtenerTodoJoin()
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select rl.Id, p.Nombre, p.Apellido, p.Cedula, pl.Nombre as 'Prueba' from Resultados_Laboratorio rl inner join Pacientes p on rl.Id_Paciente = p.Id inner join Pruebas_Laboratorio pl on rl.Id_Paciente = pl.Id where Estado_Resultado='Pendiente'", _conexion);
            return CargarDatos(consulta);
        }

        public DataTable FiltrarResultados(string cedula)
        {
            SqlDataAdapter consulta = new SqlDataAdapter("select rl.Id, p.Nombre, p.Apellido, p.Cedula, pl.Nombre as 'Prueba' from Resultados_Laboratorio rl inner join Pacientes p on rl.Id_Paciente = p.Id inner join Pruebas_Laboratorio pl on rl.Id_Paciente = pl.Id where Estado_Resultado='Pendiente' and p.Cedula like '%@cedula%' order by p.Cedula Desc", _conexion);
            consulta.SelectCommand.Parameters.AddWithValue("@cedula", cedula);
            return CargarDatos(consulta);
        }

        public bool Agregar(Resultados_Laboratorio resultado)
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

        public DataTable ValidarCedula(Pacientes paciente)
        {
            SqlDataAdapter comando = new SqlDataAdapter("Select Cedula From Resultados_Laboratorio WHERE Cedula=@cedula", _conexion);
            comando.SelectCommand.Parameters.AddWithValue("@cedula", paciente.Cedula);
            return CargarDatos(comando);
        }
        public bool Editar(Resultados_Laboratorio resultado)
        {

            SqlCommand comando = new SqlCommand("update Resultados_Laboratorio set Resultado_Prueba=@resultado, Estado_Resultado=@estado where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", resultado.Id_Paciente);
            comando.Parameters.AddWithValue("@estado", resultado.Estado_Resultado);
            comando.Parameters.AddWithValue("@resultado", resultado.Resultado_Prueba);

            return EjecutarComando(comando);

        }
        public Pacientes ObtenerporID1(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id from Resultados_Laboratorio where Id=@id", _conexion);

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

    }
}
