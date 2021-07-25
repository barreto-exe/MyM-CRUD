using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

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

        public static List<Employee> SearchEmployees(string search)
        {
            //Traer datos de la BD
            string query =
                "SELECT *, (SELECT 1 FROM franquicias WHERE cedula_e = supervisor) es_encargado " +
                "FROM empleados e " +
                "WHERE " +
                "(e.cedula_e LIKE @Search " +
                "OR e.nombre_e LIKE @Search " +
                "OR e.telefono_e LIKE @Search " +
                "OR e.direccion_e LIKE @Search) " +
                "AND e.rif_franquicia = @Rif";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");
            op.PasarParametros("Rif", App.Session.Branch);
            
            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Employee> employees =
                from DataRow dr in result.Rows
                select new Employee()
                {
                    Id = dr["cedula_e"].ToString(),
                    Name = dr["nombre_e"].ToString(),
                    Phone = dr["telefono_e"].ToString(),
                    Salary = Tools.Tools.Object2Decimal(dr["sueldo"]),
                    Address = dr["direccion_e"].ToString(),
                    IsManager = dr["es_encargado"] is DBNull ? false : true,
                };

            return employees.ToList();
        }

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO " +
                "empleados (cedula_e, nombre_e, telefono_e, sueldo, direccion_e, rif_franquicia) " +
                "VALUES (@cedula_e, @nombre_e, @telefono_e, @sueldo, @direccion_e, @rif_franquicia)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cedula_e", Id);
            op.PasarParametros("nombre_e", Name);
            op.PasarParametros("telefono_e", Phone);
            op.PasarParametros("sueldo", Salary);
            op.PasarParametros("direccion_e", Address);
            op.PasarParametros("rif_franquicia", App.Session.Branch);
            op.EjecutarComando();
        }
        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE empleados " +
                "SET nombre_e = @nombre_e, " +
                "telefono_e = @telefono_e, " +
                "sueldo = @sueldo, " +
                "direccion_e = @direccion_e, " +
                "rif_franquicia = @rif_franquicia " +
                "WHERE cedula_e = @cedula_e";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cedula_e", Id);
            op.PasarParametros("nombre_e", Name);
            op.PasarParametros("telefono_e", Phone);
            op.PasarParametros("sueldo", Salary);
            op.PasarParametros("direccion_e", Address);
            op.PasarParametros("rif_franquicia", App.Session.Branch);
            op.EjecutarComando();
        }
        protected override void BuildCrudObject(NpgsqlDataReader dr)
        {
            Id = dr["cedula_e"].ToString();
            Name = dr["nombre_e"].ToString();
            Phone = dr["telefono_e"].ToString();
            Salary = Tools.Tools.Object2Decimal(dr["sueldo"]);
            Address = dr["direccion_e"].ToString();
            IsManager = dr["es_encargado"] is DBNull ? false : true;
        }
        protected override PostgreOp GetObjectOp(object[] keys)
        {
            string query =
                "SELECT * " +
                "FROM empleados e " +
                "WHERE " +
                "e.cedula_e = @Id";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Id", keys[0]);

            return op;
        }
    }
}
