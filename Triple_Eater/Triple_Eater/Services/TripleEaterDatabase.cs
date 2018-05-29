using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Triple_Eater.DataModels;

namespace Triple_Eater.Services
{
    public class TripleEaterDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public TripleEaterDatabase(string name, ISQLiteConnectionStringFactory connectionFactory)
        {
            _db = new SQLiteAsyncConnection(connectionFactory.Create(name));
        }

        public async Task InitDatabase()
        {
            await _db.CreateTableAsync<Player>();
        }

        public async Task<IList<Player>> TryGetAllPlayersAsync()
        {
            try
            {
                return await _db.Table<Player>().ToListAsync();
            }
            catch
            {
                return new List<Player>();
            }
        }

        public async Task<bool> TryAddPlayerAsync(Player player)
        {
            try
            {
                await _db.InsertAsync(player);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> TryDeletePlayerAsync(Player player)
        {
            try
            {
                await _db.DeleteAsync(player);
                return true;
            }
            catch
            {
                return false;
            }
        }
            
        public async Task<bool> TryUpdatePlayerAsync(Player player)
        {
            try
            {
                await _db.UpdateAsync(player);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
