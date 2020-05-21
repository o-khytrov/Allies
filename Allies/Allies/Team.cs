using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allies
{
    public class Team
    {

        public string Name { get; set; }
        public Player currentPlayer { get; set; }
        public Queue<Player> Players { get; set; }
        public Team()
        {
            Players = new Queue<Player>();
        }
        public Player GetNextPlayer()
        {
            currentPlayer = Players.Dequeue();
            Players.Enqueue(currentPlayer);
            return currentPlayer;
        }

        public string PlayerList { get { return string.Join(", ", Players.Select(x => x.Name).ToArray()); } }
    }
}
