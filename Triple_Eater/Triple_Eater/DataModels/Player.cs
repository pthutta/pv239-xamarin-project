using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Triple_Eater.DataModels
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Role OriginalRole { get; set; }

        public Role CurrentRole { get; set; }

        public Operation Operation { get; set; }

        public int AccusationCounter { get; set; }

        // Reset to false after every finished group of actions over players
        public bool WasProcessed { get; set; }

    }
}
