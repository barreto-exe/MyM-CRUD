using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Invoice : CrudObject<Invoice>
    {
        public Registration AssociatedRegistration { get; set; }
        public string Number
        {
            get => $"FACT-{AssociatedRegistration.Number}";
        }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public string ClientId { get; set; }
        public decimal TotalAmount
        {
            get
            {
                var total = 0m;
                foreach (var detail in Details)
                {
                    total += detail.Subtotal;
                }
                return total - total * Discount / 100;
            }
        }
        public decimal TotalPayed
        {
            get
            {
                var total = 0m;
                foreach (var pay in Payments)
                {
                    total += pay.Amount;
                }
                return total;
            }
        }
        public decimal PendingChange
        {
            get
            {
                var pending = TotalPayed - TotalAmount;

                if (pending > 0) 
                    return pending;
                else
                    return 0;

            }
        }

        public List<ServiceOrder> Details { get; set; }
        public List<Payment> Payments { get; set; }

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO facturas(num_factura, fecha_emision, descuento_f, tipo_fact, ced_cliente) " +
                "VALUES (@num_factura, @fecha_emision, @descuento_f, @tipo_fact, @ced_cliente); " +

                "INSERT INTO facturas_servicio(num_factura, num_ficha) \n" +
                "VALUES (@num_factura, @num_ficha);";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_factura", Number);
            op.PasarParametros("fecha_emision", Date);
            op.PasarParametros("descuento_f", Discount);
            op.PasarParametros("tipo_fact", 'S');
            op.PasarParametros("ced_cliente", ClientId);
            op.PasarParametros("num_ficha", AssociatedRegistration.Number);
            op.EjecutarComando();

            foreach(var payment in Payments)
            {
                payment.AssociatedInvoice = this;
                payment.InsertTupleDatabase();
            }
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
