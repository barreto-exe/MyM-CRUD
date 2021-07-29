using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Service : CrudObject<Service>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReserveTime { get; set; }
        public bool UnderReserve
        {
            get => underReserve;
            set 
            { 
                underReserve = value; 
                if(!underReserve)
                {
                    ReserveTime = 0;
                }
            }
        }
        private bool underReserve;

        public static Service GetService(string code)
        {
            string query =
                "SELECT * " +
                "FROM servicios " +
                "WHERE cod_servicio = @cod_servicio";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", code);
            NpgsqlDataReader dr = op.EjecutarReader();
            dr.Read();
            Service s = BuildServiceFromDr(dr);
            dr.Close();
            return s;
        }
        public static List<Service> SearchServices(string search)
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM servicios " +
                "WHERE " +
                "nombre_s LIKE @Search";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Service> services =
                from DataRow dr in result.Rows
                select BuildServiceFromDr(dr);

            return services.ToList();
        }
        private static Service BuildServiceFromDr(dynamic dr) => new Service()
        {
            Code = dr["cod_servicio"].ToString(),
            Name = dr["nombre_s"].ToString(),
            Description = dr["descripcion_s"].ToString(),
            ReserveTime = (int)dr["tiempo_r"],
            UnderReserve = (bool)dr["tiene_reserva"],
        };

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT " +
                "INTO servicios (cod_servicio, nombre_s, descripcion_s, tiene_reserva, tiempo_r) " +
                "VALUES (@cod_servicio, @nombre_s, @descripcion_s, @tiene_reserva, @tiempo_r)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", Code);
            op.PasarParametros("nombre_s", Name);
            op.PasarParametros("descripcion_s", Description);
            op.PasarParametros("tiene_reserva", UnderReserve);
            op.PasarParametros("tiempo_r", ReserveTime);
            op.EjecutarComando();
        }
        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE servicios " +
                "SET nombre_s = @nombre_s, " +
                "descripcion_s = @descripcion_s, " +
                "tiene_reserva = @tiene_reserva, " +
                "tiempo_r = @tiempo_r " +
                "WHERE cod_servicio = @cod_servicio";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_servicio", Code);
            op.PasarParametros("nombre_s", Name);
            op.PasarParametros("descripcion_s", Description);
            op.PasarParametros("tiene_reserva", UnderReserve);
            op.PasarParametros("tiempo_r", ReserveTime);
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
