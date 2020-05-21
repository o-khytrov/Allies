using System;
using System.Collections.Generic;
using System.Text;

namespace Allies
{
    public class Round
    {
        public List<Quest> Quests { get; set; }
        
        public Team Team { get; set; }
        public Player Player { get; set; }

        public Round()
        {
            Quests = new List<Quest>();
        }

        public void Add(Quest quest)
        {
            Quests.Add(quest);
        }

    }
}
