using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Datos.Modelos;

namespace Datos
{
    public class Pruebas_LaboratorioBD
    {

        private SqlConnection _conexion;

        public Pruebas_LaboratorioBD(SqlConnection conexion)
        {
            _conexion = conexion;
        }

        public bool Agregar(Pruebas_Laboratorio prueba)
        {

            SqlCommand comando = new SqlCommand("insert into Pruebas_Laboratorio (Nombre) values(@nombre)", _conexion);

            comando.Parameters.AddWithValue("@nombre", prueba.Nombre);

            return EjecutarComando(comando);

        }

        public bool Editar(Pruebas_Laboratorio prueba)
        {

            SqlCommand comando = new SqlCommand("update Pruebas_Laboratorio set Nombre=@nombre where Id=@id", _conexion);

            comando.Parameters.AddWithValue("@id", prueba.Id);
            comando.Parameters.AddWithValue("@nombre", prueba.Nombre);

            return EjecutarComando(comando);

        }

        public bool Eliminar(int id)
        {

            SqlCommand comando = new SqlCommand("delete Pruebas_Laboratorio where Id=@id", _conexion);

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
            SqlDataAdapter consulta = new SqlDataAdapter("select Id, Nombre from Pruebas_Laboratorio", _conexion);
            return CargarDatos(consulta);
        }

        public Pruebas_Laboratorio ObtenerporID(int id)
        {
            try
            {
                _conexion.Open();

                SqlCommand comando = new SqlCommand("select Id, Nombre from Pruebas_Laboratorio where Id=@id", _conexion);

                comando.Parameters.AddWithValue("@id", id);

                SqlDataReader lector = comando.ExecuteReader();

                Pruebas_Laboratorio prueba = new Pruebas_Laboratorio();

                while (lector.Read())
                {
                    prueba.Id = lector.IsDBNull(0) ? 0 : lector.GetInt32(0);
                    prueba.Nombre = lector.IsDBNull(1) ? "" : lector.GetString(1);
                }

                lector.Close();

                lector.Dispose();

                _conexion.Close();

                return prueba;

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
