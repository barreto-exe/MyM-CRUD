using MyM_CRUD.DataBase;
using MyM_CRUD.Tools;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    /// <summary>
    /// Se encarga de gestionar las sesiones y los login.
    /// </summary>
    public class Session
    {
        public string User { get; set; }
        public string PassMd5 { get; set; }

        /// <summary>
        /// Comprueba que el usuario y la clave proporcionados sean correctos.
        /// </summary>
        /// <param name="message">Mensaje si ocurre un error al consultar. Sale vacío si no hubo error.</param>
        /// <returns>VerifyUserEnum</returns>
        public VerifyUserEnum VerifyUser(out string message)
        {
            VerifyUserEnum verified;
            message = "";

            string query =
                "SELECT 1 FROM FRANQUICIAS WHERE rif_franquicia = @user AND clave = @pass";
            PostgreOp op = new PostgreOp(query);
            op.PasarParametros("user", User);
            op.PasarParametros("pass", PassMd5);

            try
            {
                NpgsqlDataReader dr = op.EjecutarReader();
                if(dr.Read())
                {
                    verified = VerifyUserEnum.Success;
                }
                else
                {
                    message = "Credenciales incorrectas.";
                    verified = VerifyUserEnum.WrongCredentials;
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                message = "Ha ocurrido un error consultando los datos del usuario.";
                Log.Add($"{message}: /n {ex.Message} /n HRESULT: {ex.HResult}");
                
                verified = VerifyUserEnum.Error;
            }

            return verified;
        }

        public enum VerifyUserEnum
        {
            Error,
            Success,
            WrongCredentials,
        }
    }
}
