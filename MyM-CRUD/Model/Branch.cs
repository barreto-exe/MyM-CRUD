using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Branch : CrudObject<Branch>
    {
        public string Rif { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Employee Manager { get; set; }
        public string City { get; set; }

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
            Rif = dr["rif_franquicia"].ToString();
            Name = dr["nombre_f"].ToString();
            Address = dr["direccion_f"].ToString();
            City = dr["ciudad"].ToString();

            Manager = new Employee();
            Manager.Name = dr["nombre_e"].ToString();
            Manager.Id = dr["cedula_e"].ToString();
        }
        protected override PostgreOp GetObjectOp(object[] keys)
        {
            string query =
                "SELECT * " +
                "FROM franquicias f, empleados e " +
                "WHERE " +
                "f.supervisor = e.cedula_e " +
                "AND f.rif_franquicia = @Rif ";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Rif", keys[0]);
            
            return op;
        }
    }
}
