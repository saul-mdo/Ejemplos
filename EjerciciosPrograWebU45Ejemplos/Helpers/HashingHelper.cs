using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosPrograWebU45Ejemplos.Helpers
{
    public static class HashingHelper
    {
        // PARA OBTENER LA HUELLA DIGITAL DE UN STRING
        public static string GetHash(string cadena)
        {
            var alg = SHA256.Create();
            byte[] codificar = Encoding.UTF8.GetBytes(cadena);
            byte[] hash = alg.ComputeHash(codificar);

            string res = "";
            foreach (var item in hash)
            {
                res += item.ToString("x2");
            }
            return res;
        }

        // PARA OBTENER LA HUELLA DIGITAL DE UN ARCHIVO
        public static string GetHashArchivo(FileStream archivo)
        {
            var alg = SHA256.Create();
            byte[] codificar = new byte[archivo.Length];
            archivo.Read(codificar, 0, (int)archivo.Length);
            byte[] hash = alg.ComputeHash(codificar);

            string res = "";
            foreach (var item in hash)
            {
                res += item.ToString("x2");
            }
            return res;
        }

        // PARA OBTENER LA HUELLA DIGITAL DE UN OBJETO
        public static string GetHashObjeto(object obj)
        {
            var alg = SHA256.Create();
            BinaryFormatter br = new BinaryFormatter();
            MemoryStream memory = new MemoryStream();
            br.Serialize(memory, obj);

            byte[] hash = alg.ComputeHash(memory);

            string res = "";
            foreach (var item in hash)
            {
                res += item.ToString("x2");
            }
            return res;
        }
    }
}
