using MaterialDesignColors;
using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyM_CRUD.Model
{
    public class ServiceActivity : CrudObject<ServiceActivity>
    {
        public string ServiceCode { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public static List<ServiceActivity> SearchActivities(string serviceCode)
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM actividades " +
                "WHERE " +
                "cod_servicio = @Search " +
                "ORDER BY num_actividad ASC";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", serviceCode);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<ServiceActivity> services =
                from DataRow dr in result.Rows
                select BuildServiceFromDr(dr);

            return services.ToList();
        }
        private static ServiceActivity BuildServiceFromDr(dynamic dr) => new ServiceActivity()
        {
            ServiceCode = dr["cod_servicio"].ToString(),
            Number = Convert.ToInt32(dr["num_actividad"]),
            Description = dr["descripcion_a"].ToString(),
            Cost = Tools.Tools.Object2Decimal(dr["monto"]),
        };
        public static void ClearTuplesFromDB(string serviceCode)
        {
            string query =
                "DELETE FROM actividades " +
                "WHERE cod_servicio = @cod_servicio";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", serviceCode);
            op.EjecutarComando();
        }


        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT " +
                "INTO actividades (cod_servicio, num_actividad, monto, descripcion_a) " +
                "VALUES (@cod_servicio, @num_actividad, @monto, @descripcion_a)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", ServiceCode);
            op.PasarParametros("num_actividad", Number);
            op.PasarParametros("monto", Cost);
            op.PasarParametros("descripcion_a", Description);
            op.EjecutarComando();
        }
        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE actividades " +
                "SET descripcion_a = @descripcion_a, " +
                "monto = @monto " +
                "WHERE cod_servicio = @cod_servicio " +
                "AND num_actividad = @num_actividad";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", ServiceCode);
            op.PasarParametros("num_actividad", Number);
            op.PasarParametros("monto", Cost);
            op.PasarParametros("descripcion_a", Description);
            op.EjecutarComando();
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
