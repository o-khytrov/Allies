using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Allies
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string Name { get; set; }

        public int Score { get; set; }
    }
}
