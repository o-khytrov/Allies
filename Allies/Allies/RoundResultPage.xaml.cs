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
    public partial class RoundResultPage : ContentPage
    {
        Game _game;
        public RoundResultPage(Game game)
        {
            InitializeComponent();
            _game = game;
            var round = game.Rounds.Last();
            var team = round.Team;
            lblTeam.Text = round.Team.Name;
            lblPlayer.Text = round.Player.Name;
            results.ItemsSource = round.Quests;
            var to = game.Rounds.GroupBy(x => x.Team.Name).ToDictionary(x => x.Key, x => x.SelectMany(z => z.Quests).Where(z => z.Result).Count());
            totals.ItemsSource = to;
        }

        private void btnContinue_Clicked(object sender, EventArgs e)
        {
            var navPage = new GamePage(_game);
            Application.Current.MainPage = navPage;

        }
    }
}