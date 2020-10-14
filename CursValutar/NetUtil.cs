using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CursValutar
{
    class NetUtil
    {

        private static List<Curs> PrelucreazaXML(Stream stream)
        {
            List<Curs> listaCurs = new List<Curs>();
            XmlReader reader = XmlReader.Create(stream);
            string data = "";
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name.Equals("PublishingDate"))
                    {
                        reader.Read();
                        data = reader.Value;
                    }

                    if (reader.Name.Equals("Rate"))
                    {
                        Curs curs = new Curs();
                        curs.Valuta = reader["currency"];
                        
                        if(reader["multiplier"] != null)
                        {
                            curs.Multiplicator = Int32.Parse(reader["multiplier"]);
                        }

                        reader.Read();
                        curs.Valoare = double.Parse(reader.Value);
                        curs.Data = data;

                        listaCurs.Add(curs);
                    }
                }
            }


            return listaCurs;
        }
        public static async Task<List<Curs>> PreiaCurs()
        {
            HttpClient httpClient = new HttpClient();
            Stream stream = await httpClient.GetStreamAsync("https://bnr.ro/nbrfxrates.xml");
            return PrelucreazaXML(stream);
        }
    }
}
