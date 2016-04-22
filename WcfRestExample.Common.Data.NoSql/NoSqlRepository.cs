using LiteDB;
using NLog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using WcfRestExample.Common.Infrastructure;
using WcfRestExample.Common.Interfaces;

namespace WcfRestExample.Common.Data.NoSql
{
    /// <summary>
    /// Common repository used NoSql embedded database - LiteDB
    /// </summary>
    /// <typeparam name="TEnt">Entity type</typeparam>
    public class NoSqlRepository<TEnt> : IRepository<TEnt> where TEnt : new()
    {
        private INoSqlWrapper _dbWrapper;
        private ILoggerExt _logger;

        /// <summary>
        /// Constructor parameterless
        /// </summary>
        public NoSqlRepository() 
            : this(new LiteDbWrapper(), new LoggerWrapper(LogManager.GetCurrentClassLogger()))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="wrapper">Lite DB Wrapper object</param>
        public NoSqlRepository(INoSqlWrapper wrapper, ILoggerExt logger)
        {
            _dbWrapper = wrapper;
            _logger = logger;
        }

        private string _databasePath;
        /// <summary>
        /// Full database file name
        /// Getted from Web/App.config from "noSqlDb" AppSettings key
        /// </summary>
        private string DatabasePath
        {
            get
            {
                if (string.IsNullOrEmpty(_databasePath))
                {
                    string dbFile = System.Configuration.ConfigurationManager.AppSettings["noSqlDb"] ?? "WcfRestExample.db";
                    try
                    {
                        if (HttpRuntime.BinDirectory != null)
                        {
                            string dbPath = System.IO.Path.Combine(HttpRuntime.BinDirectory, "..");
                            dbFile = System.IO.Path.Combine(dbPath, dbFile);
                        }
                    }
                    catch (ArgumentNullException ex)
                    {
                    }

                    _databasePath = dbFile;
                }
                return _databasePath;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Is deleted</returns>
        public bool Delete(int id)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), id);

            bool deleted = _dbWrapper.Execute<TEnt>(DatabasePath, typeof(TEnt).Name,
                    col =>
                    {
                        return col.Delete(id);
                    });

            _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), deleted);

            return deleted;
        }

        /// <summary>
        /// Find entity
        /// </summary>
        /// <param name="predicate">Condition matching delegate</param>
        /// <returns>Entities matched by predicate</returns>
        public IEnumerable<TEnt> Find(Func<TEnt, bool> predicate)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), predicate);

            IEnumerable<TEnt> entities = _dbWrapper.Execute<TEnt>(DatabasePath, typeof(TEnt).Name,
                col =>
                {
                    List<TEnt> result = new List<TEnt>();

                    IEnumerable<TEnt> all = col.FindAll();
                    foreach (TEnt ent in all)
                    {
                        if (predicate.Invoke(ent))
                        {
                            result.Add(ent);
                        }
                    }

                    return result;
                });

            _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), entities);

            return entities;
        }

        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Entity</returns>
        public TEnt GetById(int id)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), id);

            TEnt entity = _dbWrapper.Execute<TEnt>(DatabasePath, typeof(TEnt).Name,
                col =>
                {
                    return col.FindById(id);
                });

            _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), entity);

            return entity;
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>New record ID</returns>
        public int Insert(TEnt entity)
        {
            _logger.TraceMethod(MethodBase.GetCurrentMethod(), entity);

            int id =_dbWrapper.Execute<TEnt>(DatabasePath, typeof(TEnt).Name,
                col =>
                {
                    BsonValue bsonId = col.Insert(entity);
                    return bsonId.AsInt32;
                });

            _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), id);

            return id;
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <returns>Is updated</returns>
        public bool Update(TEnt entity)
        {
            bool updated = _dbWrapper.Execute<TEnt>(DatabasePath, typeof(TEnt).Name,
                col =>
                {
                    return col.Update(entity);
                });

            _logger.TraceMethodResult(MethodBase.GetCurrentMethod(), updated);

            return updated;
        }
    }
}
