using System.Collections.Generic;
using System.Linq;

namespace Allies
{
    public class Game
    {
        private Player currentPlayer;
        private Team currentTeam;

        public Queue<Team> Teams { get; set; }
        public List<Round> Rounds { get; set; }
        public int RoundDuration { get; set; }
        public int MaxScore { get; set; }

        public Game()
        {
            Teams = new Queue<Team>();
            Rounds = new List<Round>();
            RoundDuration = 0;
        }

        private void GetNextTeam()
        {
            currentTeam = Teams.Dequeue();
            Teams.Enqueue(currentTeam);
        }

        private void GetNextPlayer()
        {
            currentPlayer = currentTeam.GetNextPlayer();
        }

        public void NextRound()
        {
            GetNextTeam();
            GetNextPlayer();
            var round = new Round() { Player = currentPlayer, Team = currentTeam };
            Rounds.Add(round);
        }

        public void SetResult(Quest quest)
        {
            Rounds.Last().Add(quest);
        }

        public Team GetCurrentTeam()
        {
            return currentTeam;
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public Dictionary<string, int> GameScores
        {
            get
            {
                return Rounds.GroupBy(x => x.Team.Name).ToDictionary(x => x.Key, x => x.SelectMany(z => z.Quests).Where(z => z.Result).Count());
            }
        }
    }
}