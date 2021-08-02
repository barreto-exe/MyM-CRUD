using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class ServiceOrder : CrudObject<ServiceOrder>
    {
        private ServiceActivity associatedActivity;
        public ServiceActivity AssociatedActivity
        {
            get { return associatedActivity; }
            set
            {
                associatedActivity = value;
                ServiceName = AssociatedActivity.Servicio;
                ActivityName = AssociatedActivity.Description;
            }
        }

        public string RegistrationNumber { get; set; }
        public string OrderNumber
        {
            get => $"ORD{RegistrationNumber}{AssociatedActivity.ServiceCode}{AssociatedActivity.Number}";
        }
        public string EmployeeId { get; set; }
        public string ProductId { get; set; }
        public decimal ManPowerCost { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

        //Atributos para mostrar en facturas
        public string ServiceName { get; set; }
        public string ActivityName { get; set; }
        public string EmployeeName { get; set; }
        public string ProductName { get; set; }
        public decimal Subtotal { get => ManPowerCost + ProductQuantity * ProductPrice; }

        public ServiceOrder(ServiceActivity asociatedActivity)
        {
            AssociatedActivity = asociatedActivity;
        }

        public ServiceOrder()
        {
        }

        public static ServiceOrder BuildOrderFromDr(dynamic dr) => new ServiceOrder()
        {
            ServiceName = dr["nombre_s"].ToString(),
            ActivityName = dr["descripcion_a"].ToString(),
            EmployeeName = dr["nombre_e"].ToString(),
            ManPowerCost = Tools.Tools.Object2Decimal(dr["precio_mano_obra"]),
            ProductName = dr["nombre_p"].ToString(),
            ProductQuantity = (int)dr["cant_producto"],
            ProductPrice = Tools.Tools.Object2Decimal(dr["precio_prod"]),
        };


        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO ordenes_servicio(num_ficha, cod_servicio, num_actividad, num_orden_s, " +
                "ced_empleado, cod_producto, precio_mano_obra, cant_producto, precio_prod) " +
                "VALUES (@num_ficha, @cod_servicio, @num_actividad, @num_orden_s, " +
                "@ced_empleado, @cod_producto, @precio_mano_obra, @cant_producto, @precio_prod)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_ficha", RegistrationNumber);
            op.PasarParametros("cod_servicio", AssociatedActivity.ServiceCode);
            op.PasarParametros("num_actividad", AssociatedActivity.Number);
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
