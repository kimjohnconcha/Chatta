using System;
using System.Threading.Tasks;
using SQLite;

namespace Data
{
    public class DataStore<TEntity> where TEntity : new()
    {
        protected readonly SQLiteAsyncConnection Connection;

        public DataStore()
        {
            var conn = new SQLiteConnection(DataLayerConstants.OfflineDbPath);
            conn.CreateTable<TEntity>();
            Connection = new SQLiteAsyncConnection(DataLayerConstants.OfflineDbPath);
        }

        public AsyncTableQuery<TEntity> Query => Connection.Table<TEntity>();

        public virtual async Task InsertAsync(TEntity record)
        {
            //Api submit successful. Insert to Db.
            await Connection.InsertAsync(record);
        }

        public virtual async Task UpdateAsync(TEntity record)
        {
            //Api submit successful. Insert to Db.
            await Connection.UpdateAsync(record);
        }

        public virtual async Task DeleteAsync(TEntity record)
        {
            await Connection.DeleteAsync(record);
        }

        public virtual async Task DeleteAllAsync()
        {
            await Connection.DropTableAsync<TEntity>();
            await Connection.CreateTableAsync<TEntity>();
        }
    }
}
