using MaterialDesignColors;
using MyM_CRUD.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Npgsql;

namespace MyM_CRUD.Model
{
    public class Manufacturer : CrudObject<Manufacturer>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<Manufacturer> GetAllFromDB()
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM fabricantes  ";
            PostgreOp op = new PostgreOp(query);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Manufacturer> manufacturers =
                from DataRow dr in result.Rows
                select new Manufacturer()
                {
                    Code = dr["cod_fabricante"].ToString(),
                    Name = dr["nombre_f"].ToString(),
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
