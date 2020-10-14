using Microcharts;
using Microcharts.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CursValutar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Istoric : ContentPage
    {
        List<Curs> listaCurs;
        List<ChartEntry> valoriGrafic = new List<ChartEntry>();
        public Istoric()
        {
            InitializeComponent();
            DAOcurs daoCurs = new DAOcurs();
            listaCurs = daoCurs.ObtineCursDinData(DateTime.Now.ToString("yyyy-MM-dd"));
            // listaCurs = daoCurs.ObtineCurs("EUR"); valoare se ia dintr un picker

            foreach(Curs curs in listaCurs)
            {

                if (curs.Valuta.Equals("XAU"))
                    continue;
                ChartEntry chartEntry = new ChartEntry((float)curs.Valoare)
                {
                    Color = new SkiaSharp.SKColor(120, 0, 120),
                    Label = curs.Data,
                    ValueLabel = curs.Valoare.ToString()
                };
                valoriGrafic.Add(chartEntry);
            }

            chartViewIstoric.Chart = new PointChart() { Entries = valoriGrafic };

        }
    }
}