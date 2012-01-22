using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DevelopmentProject.DAL
{
    public interface IRepository<T>
    {
        T SingleOrDefault<T>(Func<T, bool> predicate);
        T SingleOrDefault<T>(Expression<Func<T, bool>> criteria);
        IEnumerable<T> Find(Expression<Func<T, bool>> criteria);
        IEnumerable<T> All<T>();
        void Delete<T>(T item);
        void Add<T>(T item);
        void SaveChanges();

    }
}
