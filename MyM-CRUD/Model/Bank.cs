using MyM_CRUD.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using static MyM_CRUD.Model.Product;

namespace MyM_CRUD.Model
{
    public class Bank
    {
        public string Code { get; set; }
        public string Name { get; set; }


        public static List<Bank> GetAllFromDB()
        {
            string query =
                "SELECT * " +
                "FROM bancos";
            PostgreOp op = new PostgreOp(query);

            //Colocar resultados en memoria
            DataAdapter da = op.ObtenerAdapter();
            DataSet result = new DataSet();
            da.Fill(result);

            //Pasar datos a una lista
            IEnumerable<Bank> banks =
                from DataRow dr in result.Tables[0].Rows
                select BuildProductFromDr(dr);

            return banks.ToList();
        }
        private static Bank BuildProductFromDr(dynamic dr) => new Bank()
        {
            Code = dr["cod_banco"].ToString(),
            Name = dr["nombre_b"].ToString(),
        };
    }
}
