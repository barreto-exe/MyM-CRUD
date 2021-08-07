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
        public static (List<string>, ChartValues<double>) ChartProductSellCollection()
        {
            DeleteInmemoryTables();

            PostgreOp op = null;

            var labels = new List<string>();
            var values = new ChartValues<double>();
            //Traer datos
            try
            {
                #region query
                op.Query =
                    "SELECT p.nombre_p, SUM(r.cant_Comprada) AS suma"+
                    "INTO TEMP R1"+
                    "FROM registran r, productos p, facturas fa"+
                    "WHERE fa.franquicia = @rif_franquicia"+
                    "WHERE fa.num_Factura = r.num_fact_tienda"+
                    "AND r.cod_tienda_prod = p.cod_Producto"+
                    "GROUP BY 1;"+

                    "SELECT nombre_p"+
                    "INTO TEMP producto_menos_vendido_T"+
                    "FROM R1"+
                    "WHERE suma = (SELECT MIN(suma) FROM R1);"+

                    "SELECT nombre_p"+
                    "INTO TEMP producto_mas_vendido_T"+
                    "FROM R1"+
                    "WHERE suma = (SELECT MAX(suma) FROM R1);"+

                    "SELECT menosv.nombre_p AS producto_menos_vendido,"+
                    "masv.nombre_p AS producto_mas_vendido"+
                    "FROM producto_menos_vendido_T menosv, producto_mas_vendido_T masv;";
                #endregion
                op.PasarParametros("rif_franquicia", App.Session.Branch);
                NpgsqlDataReader dr = op.EjecutarReader();
                while (dr.Read())
                {
                    labels.Add($"{dr["nombre_p"]}");
                    values.Add(Tools.Tools.Object2Double(dr["suma"]));
                }
                dr.Close();
            }
            catch { }

            return (labels, values);
        }
        public static (List<string>, ChartValues<double>) ChartFrequentClientsCollection()
        {
            DeleteInmemoryTables();

            PostgreOp op = null;

            var labels = new List<string>();
            var values = new ChartValues<double>();
            //Traer datos
            try
            {
                #region query
                op.Query = "SELECT c.cedula_C, c.nombre_C, COUNT(r.*) AS cantidad"+
                            "INTO TEMP R1"+
                            "FROM registros r, vehiculos v, clientes c"+
                            "WHERE r.placa_Vehiculo = v.placa"+
                            "AND v.ced_dueno = c.cedula_C"+
                            "AND r.franquicia = @rif_franquicia" +
                            "GROUP BY 1, 2;"+

                            "SELECT cedula_C, nombre_C"+
                            "INTO TEMP menos_frecuente_T"+
                            "FROM R1"+
                            "WHERE cantidad = (SELECT MIN(cantidad) FROM R1);"+

                            "SELECT cedula_C, nombre_C"+
                            "INTO TEMP mas_frecuente_T"+
                            "FROM R1"+
                            "WHERE cantidad = (SELECT MAX(cantidad) FROM R1);"+

                            "SELECT menosf.cedula_C AS cedula_cliente_menos_frecuente,  menosf.nombre_C AS nombre_cliente_menos_frecuente,"+
                            "masf.cedula_C AS cedula_cliente_mas_frecuente, masf.nombre_C AS nombre_cliente_mas_frecuente"+
                            "FROM menos_frecuente_T menosf, mas_frecuente_T masf;";
                #endregion
                op.PasarParametros("rif_franquicia", App.Session.Branch);
                NpgsqlDataReader dr = op.EjecutarReader();
                while (dr.Read())
                {
                    labels.Add($"{dr["nombre_C"]}");
                    values.Add(Tools.Tools.Object2Double(dr["cantidad"]));
                }
                dr.Close();
            }
            catch { }

            return (labels, values);
        }
        public static void ChartBranchesCollection()
        {
            SeriesCollection result = new SeriesCollection();
        }
        public static (List<string>, ChartValues<double>) ChartVehicleBrandsCollection()
        {
            DeleteInmemoryTables();

            PostgreOp op = null;

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
        public static (List<string>, ChartValues<double>) ChartHighServiceCollection()
        {
            DeleteInmemoryTables();

            PostgreOp op = null;

            var labels = new List<string>();
            var values = new ChartValues<double>();
            //Traer datos
            try
            {

                #region query
                op.Query =
                    "SELECT os.num_Ficha, s.nombre_S"+
                    "INTO TEMP R1"+
                    "FROM ordenes_servicio os, servicios s, registros r"+
                    "WHERE os.cod_Servicio = s.cod_Servicio"+
                    "AND os.num_Ficha = r.num_Ficha"+
                    "AND r.franquicia = @rif_franquicia"+
                    "GROUP BY 1, 2;"+

                    "SELECT nombre_S, COUNT(*) AS cantidad"+
                    "INTO TEMP R2"+
                    "FROM R1"+
                    "GROUP BY 1;"+
                    
                    "SELECT nombre_S"+
                    "INTO servicio_menos_solicitado_T"+
                    "FROM R2"+
                    "WHERE cantidad = (SELECT MIN(cantidad) FROM R2);"+

                    "SELECT nombre_S"+
                    "INTO servicio_mas_solicitado_T"+
                    "FROM R2"+
                    "WHERE cantidad = (SELECT MAX(cantidad) FROM R2);"+

                    "SELECT menos.nombre_S AS  servicio_menos_solicitado,"+
                    "mas.nombre_S AS  servicio_mas_solicitado"+
                    "FROM servicio_menos_solicitado_T menos, servicio_mas_solicitado_T mas;";
                #endregion
                op.PasarParametros("rif_franquicia", App.Session.Branch);
                NpgsqlDataReader dr = op.EjecutarReader();
                while (dr.Read())
                {
                    labels.Add($"{dr["nombre_S"]}");
                    values.Add(Tools.Tools.Object2Double(dr["cantidad"]));
                }
                dr.Close();
            }
            catch { }

            return (labels, values);
        }
        public static (List<string>, ChartValues<double>) ChartHighWorkerCollection()
        {
            DeleteInmemoryTables();

            PostgreOp op = null;

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

        private static void DeleteInmemoryTables()
        {
            //Eliminar tablas en memoria
            try
            {
                string query =
                    "DROP TABLE R1; " +
                    "DROP TABLE R2; ";
                PostgreOp op = new PostgreOp(query);
                op.EjecutarComando();
            }
            catch { }
        }
    }
}
