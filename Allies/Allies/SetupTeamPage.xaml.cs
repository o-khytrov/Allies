using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Allies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetupTeamPage : ContentPage
    {
        private Game Game;
        private Team Team;
        private ObservableCollection<Player> Players;

        public SetupTeamPage(Game game)
        {
            Players = new ObservableCollection<Player>();
            InitializeComponent();
            PlayersList.HeightRequest = 10;
            PlayersList.BackgroundColor = new Color(241, 248, 248);
            PlayersList.ItemsSource = Players;
            Game = game;
            Team = new Team();
        }

        private void SaveTeamClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teamName.Text))
            {
                DisplayAlert("!", "Нужно имя команды", "Ok");
                return;
            }
            Team.Name = teamName.Text;
            Team.Players = new Queue<Player>(Players.ToList());
            Game.Teams.Enqueue(Team);
            var navPage = new SetupGamePage(Game);
            Application.Current.MainPage = navPage;
        }

        private void addTeamMember_Clicked(object sender, EventArgs e)
        {
            playerName.IsVisible = true;
        }

        private void playerName_Completed(object sender, EventArgs e)
        {
            var newPlayer = new Player() { Name = playerName.Text.ToUpper() };
            playerName.Text = string.Empty;
            Players.Add(newPlayer);
            PlayersList.HeightRequest = Players.Count() * 10;
        }

        private void RemovePlayer(object sender, EventArgs e)
        {
            var button = sender as Button;
            var player = button.BindingContext as Player;
            Players.Remove(player);
            PlayersList.HeightRequest = Players.Count() * 10;
        }
    }
}