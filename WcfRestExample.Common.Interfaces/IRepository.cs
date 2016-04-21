using System;
using System.Collections.Generic;

namespace WcfRestExample.Common.Interfaces
{
    /// <summary>
    /// Common DB Entities repository interface
    /// </summary>
    /// <typeparam name="TRepo">Entity type</typeparam>
    public interface IRepository<TRepo> where TRepo : new()
    {
        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Entity</returns>
        TRepo GetById(int id);
        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>New record ID</returns>
        int Insert(TRepo entity);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>Is updated</returns>
        bool Update(TRepo entity);
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Is deleted</returns>
        bool Delete(int id);

        /// <summary>
        /// Find entity
        /// </summary>
        /// <param name="predicate">Condition matching delegate</param>
        /// <returns>Entities matched by predicate</returns>
        IEnumerable<TRepo> Find(Func<TRepo, bool> predicated);
    }
}
