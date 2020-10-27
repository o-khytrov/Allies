using Allies.Persistance;
using SQLite;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameSettingsPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private Game Game { get; set; }
        private Setting roundTime;
        private Setting maxScore;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var settings = await _connection.Table<Setting>().ToListAsync();
            roundTime = settings.FirstOrDefault(x => x.Key == "round_time");
            maxScore = settings.FirstOrDefault(x => x.Key == "max_score");
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
            Game.RoundDuration = Int32.Parse(roundTime.Value);
            Game.MaxScore = Int32.Parse(maxScore.Value);
            timeRoundSlider.Value = Game.RoundDuration;
            gameScoreSlider.Value = Game.MaxScore;
        }

        private int StepValue = 10;

        public GameSettingsPage(Game game)
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            InitializeComponent();

            Game = game;
        }

        private async void saveSettingsClicked(object sender, EventArgs e)
        {
            await _connection.UpdateAsync(roundTime);
            await _connection.UpdateAsync(maxScore);
            var navPage = new SetupGamePage(Game);
            Application.Current.MainPage = navPage;
        }

        private void timeRoundSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);
            timeRoundSlider.Value = newStep * StepValue;

            if (roundTime != null)
            {
                roundTime.Value = timeRoundSlider.Value.ToString();
            }
            Game.RoundDuration = (int)timeRoundSlider.Value;
        }

        private void gameScoreSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);
            gameScoreSlider.Value = newStep * StepValue;

            if (maxScore != null)
            {
                maxScore.Value = timeRoundSlider.Value.ToString();
            }
            Game.MaxScore = (int)gameScoreSlider.Value;
        }
    }
}