using Datos;
using Datos.Modelos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Logica
{
    public class MedicosCrud
    {

        private MedicosBD _medicos;

        public MedicosCrud(SqlConnection conexion)
        {
            _medicos = new MedicosBD(conexion);
        }
        public bool Agregar(Medicos medico)
        {
            return _medicos.Agregar(medico);
        }

        public bool Editar(Medicos medico)
        {
            return _medicos.Editar(medico);
        }

        public bool Eliminar(int id)
        {
            return _medicos.Eliminar(id);
        }

        public bool GuardarFoto(int id, string destino)
        {
            return _medicos.GuardarFoto(id, destino);
        }

        public int UltimoId()
        {
            return _medicos.UltimoId();
        }

        public Medicos ObtenerporID(int id)
        {
            return _medicos.ObtenerporID(id);
        }

        public DataTable ObtenerTodo()
        {
            return _medicos.ObtenerTodo();
        }

    }
}
