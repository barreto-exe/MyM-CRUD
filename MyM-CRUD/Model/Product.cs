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
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ManufacturerCode { get; set; }
        public ProductType Type { get; set; }
        public bool IsEcologic { get; set; }
        public string? LineCode { get; set; }

        public static List<Product> SearchProducts(string search)
        {
            //Traer datos de la BD
            string query =
                "SELECT p.*, s.es_ecologico, s.cod_linea " +
                "FROM productos p " +
                "LEFT JOIN servicio_productos s ON (p.cod_producto = s.cod_producto) " +
                "WHERE p.nombre_p LIKE @Search " +
                "OR p.cod_producto LIKE @Search";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Product> products =
                from DataRow dr in result.Rows
                select BuildProductFromDr(dr);

            return products.ToList();
        }
        private static Product BuildProductFromDr(dynamic dr)
        {
            Product product = new Product()
            {
                Code = dr["cod_producto"].ToString(),
                Name = dr["nombre_p"].ToString(),
                Price = Tools.Tools.Object2Decimal(dr["precio_p"]),
                Description = dr["descripcion_p"].ToString(),
                ManufacturerCode = dr["cod_fabricante"].ToString(),
                Type = (ProductType) dr["tipo_p"].ToString()[0],
            };

            if(product.Type == ProductType.ForService)
            {
                product.IsEcologic = (bool)dr["es_ecologico"];
                product.LineCode = dr["cod_linea"].ToString();
            }

            return product;
        }


        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO productos(cod_producto, precio_p, nombre_p, descripcion_p, tipo_p, cod_fabricante) " +
                "VALUES (@cod_producto, @precio_p, @nombre_p, @descripcion_p, @tipo_p, @cod_fabricante); ";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_producto", Code);
            op.PasarParametros("precio_p", Price);
            op.PasarParametros("nombre_p", Name);
            op.PasarParametros("descripcion_p", Description);
            op.PasarParametros("tipo_p", (char)Type);
            op.PasarParametros("cod_fabricante", ManufacturerCode);

            if (Type == ProductType.ForSell)
            {
                op.Query += 
                    "INSERT INTO tienda_productos " +
                    "VALUES (@cod_producto); ";
            }
            if(Type == ProductType.ForService)
            {
                op.Query +=
                    "INSERT INTO servicio_productos(cod_producto, es_ecologico, cod_linea) \n" +
                    "VALUES (@cod_producto, @es_ecologico, @cod_linea); ";
                op.PasarParametros("es_ecologico", IsEcologic);
                op.PasarParametros("cod_linea", LineCode);
            }

            op.EjecutarComando();
        }
        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE productos " +
                "SET precio_p = @precio_p, " +
                "nombre_p = @nombre_p, "  +
                "descripcion_p = @descripcion_p, " +
                "tipo_p = @tipo_p, " +
                "cod_fabricante = @cod_fabricante " +
                "WHERE cod_producto = @cod_producto; ";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cod_producto", Code);
            op.PasarParametros("precio_p", Price);
            op.PasarParametros("nombre_p", Name);
            op.PasarParametros("descripcion_p", Description);
            op.PasarParametros("tipo_p", (char)Type);
            op.PasarParametros("cod_fabricante", ManufacturerCode);


            op.Query +=
                "DELETE FROM tienda_productos WHERE cod_producto = @cod_producto;" +
                "DELETE FROM servicio_productos WHERE cod_producto = @cod_producto;";

            if (Type == ProductType.ForSell)
            {
                op.Query +=
                    "INSERT INTO tienda_productos " +
                    "VALUES (@cod_producto); ";
            }
            if (Type == ProductType.ForService)
            {
                op.Query +=
                    "INSERT INTO servicio_productos(cod_producto, es_ecologico, cod_linea) " +
                    "VALUES (@cod_producto, @es_ecologico, @cod_linea); ";
                op.PasarParametros("es_ecologico", IsEcologic);
                op.PasarParametros("cod_linea", LineCode);
            }

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

        public enum ProductType
        {
            ForService = 'S',
            ForSell = 'T',
        }
    }
}
