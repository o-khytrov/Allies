using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Allies
{
    [DesignTimeVisible(false)]
    public partial class GamePage : ContentPage
    {
        private int _time;
        private bool isPlaying = true;

        public int Score { get; set; }
        public string Word { get; set; }

        public Game _game { get; set; }

        private string[] words = new string[]
        {
        "Корова",
        "Собака",
        "Рыба",
        "Змея",
        "Пчела",
        "Кот",
        "Рыбалка",
        "Лимонад",
        "Корабль",
        "Воробрей",
        "Франция",
        "Мастер",
        "Работа",
        "Колбаса"
        };

        public GamePage(Game game)
        {
            _game = game;
            InitializeComponent();
            _time = _game.RoundDuration;
            time.Text = _time.ToString();
            btn_correct.ImageSource = ImageSource.FromResource(@"Allies.Images.correct.png");
            _game.NextRound();
            lblTeam.Text = _game.GetCurrentTeam().Name;
            lblPlayer.Text = _game.GetCurrentPlayer().Name;
            UpdateScore();
            SetTimer();
            MoveNext();
        }

        private void btnWrongClicked(object sender, EventArgs e)
        {
            Score--;
            if (Score < 0)
                Score = 0;

            UpdateScore();
            _game.SetResult(new Quest() { Word = Word, Result = true });
            MoveNext();
        }

        private void btnCorrectClicked(object sender, EventArgs e)
        {
            Score++;
            UpdateScore();
            _game.SetResult(new Quest() { Word = Word, Result = true });
            MoveNext();
        }

        private void MoveNext()
        {
            var rnd = new Random();
            var r = rnd.Next(0, words.Length - 1);
            Word = words[r];
            label.Text = Word;
        }

        private void UpdateScore()
        {
            score.Text = Score.ToString();
            if (_game.GameScores.Any(x => x.Value > _game.MaxScore))
            {
                isPlaying = false;
                var navPage = new NavigationPage(new GameOverPage(_game));
                Application.Current.MainPage = navPage;
            }
        }

        private void SetTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimedEvent);
        }

        private bool OnTimedEvent()
        {
            _time--;
            time.Text = _time.ToString();
            if (_time <= 0 && isPlaying)
            {
                isPlaying = false;
                var navPage = new NavigationPage(new RoundResultPage(_game));
                Application.Current.MainPage = navPage;
                return false;
            }

            return true;
        }
    }
}