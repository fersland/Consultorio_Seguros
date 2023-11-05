using Consultorio_Seguros.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Consultorio_Seguros.DAL
{
    public class Asegurado_DAL
    {
        SqlConnection db = null;
        SqlCommand command = null;

        public static IConfiguration Config {  get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");

            Config = builder.Build();
            return Config.GetConnectionString("db");
        }
        public List<AseguradoVM> GetAll()
        {
            List<AseguradoVM> aseguradoListar = new List<AseguradoVM>();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_LISTAR]";
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    var model = new AseguradoVM();

                    model.Id = Convert.ToInt32(dr["Id"]);
                    model.CedulaCliente = dr["CedulaCliente"].ToString();
                    model.NombreCliente = dr["NombreCliente"].ToString();
                    model.CodigoSeguro = dr["CodigoSeguro"].ToString();
                    model.NombreSeguro = dr["NombreSeguro"].ToString();
                    model.Asegurada = dr["Asegurada"].ToString();
                    model.Prima = Convert.ToDecimal(dr["Prima"].ToString());
                    aseguradoListar.Add(model);
                }
                db.Close();
            }

            return aseguradoListar;
        }

        public Asegurado GetById(int id)
        {
            Asegurado asegurado = new Asegurado();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_ID]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    asegurado.Id = Convert.ToInt32(dr["Id"]);
                    asegurado.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                    asegurado.SeguroId = Convert.ToInt32(dr["SeguroId"]);
                }
                db.Close();
            }
            return asegurado;
        }

        public AseguradoVM GetByCedula(string param)
        {
            AseguradoVM view = new AseguradoVM();
            using (db = new SqlConnection(GetConnectionString())){
                command = db.CreateCommand();
                command.CommandText = "[Dbo].[SP_ASEGURADO_BUSCAR_CEDULA]";
                command.Parameters.AddWithValue("@Cedula", param);
                db.Open();

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    view.Id = Convert.ToInt32(dr["Id"]);
                    view.CedulaCliente = dr["CedulaCliente"].ToString();
                    view.NombreCliente = dr["NombreCliente"].ToString();
                    view.CodigoSeguro = dr["CodigoSeguro"].ToString();
                    view.NombreSeguro = dr["NombreSeguro"].ToString();
                    view.Asegurada = dr["Asegurada"].ToString();
                    view.Prima = Convert.ToDecimal(dr["Prima"].ToString());
                }
                db.Close();
            }
            return view;
        }

        public bool Insert(Asegurado model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_CREAR]";
                command.Parameters.AddWithValue("@ClienteId", model.ClienteId);
                command.Parameters.AddWithValue("@SeguroId", model.SeguroId);
                db.Open();
                id = command.ExecuteNonQuery();
                db.Close();
            }

            return id > 0 ? true : false;
        }

        public bool Update(Asegurado model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_EDITAR]";
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@ClienteId", model.ClienteId);
                command.Parameters.AddWithValue("@SeguroId", model.SeguroId);
                db.Open();
                id = command.ExecuteNonQuery();
                db.Close();
            }

            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int deleteRouwCount = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_ELIMINAR]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();
                deleteRouwCount = command.ExecuteNonQuery();
                db.Close();
            }

            return deleteRouwCount > 0 ? true : false;
        }
    }
}
