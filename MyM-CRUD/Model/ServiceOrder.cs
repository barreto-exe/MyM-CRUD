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
        public string ServiceCode { get => AsociatedActivity.ServiceCode; }
        public int ActivityNumber { get => AsociatedActivity.Number; }
        public string OrderNumber
        {
            get => $"ORD{RegistrationNumber}{AsociatedActivity.ServiceCode}{AsociatedActivity.Number}";
        }
        public string EmployeeId { get; set; }
        public string ProductId { get; set; }
        public decimal ManPowerCost { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

        public ServiceOrder(ServiceActivity asociatedActivity)
        {
            AsociatedActivity = asociatedActivity;
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
