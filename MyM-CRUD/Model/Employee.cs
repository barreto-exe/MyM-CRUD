using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Employee : CrudObject<Employee>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public bool IsManager { get; set; }

        public override string InsertTupleDatabase()
        {
            throw new NotImplementedException();
        }
        public override string UpdateTupleDataBase()
        {
            throw new NotImplementedException();
        }
        protected override void BuildCrudObject(NpgsqlDataReader dr)
        {
            Id = dr["ced_empleado"].ToString();
            Name = dr["nombre_p"].ToString();
            Phone = dr["telefono_ps"].ToString();
            Salary = Tools.Tools.Object2Decimal(dr["sueldo"]);
            Address = dr["direccion_e"].ToString();
            IsManager = dr["es_encargado"].ToString() == "t";
        }
        protected override PostgreOp GetObjectOp(Dictionary<string, object> keys)
        {
            string query =
                "SELECT * " +
                "FROM empleados e, personas p " +
                "WHERE " +
                "e.ced_empleado = p.cedula_p AND" +
                "e.ced_empleado = @Id";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Id", keys["Id"]);

            return op;
        }
    }
}
