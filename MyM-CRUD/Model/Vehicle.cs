using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Vehicle : CrudObject<Vehicle>
    {
        public string Id { get; set; }
        public string OilType { get; set; }
        public DateTime DateAcquired { get; set; }
        public string MechanicName { get; set; }
        public string MechanicPhone { get; set; }
        public string ModelName { get; set; }
        public string OwnerId { get; set; }

        public static List<Vehicle> SearchVehicles(string search)
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM vehicles  " +
                "WHERE " +
                "placa LIKE @Search ";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Vehicle> v =
                from DataRow dr in result.Rows
                select new Vehicle()
                {
                    Id = dr["placa"].ToString(),
                    OilType = dr["tipo_aceite"].ToString(),
                    DateAcquired = (DateTime)dr["fecha_adq"],
                    MechanicName = dr["nom_mecanico"].ToString(),
                    MechanicPhone = dr["tlf_mecanico"].ToString(),
                    OwnerId = dr["ced_dueno"].ToString(),
                    ModelName = dr["nom_modelo"].ToString(),
                };

            return v.ToList();
        }

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT " +
                "INTO vehiculos (placa, tipo_aceite, fecha_adq, nom_mecanico, tlf_mecanico, ced_dueno, nom_modelo) " +
                "VALUES (@placa, @tipo_aceite, @fecha_adq, @nom_mecanico, @tlf_mecanico, @ced_dueno, @nom_modelo)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("placa", Id);
            op.PasarParametros("tipo_aceite", OilType);
            op.PasarParametros("fecha_adq", DateAcquired);
            op.PasarParametros("nom_mecanico", MechanicName);
            op.PasarParametros("tlf_mecanico", MechanicPhone);
            op.PasarParametros("ced_dueno", OwnerId);
            op.PasarParametros("nom_modelo", ModelName);
            op.EjecutarComando();
        }

        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE vehiculos " +
                "SET tipo_aceite = @tipo_aceite," +
                "fecha_adq = @fecha_adq," +
                "nom_mecanico = @nom_mecanico," +
                "tlf_mecanico = @tlf_mecanico," +
                "ced_dueno = @ced_dueno, " +
                "nom_modelo =  @nom_modelo" +
                "WHERE placa = @placa";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("placa", Id);
            op.PasarParametros("tipo_aceite", OilType);
            op.PasarParametros("fecha_adq", DateAcquired);
            op.PasarParametros("nom_mecanico", MechanicName);
            op.PasarParametros("tlf_mecanico", MechanicPhone);
            op.PasarParametros("ced_dueno", OwnerId);
            op.PasarParametros("nom_modelo", ModelName);
            op.EjecutarComando();
        }

        protected override void BuildCrudObject(NpgsqlDataReader dr)
        {
            Id = dr["placa"].ToString();
            OilType = dr["tipo_aceite"].ToString();
            DateAcquired = (DateTime)dr["fecha_adq"];
            MechanicName = dr["nom_mecanico"].ToString();
            MechanicPhone = dr["tlf_mecanico"].ToString();
            OwnerId = dr["ced_dueno"].ToString();
            ModelName = dr["nom_modelo"].ToString();
        }

        protected override PostgreOp GetObjectOp(object[] keys)
        {
            throw new NotImplementedException();
        }
    }
}
