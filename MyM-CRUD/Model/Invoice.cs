using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Invoice : CrudObject<Invoice>
    {
        public string Number
        {
            get => $"FACT-{RegistrationId}";
        }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public string ClientId { get; set; }
        public string RegistrationId { get; set; }

        public List<ServiceOrder> Details { get; set; }
        public List<Payment> Payments { get; set; }

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
