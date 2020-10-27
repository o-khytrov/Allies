using Allies.Persistance;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        public MainPage()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await _connection.CreateTableAsync<Setting>();
            await _connection.CreateTableAsync<Player>();
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var settings = await _connection.Table<Setting>().ToListAsync();
            var roundTime = settings.FirstOrDefault(x => x.Key == "round_time");
            var maxScore = settings.FirstOrDefault(x => x.Key == "max_score");
            if (roundTime == null)
            {
                roundTime = new Setting() { Key = "round_time", Value = "60" };
                await _connection.InsertAsync(roundTime);
            }
            if (maxScore == null)
            {
                maxScore = new Setting() { Key = "max_score", Value = "50" };
                await _connection.InsertAsync(maxScore);
            }
            var game = new Game();
            game.RoundDuration = Int32.Parse(roundTime.Value);
            game.MaxScore = Int32.Parse(maxScore.Value);
            var navPage = new NavigationPage(new SetupGamePage(game));
            Application.Current.MainPage = navPage;
        }
    }
}