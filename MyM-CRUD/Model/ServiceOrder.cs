using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class ServiceOrder : CrudObject<ServiceOrder>
    {
        public ServiceActivity AsociatedActivity { get; set; }
        public string RegistrationNumber { get; set; }
        public string OrderNumber
        {
            get => $"ORD{RegistrationNumber}{AsociatedActivity.ServiceCode}{AsociatedActivity.Number}";
        }
        public string EmployeeId { get; set; }
        public string ProductId { get; set; }
        public decimal ManPowerCost { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

        //Atributos para mostrar en facturas
        public string ServiceName { get => AsociatedActivity.Servicio; }
        public string ActivityName { get => AsociatedActivity.Description; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public decimal Subtotal { get => ManPowerCost + ProductQuantity * ProductPrice; }

        public ServiceOrder(ServiceActivity asociatedActivity)
        {
            AsociatedActivity = asociatedActivity;
        }

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO ordenes_servicio(num_ficha, cod_servicio, num_actividad, num_orden_s, " +
                "ced_empleado, cod_producto, precio_mano_obra, cant_producto, precio_prod) " +
                "VALUES (@num_ficha, @cod_servicio, @num_actividad, @num_orden_s, " +
                "@ced_empleado, @cod_producto, @precio_mano_obra, @cant_producto, @precio_prod)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_ficha", RegistrationNumber);
            op.PasarParametros("cod_servicio", AsociatedActivity.ServiceCode);
            op.PasarParametros("num_actividad", AsociatedActivity.Number);
            op.PasarParametros("num_orden_s", OrderNumber);
            op.PasarParametros("ced_empleado", EmployeeId);
            op.PasarParametros("precio_mano_obra", ManPowerCost);
            op.PasarParametros("cant_producto", ProductQuantity);
            op.PasarParametros("precio_prod", ProductPrice);
            if(ProductId == "")
            {
                op.PasarParametros("cod_producto", DBNull.Value);
            }
            else
            {
                op.PasarParametros("cod_producto", ProductId);
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
