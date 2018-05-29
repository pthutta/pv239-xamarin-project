using System;
using System.Collections.Generic;
using System.Text;

namespace Triple_Eater.Services
{
    public interface ISQLiteConnectionStringFactory
    {
        string Create(string name);
    }
}
