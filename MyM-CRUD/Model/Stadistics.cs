using LiveCharts;
using LiveCharts.Wpf;
using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MyM_CRUD.Model
{
    public static class Stadistics
    {
        public static void ChartProductSellCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
        public static void ChartFrequentClientsCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
        public static void ChartBranchesCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
        public static (List<string>, ChartValues<double>) ChartVehicleBrandsCollection()
        {
            PostgreOp op = null;

            //Eliminar tablas en memoria
            try
            {
                string query =
                    "DROP TABLE R1; " +
                    "DROP TABLE R2; ";
                op = new PostgreOp(query);
                op.EjecutarComando();
            }
            catch { }

            var labels = new List<string>();
            var values = new ChartValues<double>();
            //Traer datos
            try
            {

                #region query
                op.Query =
                    "SELECT s.nombre_S, ma.nom_marca, COUNT(r.*) AS cantidad " +
                    "INTO TEMP R1 " +
                    "FROM registros r, ordenes_servicio os, servicios s, " +
                    "     vehiculos v, modelos mo, marcas ma " +
                    "WHERE r.placa_Vehiculo = v.placa " +
                    "AND v.nom_modelo = mo.nom_modelo " +
                    "AND mo.cod_marca = ma.cod_Marca " +
                    "AND r.num_Ficha = os.num_Ficha " +
                    "AND os.cod_Servicio = s.cod_Servicio " +
                    "AND r.rif_franquicia = @rif_franquicia " +
                    "GROUP BY 1, 2; " +

                    "SELECT R1.nombre_S, MAX(R1.cantidad) AS maximo " +
                    "INTO TEMP R2 " +
                    "FROM R1 " +
                    "GROUP BY 1; " +

                    "SELECT R1.* " +
                    "FROM R1, R2 " +
                    "WHERE R1.nombre_S = R2.nombre_S " +
                    "AND R1.cantidad = R2.maximo " +
                    "ORDER BY R1.nombre_s;";
                #endregion
                op.PasarParametros("rif_franquicia", App.Session.Branch);
                NpgsqlDataReader dr = op.EjecutarReader();
                while(dr.Read())
                {
                    labels.Add($"{dr["nom_marca"]}: {dr["nombre_s"]}");
                    values.Add(Tools.Tools.Object2Double(dr["cantidad"]));
                }
                dr.Close();
            }
            catch { }

            return (labels, values);
        }
        public static void ChartHighServiceCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
        public static (List<string>, ChartValues<double>) ChartHighWorkerCollection()
        {
            PostgreOp op = null;

            //Eliminar tablas en memoria
            try
            {
                string query =
                    "DROP TABLE R1; " +
                    "DROP TABLE R2; ";
                op = new PostgreOp(query);
                op.EjecutarComando();
            }
            catch { }

            var labels = new List<string>();
            var values = new ChartValues<double>();
            //Traer datos
            try
            {
                #region query
                op.Query = "SELECT os.num_ficha, os.cod_Servicio, os.ced_empleado" +
                "INTO TEMP R1" +
                "FROM ordenes_servicio os, registros r" +
                "WHERE os.num_Ficha = r.num_Ficha" +
                "AND r.franquicia = @rif_franquicia" +
                //"AND to_char(r.fecha_Ent, 'yyyy') = @anio" +
                //"AND to_char(r.fecha_Ent, 'mm') = @mes" +
                "GROUP BY 1, 2, 3;" +

                "SELECT R1.ced_empleado, COUNT(R1.cod_Servicio) AS cantidad" +
                "FROM R1" +
                "GROUP BY 1;";
                #endregion

                op.PasarParametros("rif_franquicia", App.Session.Branch);
                //op.PasarParametros("mes", 8);
                //op.PasarParametros("anio", 2021);
                NpgsqlDataReader dr = op.EjecutarReader();
                while (dr.Read())
                {
                    labels.Add($"{dr["ced_empleado"]}");
                    values.Add(Tools.Tools.Object2Double(dr["cantidad"]));
                }
                dr.Close();
            }
            catch { }

            return (labels, values);  
        }
        public static void ChartHighSupplierCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
    }
}
