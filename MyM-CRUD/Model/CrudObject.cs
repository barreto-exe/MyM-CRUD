using MyM_CRUD.DataBase;
using MyM_CRUD.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MyM_CRUD.Model
{
    public abstract class CrudObject <T>
    {
        /// <summary>
        /// Actualiza el registro en la base de datos (usa el código cómo referencia).
        /// </summary>
        /// <returns>Mensaje de error si la operación falla. Vacío si la operación es exitosa.</returns>
        public abstract void UpdateTupleDataBase();

        /// <summary>
        /// Registra la tupla en la base de datos.
        /// </summary>
        /// <returns>Mensaje de error si la operación falla. Vacío si la operación es exitosa.</returns>
        public abstract void InsertTupleDatabase();

        /// <summary>
        /// Infla el objeto con la tupla encontrada en la base de datos.
        /// </summary>
        /// <param name="key">Parámetro de búsqueda.</param>
        /// <returns></returns>
        public void SelectFromDatabase(object[] keys)
        {
            //Verificar que no haya claves vacías
            if(keys != null && keys.Length > 0)
            {
                //Construir consulta a la bd
                PostgreOp op = GetObjectOp(keys);

                try
                {
                    //Ejecutar consulta
                    NpgsqlDataReader dr = op.EjecutarReader();

                    //Si hay datos, construir objeto
                    if (dr.Read())
                    {
                        BuildCrudObject(dr);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    string message = "Ha ocurrido un error consultando una tupla.";
                    Log.Add($"{message}: /n {ex.Message} /n HRESULT: {ex.HResult}");
                }
            }
        }
        protected abstract PostgreOp GetObjectOp(object[] keys);
        protected abstract void BuildCrudObject(NpgsqlDataReader dr);


        protected static DataTable QueryFromDataBase(PostgreOp op)
        {
            DataSet set = new DataSet();
            try
            {
                DataAdapter da = op.ObtenerAdapter();
                da.Fill(set);
            }
            catch (Exception ex)
            {

            }

            return set.Tables[0];
        }
    }
}
