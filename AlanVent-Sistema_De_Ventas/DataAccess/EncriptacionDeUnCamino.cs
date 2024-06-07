using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlanVent_Sistema_De_Ventas.DataAccess
{
    public class EncriptacionDeUnCamino
    {
        public static string Encriptacion(string Encriptar)
        {
            string rpta = "";
            SHA256Managed sha = new SHA256Managed();
            byte[] nocifrado = Encoding.Default.GetBytes(Encriptar);
            byte[] cifrada = sha.ComputeHash(nocifrado);
            rpta = BitConverter.ToString(cifrada).Replace("-", "");
            return rpta;
        }
    }
}
