
namespace MauiApp13
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void KayitLoginEkraniGoster(object sender, EventArgs e)
        {
            if (kayitEkran.IsVisible)
            {
                kayitEkran.IsVisible = false;
                loginEkran.IsVisible = true;
            }
            else
            {
                loginEkran.IsVisible = false;
                kayitEkran.IsVisible = true;
            }
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            var user = await Models.FireBaseServices.Register(txtName.Text, txtREmail.Text, txtRPassword.Text);
            if (user != null)
            {
                // istenen sayfaya git
            }
            else
            {
                await DisplayAlert("Hata", "Kayıt başarısız", "OK");
            }
        }

        private async void LoginInClicked(object sender, EventArgs e)
        {
            //var data = NewsServices.GetNews("https://www.trthaber.com/manset_articles.rss");
            //await Console.Out.WriteLineAsync(data.Result);
            //return;

            var user = await Models.FireBaseServices.Login(txtEmail.Text, txtPassword.Text);
            if (user != null)
            {
                // istenen sayfaya git
                await DisplayAlert($"Hoşgeldin! {user.Info.DisplayName}", "Giriş başarılı", "OK");
                await Shell.Current.GoToAsync("//Haberler");
                await Shell.Current.GoToAsync("//DovizKurlari");
                await Shell.Current.GoToAsync("//Ayarlar");
                await Shell.Current.GoToAsync("//HavaDurumu");
                await Shell.Current.GoToAsync("//NotesPage");
                await Shell.Current.GoToAsync("//AnaSayfa");


            }
            else
            {
                await DisplayAlert("Hata", "Kullanıcı adı veya şifre hatalı", "OK");
            }
        }
    }

}