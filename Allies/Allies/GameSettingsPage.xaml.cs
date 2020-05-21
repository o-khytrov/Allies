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
    public partial class GameSettingsPage : ContentPage
    {
        int StepValue = 10;
        public Game Game { get; set; }
        public GameSettingsPage(Game game)
        {
            InitializeComponent();
            
            Game = game;
            slider.Value = game.RoundDuration;
        }

        private void saveSettingsClicked(object sender, EventArgs e)
        {
            var navPage = new SetupGamePage(Game);
            Application.Current.MainPage = navPage;

        }

        private void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / StepValue);

            slider.Value = newStep * StepValue;
            Game.RoundDuration = (int)slider.Value;

        }
    }
}