using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        public string ClientName { get; set; }
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

        public void BuildByAssociatedRegistration()
        {
            BuildingInvoice();
            BuildingDetails();
            BuildingPayments();

            void BuildingInvoice()
            {
                string query =
                    "SELECT * " +
                    "FROM facturas f " +
                    "INNER JOIN clientes c ON (f.ced_cliente = c.cedula_c) " +
                    "WHERE num_factura = @num_factura";
                PostgreOp op = new PostgreOp(query);
                op.PasarParametros("num_factura", Number);

                //Colocar resultados en memoria
                DataTable result = QueryFromDataBase(op);

                var invoice = BuildInvoiceFromDr(result.Rows[0]);
                Date = invoice.Date;
                Discount = invoice.Discount;
                ClientId = invoice.ClientId;
                ClientName = invoice.ClientName;
            }
            void BuildingDetails()
            {
                string query =
                    "SELECT * " +
                    "FROM ordenes_servicio o " +
                    "INNER JOIN actividades a " +
                    "ON (o.cod_servicio = a.cod_servicio AND o.num_actividad = a.num_actividad) " +
                    "INNER JOIN servicios s " +
                    "ON (o.cod_servicio = s.cod_servicio) " +
                    "INNER JOIN empleados e " +
                    "ON (o.ced_empleado = e.cedula_e) " +
                    "INNER JOIN productos p " +
                    "ON (o.cod_producto = p.cod_producto) " +
                    "WHERE num_ficha = @num_ficha";
                PostgreOp op = new PostgreOp(query);
                op.PasarParametros("num_ficha", AssociatedRegistration.Number);

                //Colocar resultados en memoria
                DataTable result = ServiceOrder.QueryFromDataBase(op);

                //Pasar datos a una lista
                IEnumerable<ServiceOrder> details =
                    from DataRow dr in result.Rows
                    select ServiceOrder.BuildOrderFromDr(dr);

                Details = details.ToList();
            }
            void BuildingPayments()
            {
                string query =
                    "SELECT * " +
                    "FROM pagos p " +
                    "LEFT JOIN bancos b ON (p.cod_banco = b.cod_banco) " +
                    "WHERE num_factura = @num_factura";
                PostgreOp op = new PostgreOp(query);
                op.PasarParametros("num_factura", Number);

                //Colocar resultados en memoria
                DataTable result = Payment.QueryFromDataBase(op);

                //Pasar datos a una lista
                IEnumerable<Payment> payments =
                    from DataRow dr in result.Rows
                    select Payment.BuildPaymentFromDr(dr);

                Payments = payments.ToList();
            }
        }
        public static Invoice BuildInvoiceFromDr(dynamic dr) => new Invoice
        {
            Date = (DateTime) dr["fecha_emision"],
            Discount = Tools.Tools.Object2Decimal(dr["descuento_f"]),
            ClientId = dr["ced_cliente"].ToString(),
            ClientName = dr["nombre_c"].ToString(),
        };

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
