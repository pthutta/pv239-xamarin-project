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

        public Guid ActionId { get; set; }

        public int AccusationCounter { get; set; }

    }
}
