using MaterialDesignColors;
using MyM_CRUD.DataBase;
using Npgsql;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyM_CRUD.Model
{
    public class Payment : CrudObject<Payment>
    {

        public Invoice AssociatedInvoice { get; set; }
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string BankCode { get; set; }
        public string CardNumber { get; set; }

        //Parámetros para front en facturas
        public string BankName { get; set; }

        public Payment()
        {
            BankCode = "";
            BankName = "";
            CardNumber = "";
        }

        public static List<string> PayMethods()
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM modalidades_pago";
            PostgreOp op = new PostgreOp(query);
            NpgsqlDataReader dr = op.EjecutarReader();

            var m = new List<string>();

            while(dr.Read())
            {
                m.Add(dr.GetString(0));
            }
            dr.Close();

            m.Remove("Cambio");
            return m;
        }
        public static Payment BuildPaymentFromDr(dynamic dr) => new Payment()
        {
            Number = int.Parse(dr["num_pago"]),
            Amount = Tools.Tools.Object2Decimal(dr["monto"]),
            Method = dr["modalidad_p"].ToString(),
            BankCode = dr["cod_banco"].ToString(),
            BankName = dr["nombre_b"].ToString(),
            CardNumber = dr["num_tarjeta"].ToString(),
        };


        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO pagos(num_factura, num_pago, monto, fecha_pago, modalidad_p, cod_banco, num_tarjeta) \n" +
                "VALUES (@num_factura, @num_pago, @monto, @fecha_pago, @modalidad_p, @cod_banco, @num_tarjeta)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_factura", AssociatedInvoice.Number);
            op.PasarParametros("num_pago", Number);
            op.PasarParametros("monto", Amount);
            op.PasarParametros("fecha_pago", AssociatedInvoice.Date);
            op.PasarParametros("modalidad_p", Method);
            if(Method == "Tarjeta")
            {
                op.PasarParametros("cod_banco", BankCode);
                op.PasarParametros("num_tarjeta", CardNumber);
            }
            else
            {
                op.PasarParametros("cod_banco", DBNull.Value);
                op.PasarParametros("num_tarjeta", DBNull.Value);
            }
            op.EjecutarComando();
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
