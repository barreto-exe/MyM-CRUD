using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


        public static List<Client> SearchClients(string search)
        {
            //Traer datos de la BD
            string query =
                "SELECT * " +
                "FROM clientes  " +
                "WHERE " +
                "cedula_c LIKE @Search " +
                "OR nombre_c LIKE @Search " +
                "OR tlf_principal LIKE @Search " +
                "OR tlf_alternativo LIKE @Search " +
                "OR email LIKE @Search";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Search", $"%{search}%");

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Client> clients =
                from DataRow dr in result.Rows
                select new Client()
                {
                    Id = dr["cedula_c"].ToString(),
                    Name = dr["nombre_c"].ToString(),
                    Phone = dr["tlf_principal"].ToString(),
                    PhoneAlt = dr["tlf_alternativo"].ToString(),
                    Email = dr["email"].ToString(),
                };

            return clients.ToList();
        }

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT " +
                "INTO clientes (cedula_c, nombre_c, tlf_principal, tlf_alternativo, email) " +
                "VALUES (@cedula_c, @nombre_c, @tlf_principal, @tlf_alternativo, @email)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cedula_c", Id);
            op.PasarParametros("nombre_c", Name);
            op.PasarParametros("tlf_principal", Phone);
            op.PasarParametros("tlf_alternativo", PhoneAlt);
            op.PasarParametros("email", Email);
            op.EjecutarComando();
        }

        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE clientes " +
                "SET nombre_c = @nombre_c, " +
                "tlf_principal = @tlf_principal, " +
                "tlf_alternativo = @tlf_alternativo, " +
                "email = @email " +
                "WHERE cedula_c = @cedula_c";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("cedula_c", Id);
            op.PasarParametros("nombre_c", Name);
            op.PasarParametros("tlf_principal", Phone);
            op.PasarParametros("tlf_alternativo", PhoneAlt);
            op.PasarParametros("email", Email);
            op.EjecutarComando();
        }

        protected override void BuildCrudObject(NpgsqlDataReader dr)
        {
            Id = dr["cedula"].ToString();
            Name = dr["nombre_c"].ToString();
            Phone = dr["tlf_principal"].ToString();
            PhoneAlt = dr["tlf_alternativo"].ToString();
            Email = dr["email"].ToString();
        }

        protected override PostgreOp GetObjectOp(object[] keys)
        {
            string query =
                "SELECT * " +
                "FROM clientes " +
                "WHERE " +
                "cedula_c = @Id";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Id", keys[0]);

            return op;
        }
    }
}
