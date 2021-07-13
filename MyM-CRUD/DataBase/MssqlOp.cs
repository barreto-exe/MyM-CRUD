using MyM_CRUD.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyM_CRUD.DataBase
{
    public class MssqlOp : SqlOp<SqlConnection, SqlCommand, SqlDataReader, SqlDataAdapter>
    {
        static MssqlOp()
        {
            //Credenciales desde archivo de configuración.
            Servidor = Properties.DataBase.Default.ServidorMsSql;
            BaseDatos = Properties.DataBase.Default.BaseDatosMsSql;
            Usuario = Properties.DataBase.Default.UsuarioMsSql;
            Password = Properties.DataBase.Default.PassMsSql;
        }

        public MssqlOp() : base() { }
        public MssqlOp(string query) : base(query) { }

        public override SqlCommand ComandoGenerado()
        {
            SqlCommand command = new SqlCommand()
            {
                CommandText = Query,
                Connection = ConexionGlobal
            };

            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }

            return command;
        }

        ///<inheritdoc/>
        ///<exception cref="SqlException"></exception>
        ///<exception cref="InvalidCastException"></exception>
        public override int EjecutarComando()
        {
            try
            {
                return ComandoGenerado().ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case InvalidCastException ice:
                        throw ice;
                    case SqlException se:
                        throw se;
                    default:
                        return 0;
                }
            }
        }

        ///<inheritdoc/>
        ///<exception cref="SqlException"></exception>
        ///<exception cref="InvalidCastException"></exception>
        public override SqlDataReader EjecutarReader()
        {
            try
            {
                return ComandoGenerado().ExecuteReader();
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case InvalidCastException ice:
                        throw ice;
                    case SqlException se:
                        throw se;
                    default:
                        throw ex;
                }
            }
        }

        public override SqlConnection NuevaConexion()
        {
            RecargarDatosConexion();
            SqlConnection connection = new SqlConnection
            (
                "Server=" + Servidor + ";Database=" + BaseDatos + ";User Id=" + Usuario + ";Password=" + Password + ";"
            );
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Log.Add("MssqlOp", "NuevaConexion()", ex.Message);
                return null;
            }
        }

        public override SqlDataAdapter ObtenerAdapter()
        {
            return new SqlDataAdapter(ComandoGenerado());
        }

        public override void RecargarDatosConexion()
        {
            //Credenciales desde archivo de configuración.
            Servidor = Properties.DataBase.Default.ServidorMsSql;
            BaseDatos = Properties.DataBase.Default.BaseDatosMsSql;
            Usuario = Properties.DataBase.Default.UsuarioMsSql;
            Password = Properties.DataBase.Default.PassMsSql;
        }
    }
}
