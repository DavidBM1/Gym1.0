using Gym5BLL.Implementations;
using Gym5DAL;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Gymm1._0.Clases
{
    public class FuncionesBasicas
    {
        #region Variables
        public byte[] Clave = Encoding.ASCII.GetBytes("GymAdmin159");
        public byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");
        private IConsecutivosBLL consecutivosBLL;
        #endregion

        #region Metodos
        public string ConsecutivoModifica(string Prefijo){
            consecutivosBLL = new ConsecutivosBLLImp();

            Consecutivo consecutivos = consecutivosBLL.BuscarPorId(Prefijo);
            consecutivos.valor++;
            int valorr = consecutivos.valor;
            Consecutivo consecutivos2 = new Consecutivo()
            {
                prefijo = consecutivos.prefijo,
                valor = valorr
            };
            consecutivosBLL.Modificar(consecutivos2);
            return string.Format("{0}-{1}", consecutivos.prefijo, valorr);
        }

        public string fechaConvertidor(string fecha)
        {
            string mes = fecha.Substring(0, 2);
            string dia = fecha.Substring(3, 2);
            string ano = fecha.Substring(6, 4);
            return string.Format("{0}/{1}/{2}", dia, mes, ano);
        }

        public string fechaConvertidorInv(string fecha)
        {
            string mes = fecha.Substring(0, 2);
            string dia = fecha.Substring(3, 2);
            string ano = fecha.Substring(6, 4);
            return string.Format("{0}/{1}/{2}", dia, mes, ano);
        }


        public string Encripta(string Cadena)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }



        public string Desencripta(string Cadena)
        {
            try
            {
                byte[] inputBytes = Convert.FromBase64String(Cadena);
                byte[] resultBytes = new byte[inputBytes.Length];
                string textoLimpio = String.Empty;
                RijndaelManaged cripto = new RijndaelManaged();
                using (MemoryStream ms = new MemoryStream(inputBytes))
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(objCryptoStream, true))
                        {
                            textoLimpio = sr.ReadToEnd();
                        }
                    }
                }
                return textoLimpio;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
        #endregion
    }
}