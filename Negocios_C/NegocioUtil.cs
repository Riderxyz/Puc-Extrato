using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios_C
{
    public class NegocioUtil
    {
        public string decripto(string texto)
        {
            var result = "";
            var arr = Encoding.ASCII.GetBytes(texto);
            Int64 i = 0,  y = 0, r = 0;
            var n = DateTime.Now.Minute; 
            for (i = 0; i < arr.Length; i++)
            {
                var x = arr[i];

                if (i + 1 > arr.Length - 1)
                    y = arr[0];

                else
                    y = arr[i + 1];
                r = Convert.ToInt64(Math.Pow(x + y, i + 1));
                r = r * n;
                result = result + r.ToString();
            }
            return result;
        }
    }
}
