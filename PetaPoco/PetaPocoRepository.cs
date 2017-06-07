using System;
using System.Collections.Generic;
using PetaPoco.Internal;
using UserData.Data.Repositories;

namespace PetaPoco
{
    public class PetaPocoRepository : IRepository
    {
        public Database Db
        {
            get { return db; }
        }

        private Database db;
        public PetaPocoRepository()
        {
            db = new Database("RBmembership");
        }
        public TPassType Single<TPassType>(object primaryKey)
        {
            return db.Single<TPassType>(primaryKey);
        }
        public IEnumerable<TPassType> Query<TPassType>()
        {
            var pd = PocoData.ForType(typeof(TPassType));
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return db.Query<TPassType>(sql);
        }

        public IEnumerable<TPassType> Query<TPassType>(Sql sql)
        {
            return this.db.Query<TPassType>(sql);
        }

        public IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args)
        {
            return db.Query<TPassType>(sql, args);
        }
        public List<TPassType> Fetch<TPassType>()
        {
            var pd = PocoData.ForType(typeof(TPassType));
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return db.Fetch<TPassType>(sql);
        }

        public List<T> Fetch<T>(Sql sql)
        {
            return db.Fetch<T>(sql);
        }

        public List<TPassType> Fetch<TPassType>(string sql, params object[] args)
        {
            return db.Fetch<TPassType>(sql, args);
        }
        public Page<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            return db.Page<TPassType>(pageNumber, itemsPerPage, sql, args) as Page<TPassType>;
        }
        public Page<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, Sql sql)
        {
            return db.Page<TPassType>(pageNumber, itemsPerPage, sql) as Page<TPassType>;
        }
        public int Insert(object poco)
        {
            return Convert.ToInt32(db.Insert(poco));
        }
        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return Convert.ToInt32(db.Insert(tableName, primaryKeyName, autoIncrement, poco));
        }
        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return Convert.ToInt32(db.Insert(tableName, primaryKeyName, poco));
        }
        public int Update(object poco)
        {
            return db.Update(poco);
        }
        public int Update(object poco, object primaryKeyValue)
        {
            return db.Update(poco, primaryKeyValue);
        }
        public int Update(string tableName, string primaryKeyName, object poco)
        {
            return db.Update(tableName, primaryKeyName, poco);
        }
        public int Update(object poco, IEnumerable<string> columns)
        {
            return db.Update(poco, columns);
        }
        public int Delete<TPassType>(object pocoOrPrimaryKey)
        {
            return db.Delete<TPassType>(pocoOrPrimaryKey);
        }

        public List<T> SkipTake<T>(long skip, long take, string sql, params object[] args)
        {
            return db.SkipTake<T>(skip, take, sql, args);
        }

        public T ExecuteScalar<T>(string sql, params object[] args)
        {
            return db.ExecuteScalar<T>(sql, args);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}