using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace Allies.Persistance
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
