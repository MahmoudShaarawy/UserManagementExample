using System;
using System.Collections.Generic;
using PetaPoco;

namespace UserData.Data.Repositories
{
    public interface IRepository : IDisposable
    {
        T Single<T>(object primaryKey);

        IEnumerable<T> Query<T>();
        IEnumerable<TPassType> Query<TPassType>(Sql sql);
        IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args);

        List<T> Fetch<T>();
        List<T> Fetch<T>(Sql sql);


        List<TPassType> Fetch<TPassType>(string sql, params object[] args);
        Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args);
        int Insert(object itemToAdd);
        int Update(object itemToUpdate, object primaryKeyValue);
        int Update(object itemToAdd);
        T ExecuteScalar<T>(string sql, params object[] args);

        int Delete<T>(object primaryKeyValue);
        List<T> SkipTake<T>(long skip, long take, string sql, params object[] args);

    }
}