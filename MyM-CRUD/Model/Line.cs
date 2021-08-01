using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyM_CRUD.Model
{
    public class Line : CrudObject<Line>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<Line> GetAllFromDB()
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM lineas_suministro ";
            PostgreOp op = new PostgreOp(query);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Line> manufacturers =
                from DataRow dr in result.Rows
                select new Line()
                {
                    Code = dr["cod_linea"].ToString(),
                    Name = dr["nombre_l"].ToString(),
                };

            return manufacturers.ToList();
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
