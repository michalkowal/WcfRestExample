using LiteDB;
using System;
using System.Collections.Generic;

namespace WcfRestExample.Common.Data.NoSql
{
    /// <summary>
    /// Interface described NoSql database wrapper
    /// </summary>
    public interface INoSqlWrapper
    {
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="action">Delegate with action on collection</param>
        void Execute<TEnt>(string databasePath, string collectionName, Action<ICollectionWrapper<TEnt>> action) where TEnt : new();
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return entity prepared by func delegate</returns>
        TEnt Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, TEnt> func) where TEnt : new();
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return entities collection prepared by func delegate</returns>
        IEnumerable<TEnt> Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, IEnumerable<TEnt>> func) where TEnt : new();
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return id value prepared by func delegate</returns>
        int Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, int> func) where TEnt : new();
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return flag is entity deleted/updated</returns>
        bool Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, bool> func) where TEnt : new();

    }

    /// <summary>
    /// LiteDBWrapper used by NoSqlRespository
    /// </summary>
    public class LiteDbWrapper : INoSqlWrapper
    {
        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="action">Delegate with action on collection</param>
        public void Execute<TEnt>(string databasePath, string collectionName, Action<ICollectionWrapper<TEnt>> action) where TEnt : new()
        {
            using (LiteDatabase db = new LiteDatabase(databasePath))
            {
                LiteCollection<TEnt> col = db.GetCollection<TEnt>(collectionName);
                action.Invoke(new LiteCollectionWrapper<TEnt>(col, this));
            }
        }

        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return entity prepared by func delegate</returns>
        public TEnt Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, TEnt> func) where TEnt : new()
        {
            using (LiteDatabase db = new LiteDatabase(databasePath))
            {
                LiteCollection<TEnt> col = db.GetCollection<TEnt>(collectionName);

                return func.Invoke(new LiteCollectionWrapper<TEnt>(col, this));
            }
        }

        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return entities collection prepared by func delegate</returns>
        public IEnumerable<TEnt> Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, IEnumerable<TEnt>> func) where TEnt : new()
        {
            using (LiteDatabase db = new LiteDatabase(databasePath))
            {
                LiteCollection<TEnt> col = db.GetCollection<TEnt>(collectionName);

                return func.Invoke(new LiteCollectionWrapper<TEnt>(col, this));
            }
        }

        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return id value prepared by func delegate</returns>
        public int Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, int> func) where TEnt : new()
        {
            using (LiteDatabase db = new LiteDatabase(databasePath))
            {
                LiteCollection<TEnt> col = db.GetCollection<TEnt>(collectionName);

                return func.Invoke(new LiteCollectionWrapper<TEnt>(col, this));
            }
        }

        /// <summary>
        /// Open database and get Entitites collection
        /// </summary>
        /// <typeparam name="TEnt">Entity type</typeparam>
        /// <param name="databasePath">File name of database</param>
        /// <param name="collectionName">Name of entities collection</param>
        /// <param name="func">Delegate with action on collection</param>
        /// <returns>Return flag is entity deleted/updated</returns>
        public bool Execute<TEnt>(string databasePath, string collectionName, Func<ICollectionWrapper<TEnt>, bool> func) where TEnt : new()
        {
            using (LiteDatabase db = new LiteDatabase(databasePath))
            {
                LiteCollection<TEnt> col = db.GetCollection<TEnt>(collectionName);

                return func.Invoke(new LiteCollectionWrapper<TEnt>(col, this));
            }
        }
    }
}
