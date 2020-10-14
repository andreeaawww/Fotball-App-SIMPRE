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
    public partial class ConvertorPage : ContentPage
    {
        List<Curs> listaCurs = new List<Curs>();
        List<string> valute = new List<string>();
        public ConvertorPage()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            DAOcurs daoCurs = new DAOcurs();
          //  listaCurs = daoCurs.ObtineCursDinData(DateTime.Now.ToString("yyyy-MM-dd"));
            listaCurs = await NetUtil.PreiaCurs();
            listaCurs.Insert(0, new Curs { Valuta = "RON", Valoare = 1 });
            foreach(Curs curs in listaCurs)
            {
                valute.Add(curs.Valuta);
            }

            PickerValutaSursa.ItemsSource = valute;
            PickerValutaSursa.SelectedIndex = 0;
            PickerValoareDestinatie.ItemsSource = valute;
            PickerValoareDestinatie.SelectedIndex = 0;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            //convertim valoarea din entry la double
            //efectuam conversia calcule raportandu ne la RON
            //initializam proprietatea text pentru entryRezultat
            //lista nu include RON treb sa adaugam curs cu RON (1 RON = 1 RON)
            //initializate ob de tip picker cu valutele din listaCurs

            double sumaDeConvertit = 0;
            if (EntrySumaDeConvertit.Text != null)
            {
                 sumaDeConvertit = double.Parse(EntrySumaDeConvertit.Text);
            }

            string valutaSursa = PickerValutaSursa.SelectedItem as string;
            string valutaDestinatie = PickerValoareDestinatie.SelectedItem as string;
            Curs cursSursa = null;
            Curs cursDestinatie = null;

            foreach (Curs curs in listaCurs)
            {
                if (curs.Valuta.Equals(valutaSursa))
                {
                    cursSursa = curs;
                }
                if (curs.Valuta.Equals(valutaDestinatie))
                {
                    cursDestinatie = curs;
                }
            }

            double rezultatConversie = 
            (sumaDeConvertit*cursSursa.Multiplicator*cursSursa.Valoare)/
            (cursDestinatie.Multiplicator * cursDestinatie.Valoare);

            EntryRezultatConversie.Text = rezultatConversie.ToString();

            
        }
    }
}