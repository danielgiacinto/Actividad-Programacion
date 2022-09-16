using AppCarreras__Dao_.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCarreras__Dao_.Capa_de_Acceso_a_Datos
{
    internal class HelperDAO
    {
        private static HelperDAO instance;
        private string cadenaConexion;

        private HelperDAO()
        {
            cadenaConexion = Properties.Resources.cnnString;
        }

        public static HelperDAO ObtenerInstancia()
        {
            if (instance == null)
            {
                instance = new HelperDAO();
            }
            return instance;
        }

        public DataTable ConsultaSql(string SP_NAME)
        {
            DataTable table = new DataTable();
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            cnn.ConnectionString = cadenaConexion;
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = SP_NAME;
            table.Load(cmd.ExecuteReader());
            cnn.Close();

            return table;

        }
        public int ProximoIdCarrera()
        {
            int nro = 0;

            try
            {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = Properties.Resources.cnnString;
                cnn.Open();
                SqlCommand cmd = new SqlCommand("sp_proxima_carrera", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@next", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                nro = param.Value.GetType() == typeof(int) ? (int)param.Value : 1;
                cnn.Close();
            }
            catch (Exception)
            {
                nro = -1;
            }
            return nro;
        }
        public bool Create(Carrera carrera)
        {
            bool ok = true;
            SqlConnection cnn = new SqlConnection(Properties.Resources.cnnString);
            SqlTransaction trs = null;

            try
            {
                cnn.Open();
                trs = cnn.BeginTransaction();

                SqlCommand cmd = new SqlCommand("sp_insertar_carrera", cnn, trs);
                cmd.Transaction = trs;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", carrera.NombreTitulo);

                SqlParameter param = new SqlParameter("@new_id_carrera", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int idCarrera = Convert.ToInt32(param.Value);


                foreach (DetalleCarrera dc in carrera.LisDetalleCarrera)
                {
                    SqlCommand cmdDetalle = new SqlCommand();
                    cmdDetalle.Transaction = trs;
                    cmdDetalle.Connection = cnn;
                    cmdDetalle.CommandText = "sp_insertar_detalleCarreras";
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.AddWithValue("@anioCursado", dc.AnioCursado);
                    cmdDetalle.Parameters.AddWithValue("@cuatrimestre", dc.Cuatrimestre);
                    cmdDetalle.Parameters.AddWithValue("@id_carrera", idCarrera);
                    cmdDetalle.Parameters.AddWithValue("@id_asignatura", dc.Asignatura.IdAsignatura);
                    cmdDetalle.ExecuteNonQuery();

                    cmdDetalle.Parameters.Clear();

                }

                trs.Commit();
            }
            catch (Exception)
            {
                trs.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
        public DataTable consultarDetalleCarrera(string nombre)
        {
            DataTable table = new DataTable();
            SqlConnection cnn = new SqlConnection(Properties.Resources.cnnString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_consultar_detalleCarreras";
            cmd.Parameters.AddWithValue("@nombre", nombre);

            table.Load(cmd.ExecuteReader());
            cnn.Close();
            return table;
        }

        public bool bajaCarrera(int idCarrera)
        {
            bool ok = true;
            SqlConnection cnn = new SqlConnection(Properties.Resources.cnnString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction t = null;

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_eliminar_carrera";
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@id_carrera", idCarrera);
                ok = cmd.ExecuteNonQuery() == 1;

                t.Commit();
            }
            catch
            {
                t.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }

        public bool insertAsignatura(string nombre)
        {
            SqlConnection cnn = new SqlConnection(Properties.Resources.cnnString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction t = null;
            bool ok = true;

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_insertar_asignatura";
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                ok = cmd.ExecuteNonQuery() == 1;

                t.Commit();
            }
            catch
            {
                t.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return ok;
        }
    }
}
