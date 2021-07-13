using MyM_CRUD.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyM_CRUD.DataBase
{
    public class MysqlOp : SqlOp<MySqlConnection, MySqlCommand, MySqlDataReader, MySqlDataAdapter>
    {
        static MysqlOp()
        {
            //Credenciales desde archivo de configuración.
            Servidor = Properties.DataBase.Default.ServidorMySql;
            BaseDatos = Properties.DataBase.Default.BaseDatosMySql;
            Usuario = Properties.DataBase.Default.UsuarioMySql;
            Password = Properties.DataBase.Default.PassMySql;
        }
        public MysqlOp() : base() { }
        public MysqlOp(string query) : base(query) { }

        public override MySqlCommand ComandoGenerado()
        {
            MySqlCommand command = new MySqlCommand()
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
                    case MySqlException se:
                        throw se;
                    default:
                        return 0;
                }
            }
        }

        ///<inheritdoc/>
        ///<exception cref="MySqlException"></exception>
        ///<exception cref="InvalidCastException"></exception>
        public override MySqlDataReader EjecutarReader()
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
                    case MySqlException se:
                        throw se;
                    default:
                        throw ex;
                }
            }
        }
        public override MySqlConnection NuevaConexion()
        {
            RecargarDatosConexion();
            MySqlConnection connection = new MySqlConnection
            (
                "Host=" + Servidor + ";" +
                "Database=" + BaseDatos + ";" +
                "Username=" + Usuario + ";" +
                "Password=" + Password + ";"
            );
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Log.Add("MysqlOp", "NuevaConexion()", ex.Message);
                return null;
            }
        }
        public override MySqlDataAdapter ObtenerAdapter()
        {
            return new MySqlDataAdapter(ComandoGenerado());
        }

        public override void RecargarDatosConexion()
        {
            //Credenciales desde archivo de configuración.
            Servidor = Properties.DataBase.Default.ServidorMySql;
            BaseDatos = Properties.DataBase.Default.BaseDatosMySql;
            Usuario = Properties.DataBase.Default.UsuarioMySql;
            Password = Properties.DataBase.Default.PassMySql;
        }
    }
}
