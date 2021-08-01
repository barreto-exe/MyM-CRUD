using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyM_CRUD.Model
{
    public class VehicleModel : CrudObject<VehicleModel>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

        public static List<VehicleModel> GetAllFromDB()
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM modelos mo " +
                "INNER JOIN marcas ma ON (ma.cod_marca = mo.cod_marca) " +
                "INNER JOIN tipos_vehiculo t ON (t.cod_tipo_vehiculo = mo.cod_tipo_vehiculo)";
            PostgreOp op = new PostgreOp(query);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<VehicleModel> models =
                from DataRow dr in result.Rows
                select new VehicleModel()
                {
                    Name = dr["nom_modelo"].ToString(),
                    Brand = dr["nom_marca"].ToString(),
                    Type = dr["descripcion_v"].ToString(),
                };

            return models.ToList();
        }

        public override void InsertTupleDatabase()
        {
            throw new NotImplementedException();
        }

        public override void UpdateTupleDataBase()
        {
            throw new NotImplementedException();
        }

        protected override void BuildCrudObject(NpgsqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        protected override PostgreOp GetObjectOp(object[] keys)
        {
            throw new NotImplementedException();
        }
    }
}
