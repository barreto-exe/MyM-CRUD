using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Client : CrudObject<Client>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhoneAlt { get; set; }
        public string Email { get; set; }

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
