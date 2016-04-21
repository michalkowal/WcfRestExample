using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WcfRestExample.Common.Data.NoSql
{
    public interface ICollectionWrapper<T> where T : new()
    {
        INoSqlWrapper Database { get; }
        string Name { get; }
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        int Count(Query query);
        int Delete(Expression<Func<T, bool>> predicate);
        int Delete(Query query);
        bool Delete(BsonValue id);
        bool Drop();
        bool DropIndex(string field);
        bool EnsureIndex(string field, bool unique = false);
        bool EnsureIndex(string field, IndexOptions options);
        bool EnsureIndex<K>(Expression<Func<T, K>> property, IndexOptions options);
        bool EnsureIndex<K>(Expression<Func<T, K>> property, bool unique = false);
        bool Exists(Expression<Func<T, bool>> predicate);
        bool Exists(Query query);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue);
        IEnumerable<T> Find(Query query, int skip = 0, int limit = int.MaxValue);
        IEnumerable<T> FindAll();
        T FindById(BsonValue id);
        T FindOne(Expression<Func<T, bool>> predicate);
        T FindOne(Query query);
        IEnumerable<BsonDocument> GetIndexes();
        ICollectionWrapper<T> Include(Action<T> action);
        int Insert(IEnumerable<T> docs);
        BsonValue Insert(T document);
        int InsertBulk(IEnumerable<T> docs, int buffer = 32768);
        BsonValue Max();
        BsonValue Max(string field);
        BsonValue Max<K>(Expression<Func<T, K>> property);
        BsonValue Min();
        BsonValue Min(string field);
        BsonValue Min<K>(Expression<Func<T, K>> property);
        bool Update(T document);
        bool Update(BsonValue id, T document);
    }

    public class LiteCollectionWrapper<T> : ICollectionWrapper<T> where T : new()
    {
        private INoSqlWrapper _database;
        private LiteCollection<T> _collection;
        public LiteCollectionWrapper(LiteCollection<T> collection, INoSqlWrapper database)
        {
            _collection = collection;
            _database = database;
        }

        public INoSqlWrapper Database
        {
            get
            {
                return _database;
            }
        }

        public string Name
        {
            get
            {
                return _collection.Name;
            }
        }

        public int Count()
        {
            return _collection.Count();
        }

        public int Count(Query query)
        {
            return _collection.Count(query);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _collection.Count(predicate);
        }

        public bool Delete(BsonValue id)
        {
            return _collection.Delete(id);
        }

        public int Delete(Query query)
        {
            return _collection.Delete(query);
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            return _collection.Delete(predicate);
        }

        public bool Drop()
        {
            return _collection.Drop();
        }

        public bool DropIndex(string field)
        {
            return _collection.DropIndex(field);
        }

        public bool EnsureIndex(string field, IndexOptions options)
        {
            return _collection.EnsureIndex(field, options);
        }

        public bool EnsureIndex(string field, bool unique = false)
        {
            return _collection.EnsureIndex(field, unique);
        }

        public bool EnsureIndex<K>(Expression<Func<T, K>> property, bool unique = false)
        {
            return _collection.EnsureIndex<K>(property, unique);
        }

        public bool EnsureIndex<K>(Expression<Func<T, K>> property, IndexOptions options)
        {
            return _collection.EnsureIndex<K>(property, options);
        }

        public bool Exists(Query query)
        {
            return _collection.Exists(query);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _collection.Exists(predicate);
        }

        public IEnumerable<T> Find(Query query, int skip = 0, int limit = int.MaxValue)
        {
            return _collection.Find(query, skip, limit);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue)
        {
            return _collection.Find(predicate, skip, limit);
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.FindAll();
        }

        public T FindById(BsonValue id)
        {
            return _collection.FindById(id);
        }

        public T FindOne(Query query)
        {
            return _collection.FindOne(query);
        }

        public T FindOne(Expression<Func<T, bool>> predicate)
        {
            return _collection.FindOne(predicate);
        }

        public IEnumerable<BsonDocument> GetIndexes()
        {
            return _collection.GetIndexes();
        }

        public ICollectionWrapper<T> Include(Action<T> action)
        {
            return new LiteCollectionWrapper<T>(_collection.Include(action), Database);
        }

        public BsonValue Insert(T document)
        {
            return _collection.Insert(document);
        }

        public int Insert(IEnumerable<T> docs)
        {
            return _collection.Insert(docs);
        }

        public int InsertBulk(IEnumerable<T> docs, int buffer = 32768)
        {
            return _collection.InsertBulk(docs, buffer);
        }

        public BsonValue Max()
        {
            return _collection.Max();
        }

        public BsonValue Max(string field)
        {
            return _collection.Max(field);
        }

        public BsonValue Max<K>(Expression<Func<T, K>> property)
        {
            return _collection.Max<K>(property);
        }

        public BsonValue Min()
        {
            return _collection.Min();
        }

        public BsonValue Min(string field)
        {
            return _collection.Min(field);
        }

        public BsonValue Min<K>(Expression<Func<T, K>> property)
        {
            return _collection.Min<K>(property);
        }

        public bool Update(T document)
        {
            return _collection.Update(document);
        }

        public bool Update(BsonValue id, T document)
        {
            return _collection.Update(id, document);
        }
    }
}
