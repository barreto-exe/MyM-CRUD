using System;
using System.Collections.Generic;

namespace MyM_CRUD.DataBase
{
    public abstract class SqlOp<Conexion, Comando, Reader, Adapter>
    {
        /// <summary>
        /// Una conexión del tipo correspondiente a cada motor de base de datos.
        /// Lo ideal es usar la misma en todo el programa.
        /// </summary>
        public static Conexion ConexionGlobal;
        protected static string Servidor;
        protected static string BaseDatos;
        protected static string Usuario;
        protected static string Password;

        protected List<KeyValuePair<string, object>> parametros;
        public string Query { get; set; }

        public SqlOp(string query)
        {
            Query = query;
            parametros = new List<KeyValuePair<string, object>>();
        }
        public SqlOp()
        {
            parametros = new List<KeyValuePair<string, object>>();
        }

        /// <summary>
        /// Enviar parámetros con sus valores 
        /// en caso de que el query lo requiera.
        /// </summary>
        /// <param name="nombre">Nombre del parámetro</param>
        /// <param name="valor">Valor del parámetro</param>
        public void PasarParametros(string nombre, object valor)
        {
            parametros.Add(new KeyValuePair<string, object>(nombre, valor));
        }

        /// <summary>
        /// Crea una nueva conexión a la Base de Datos. Los datos de conexión
        /// y demás son de sólo lectura y están indicados en la configuración del programa.
        /// </summary>
        /// <returns>La conexión abierta, o null si no se pudo abrir.</returns>
        public abstract Conexion NuevaConexion();
        /// <summary>
        /// El comando SQL generado a partir del string del query y los
        /// parámetros indicados.
        /// </summary>
        /// <returns>El comando SQL.</returns>
        public abstract Comando ComandoGenerado();
        /// <summary>
        /// Ejecuta el comando de lectura de datos de la operación.
        /// </summary>
        /// <returns>DataReader correspondiente al resultado del comando.</returns>
        public abstract Reader EjecutarReader();
        /// <summary>
        /// Genera el DataAdapter correspondiente al comando.
        /// </summary>
        /// <returns>DataAdapter generado.</returns>
        public abstract Adapter ObtenerAdapter();
        /// <summary>
        /// Ejecuta el comando generado por la operación.
        /// </summary>
        /// <returns>El número de registros afectados por el comando</returns>
        public abstract int EjecutarComando();

        public abstract void RecargarDatosConexion();

        /// <summary>
        /// ¡NO USAR PARA EJECUTAR QUERY, SÓLO PARA DEBUG!.
        /// Genera el string equivalente del query reemplazando los nombres de los parámetros
        /// por sus valores.
        /// </summary>
        /// <returns>String equivalente del query y sus parámetros</returns>
        public override string ToString()
        {
            string result = Query;
            foreach (KeyValuePair<string, object> parametro in parametros)
            {
                switch (parametro.Value)
                {
                    case int i:
                    case double d:
                    case decimal dd:
                    case float f:
                        result = result.Replace('@' + parametro.Key, parametro.Value.ToString());
                        break;
                    case DateTime date:
                        result = result.Replace('@' + parametro.Key, "'" + date.ToString("MM/dd/yyyy HH:mm:ss") + "'");
                        break;
                    default:
                        result = result.Replace('@' + parametro.Key, "'" + parametro.Value.ToString() + "'");
                        break;
                }

            }

            return result;
        }
    }
}
