using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace DevelopmentProject.DAL
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private IDocumentStore _store;
        private IDocumentSession _session;

        public Repository(IDocumentStore store)
        {
            _store = store;
            _session = _store.OpenSession();
        }

        public T SingleOrDefault<T>(Func<T, bool> predicate)
        {
            return _session.Query<T>().SingleOrDefault(predicate);
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> criteria)
        {
            using (_session = _store.OpenSession())
            {
                return _session.Query<T>().Where(criteria).SingleOrDefault();
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria)
        {
            using (_session = _store.OpenSession())
            {
                return _session.Query<T>().Where(criteria);
            }
        }

        public IEnumerable<T> All<T>()
        {
            return _session.Query<T>();
        }

        public void Add<T>(T item)
        {
            _session.Store(item);
        }
        
        public void Delete<T>(T item)
        {
            _session.Delete(item);
        }

        public void SaveChanges()
        {
            _session.SaveChanges();
        }
    }
}
