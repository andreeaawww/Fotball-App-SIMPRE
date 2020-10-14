using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CursValutar
{
    public partial class MainPage : ContentPage
    {

        List<Curs> listaCurs = new List<Curs>();
        public MainPage()
        {
            InitializeComponent();
            // listaCurs.Add(new Curs() { Valuta = "EUR", Valoare = 4.86, Data = "2020-10-08" });
            //  listaCurs.Add(new Curs() { Valuta = "USD", Valoare = 4.14, Data = "2020-10-08" });
            //  listaCurs.Add(new Curs() { Valuta = "JPY", Valoare = 3.91, Data = "2020-10-08", Multiplicator = 100 });

        //    listaCurs = await NetUtil.PreiaCurs();

         //   listViewCurs.ItemsSource = listaCurs;
        }

        protected override async void OnAppearing()
        {

            DAOcurs daoCurs = new DAOcurs();//aici se creaza baza de date
            listaCurs = daoCurs.ObtineCursDinData(Utils.CalculeazaDataDeReferinta(DateTime.Now));//o fct care primind o data preia si ora si determina care este ziua corespunzatoare acelui curs. ex: daca ii dam data de luni(12 oct) fct sa returneze ca data de referinta data de azi (vineri 9 oct)
            if (listaCurs.Count == 0)
            {
                listaCurs = await NetUtil.PreiaCurs();//luam datele de pe net
                foreach(Curs curs in listaCurs)
                {
                    daoCurs.AdaugaCurs(curs);//il adaugam in baza de date
                }
            }


            
            if(listaCurs.Count != 0)
              BindingContext = listaCurs[0];

            listViewCurs.ItemsSource = listaCurs;

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
             Shell.Current.GoToAsync("Setari");
          //  Navigation.PushAsync(new Setari()); daca nu avem shell
        }

    }
}
