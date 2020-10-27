using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoundResultPage : ContentPage
    {
        private Game _game;

        public RoundResultPage(Game game)
        {
            InitializeComponent();
            _game = game;
            var round = game.Rounds.Last();
            var team = round.Team;
            lblTeam.Text = round.Team.Name;
            lblPlayer.Text = round.Player.Name;
            var nextTeam = game.Teams.Peek();
            var nextPlayer = nextTeam.Players.Peek();
            lblNextPlayer.Text = $"Следующий ход: команда {nextTeam.Name} {nextPlayer.Name}";
            //results.ItemsSource = round.Quests;
            var total = game.GameScores;
            totals.ItemsSource = total;
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            var navPage = new GamePage(_game);
            Application.Current.MainPage = navPage;
        }
    }
}