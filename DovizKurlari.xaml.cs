using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace MauiApp13
{
    public partial class DovizKurlari : ContentPage
    {
        public DovizKurlari()
        {
            InitializeComponent();
        }

        private static DovizKurlari instance;
        public static DovizKurlari Page
        {
            get
            {
                if (instance == null)
                    instance = new DovizKurlari();
                return instance;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Load();
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            await Load();
        }

        AltinDoviz kurlar;
        async Task Load()
        {
            string jsondata = await GetAltinDovizGuncelKurlar();
            kurlar = JsonConvert.DeserializeObject<AltinDoviz>(jsondata);

            dovizliste.ItemsSource = new List<KurItem>()
            {
                new KurItem()
                {
                    Doviz = "Dolar",
                    Alis = kurlar.USD.Alis,
                    Satis = kurlar.USD.Satis,
                    Fark = kurlar.USD.Degisim,
                    Yon = GetImage(kurlar.USD.Degisim),
                },
                new KurItem()
                {
                    Doviz = "Euro",
                    Alis = kurlar.EUR.Alis,
                    Satis = kurlar.EUR.Satis,
                    Fark = kurlar.EUR.Degisim,
                    Yon = GetImage(kurlar.EUR.Degisim),
                },
                new KurItem()
                {
                    Doviz = "Gram Altýn",
                    Alis = kurlar.Gram_Altin.Alis,
                    Satis = kurlar.Gram_Altin.Satis,
                    Fark = kurlar.Gram_Altin.Degisim,
                    Yon = GetImage(kurlar.Gram_Altin.Degisim),
                },
            };
        }

        private string GetImage(string str)
        {
            if (str.Contains('-'))
                return "down.png";
            if (str.Equals("%0,00"))
                return "minus.png";

            return "up.png";
        }

        private async Task<string> GetAltinDovizGuncelKurlar()
        {
            string url = "https://finans.truncgil.com/today.json";
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    public class AltinDoviz
    {
        public string Update_Date { get; set; }
        public Currency USD { get; set; }
        public Currency EUR { get; set; }
        // Add other currencies as needed
        [JsonProperty("gram-altin")]
        public GoldCurrency Gram_Altin { get; set; }
    }

    public class Currency
    {
        [JsonProperty("Alýþ")]
        public string Alis { get; set; }
        [JsonProperty("Tür")]
        public string Tur { get; set; }
        [JsonProperty("Satýþ")]
        public string Satis { get; set; }
        [JsonProperty("Deðiþim")]
        public string Degisim { get; set; }
    }

    public class GoldCurrency : Currency
    {
        // Inherits properties from Currency
    }

    public class KurItem
    {
        public string Doviz { get; set; }
        public string Alis { get; set; }
        public string Satis { get; set; }
        public string Fark { get; set; }
        public string Yon { get; set; }
    }
}
