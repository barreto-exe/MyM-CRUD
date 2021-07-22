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
        public string ManagerName { get; set; }
        public string City { get; set; }

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
            Rif = dr["rif_franquicia"].ToString();
            Name = dr["nombre_f"].ToString();
            Address = dr["direccion_f"].ToString();
            ManagerName = dr["nombre_p"].ToString();
            City = dr["nom_ciudad"].ToString();
        }
        protected override PostgreOp GetObjectOp(object[] keys)
        {
            string query =
                "SELECT * " +
                "FROM franquicias f, personas p " +
                "WHERE " +
                "f.ced_encargado = p.cedula_p AND " +
                "f.rif_franquicia = @Rif ";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("Rif", keys[0]);
            
            return op;
        }
    }
}
