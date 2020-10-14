using System;
using System.Collections.Generic;
using System.Text;

namespace CursValutar
{
    class Utils
    {
        public static string CalculeazaDataDeReferinta(DateTime data)
        {

            DateTime dataReferitna = data;

            //de facut acasa
            return dataReferitna.ToString("yyyy-MM-dd");
        }
    }
}
