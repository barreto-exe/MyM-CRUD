﻿using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MyM_CRUD.Tools
{
    static class Tools
    {
        public static decimal Object2Decimal(object o)
        {
            return o == null || o is DBNull ? 0m : Convert.ToDecimal(o);
        }
        public static double Object2Double(object o)
        {
            return o == null || o is DBNull ? 0 : Convert.ToDouble(o);
        }

        public static string ValidEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            email = email.Trim();
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return email;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static (string, string) GetRandomPhraseAndAuth()
        {
            string randomPhrase, auth;
            string query =
                "SELECT * " +
                "FROM eco_mensajes " +
                "ORDER BY random() " +
                "LIMIT 1";
            PostgreOp op = new PostgreOp(query);
            NpgsqlDataReader dr = op.EjecutarReader();
            dr.Read();
            randomPhrase = $"\"{dr.GetString("msj")}\".";
            auth = $"-{dr.GetString("autor")}.";
            dr.Close();
            return (randomPhrase, auth);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
