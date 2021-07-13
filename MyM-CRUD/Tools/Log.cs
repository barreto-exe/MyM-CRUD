using System;
using System.IO;

namespace MyM_CRUD.Tools
{
    public static class Log
    {
        private static string path;
        static Log()
        {
            path = Environment.CurrentDirectory.ToString() + "\\Logs";
        }

        public static void Add(string mensaje)
        {
            CreateDirectory();
            string nombre = GetNameFile();
            string cadena = "";

            cadena += DateTime.Now + " - " + mensaje + Environment.NewLine;

            StreamWriter sw = new StreamWriter(path + "/" + nombre, true);
            sw.Write(cadena);
            sw.Close();
        }

        public static void Add(string clase, string metodo, string mensaje)
        {
            Add("Clase: " + clase + ", Método: " + metodo + ".\n" +
                "Mensaje: " + mensaje);
        }

        private static string GetNameFile()
        {
            string nombre = "log_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt";
            return nombre;
        }

        private static void CreateDirectory()
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
