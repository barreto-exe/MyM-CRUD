using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Product : CrudObject<Product>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ManufacturerCode { get; set; }

        public static List<Product> SearchProducts(string search)
        {
            //Traer datos de la BD
            string query =
                "";

            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");
            op.PasarParametros("Rif", App.Session.Branch);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Product> products =
                from DataRow dr in result.Rows
                select new Product()
                {
                };

            return products.ToList();
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
