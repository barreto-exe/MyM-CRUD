using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MyM_CRUD.Model
{
    public class Registration : CrudObject<Registration>
    {
        public string Number { get; set; }
        public DateTime? In { get; set; }
        public DateTime? EstimatedOut { get; set; }
        public DateTime? RealOut { get; set; }
        public string AuthPersonId { get; set; }
        public string VehicleId { get; set; }
        public string VehicleDescription { get; set; }

        public static List<Registration> GetRegistrationsFromBD()
        {
            string query =
                "SELECT * " +
                "FROM registros " +
                "WHERE rif_franquicia = @rif_franquicia";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("rif_franquicia", App.Session.Branch);

            //Colocar resultados en memoria
            DataTable result = QueryFromDataBase(op);

            //Pasar datos a una lista
            IEnumerable<Registration> r =
                from DataRow dr in result.Rows
                select BuildRegistrationFromDr(dr);

            return r.ToList();
        }
        public static Registration BuildRegistrationFromDr(dynamic dr) => new Registration()
        {
            Number = dr["num_ficha"].ToString(),
            In = dr["fecha_ent"] as DateTime?,
            EstimatedOut = dr["fecha_sal_est"] as DateTime?,
            RealOut = dr["fecha_sal_real"] as DateTime?,
            AuthPersonId = dr["ced_autorizado"].ToString(),
            VehicleId = dr["placa_vehiculo"].ToString(),
        };

        public override void InsertTupleDatabase()
        {
            string query =
                "INSERT INTO " +
                "registros (num_ficha, fecha_ent, fecha_sal_est, fecha_sal_real, " +
                "ced_autorizado, placa_vehiculo, rif_franquicia) " +
                "VALUES (@num_ficha, @fecha_ent, @fecha_sal_est, @fecha_sal_real, " +
                "@ced_autorizado, @placa_vehiculo, @rif_franquicia)";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_ficha", Number);
            op.PasarParametros("fecha_ent", In);
            op.PasarParametros("fecha_sal_est", EstimatedOut);
            if (RealOut == null)
            {
                op.PasarParametros("fecha_sal_real", DBNull.Value);
            }
            else
            {
                op.PasarParametros("fecha_sal_real", RealOut);
            }
            op.PasarParametros("ced_autorizado", AuthPersonId);
            op.PasarParametros("placa_vehiculo", VehicleId);
            op.PasarParametros("rif_franquicia", App.Session.Branch);
            op.EjecutarComando();
        }

        public override void UpdateTupleDataBase()
        {
            string query =
                "UPDATE registros " +
                "SET fecha_ent = @fecha_ent, " +
                "fecha_sal_est = @fecha_sal_est, " +
                "fecha_sal_real = @fecha_sal_real, " +
                "ced_autorizado = @ced_autorizado, " +
                "placa_vehiculo = @placa_vehiculo," +
                "rif_franquicia = @rif_franquicia, " +
                "num_reserva = @num_reserva " +
                "WHERE num_ficha = @num_ficha";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("num_ficha", Number);
            op.PasarParametros("fecha_ent", In);
            op.PasarParametros("fecha_sal_est", EstimatedOut);
            op.PasarParametros("fecha_sal_real", RealOut);
            op.PasarParametros("ced_autorizado", AuthPersonId);
            op.PasarParametros("placa_vehiculo", VehicleId);
            op.PasarParametros("rif_franquicia", App.Session.Branch);
            op.EjecutarComando();
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
