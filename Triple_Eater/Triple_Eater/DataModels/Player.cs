using System;
using System.Collections.Generic;
using System.Text;

namespace Triple_Eater.DataModels
{
    public class Player
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Role OriginalRole { get; set; }

        public Role CurrentRole { get; set; }

        public IAction Action { get; set; }

    }
}
