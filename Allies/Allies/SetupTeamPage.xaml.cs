using Allies.Persistance;
using SQLite;
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
        private Game _game;
        private Team Team;
        private ObservableCollection<Player> Players;
        private ObservableCollection<Player> ExistingPlayers;
        private SQLiteAsyncConnection _connection;
        public SetupTeamPage(Game game)
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            Players = new ObservableCollection<Player>();
            InitializeComponent();
            _game = game;
            Team = new Team();
        }

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Player>();
            var players = await _connection.Table<Player>().ToListAsync();
            var playersInGame = _game.Teams.SelectMany(x => x.Players).ToList();
            var freePlayers = players.Where(x => !playersInGame.Any(p => p.Id == x.Id)).ToList();
            ExistingPlayers = new ObservableCollection<Player>(freePlayers);
            existingPlayers.ItemsSource = ExistingPlayers;
            PlayerList.ItemsSource = Players;
            base.OnAppearing();
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
            _game.Teams.Enqueue(Team);
            var navPage = new SetupGamePage(_game);
            Application.Current.MainPage = navPage;
        }

        private void addTeamMember_Clicked(object sender, EventArgs e)
        {
            playerName.IsVisible = true;
        }

        private async void playerName_Completed(object sender, EventArgs e)
        {
            var newPlayer = new Player() { Name = playerName.Text.ToUpper() };
            playerName.Text = string.Empty;
            Players.Add(newPlayer);

            await _connection.InsertAsync(newPlayer);
        }

        private void RemovePlayer(object sender, EventArgs e)
        {
            var button = sender as Button;
            var player = button.BindingContext as Player;
            Players.Remove(player);
            ExistingPlayers.Add(player);

        }

        private async void removePlayer(object sender, EventArgs e)
        {
            // await _connection.DeleteAllAsync();
        }

        private void existingPlayerTapped(object sender, EventArgs e)
        {
            var player = ((TappedEventArgs)e).Parameter as Player;
            ExistingPlayers.Remove(player);
            Players.Add(player);
        }


        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var player = existingPlayers.SelectedItem as Player;
            ExistingPlayers.Remove(player);
            await _connection.DeleteAsync(player);
        }
    }
}