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
    public partial class SetupGamePage : ContentPage
    {
        public Game Game { get; set; }
        public SetupGamePage(Game game)
        {
            InitializeComponent();
            Game = game;
            Teams.ItemsSource = game.Teams;

            if (game.Teams.Count < 2)
            {
                btnStartGame.IsEnabled = false;
            }
            roundDuration.Text = game.RoundDuration.ToString();
        }

        private void startGameClicked(object sender, EventArgs e)
        {
            var gamePage = new GamePage(Game);
            Application.Current.MainPage = gamePage;

        }

        private void addTeamClicked(object sender, EventArgs e)
        {

            var gamePage = new SetupTeamPage(Game);
            Application.Current.MainPage = gamePage;
        }

        private void gameSettingsClicked(object sender, EventArgs e)
        {
            var settingsPage = new GameSettingsPage(Game);
            Application.Current.MainPage = settingsPage;

        }
    }
}