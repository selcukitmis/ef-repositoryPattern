using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

using System.Data.SqlClient;

namespace RepositoryPattern
{

    public class Repository<T>
        where T : DbContext, new()
    {
        private readonly T _context;
        private readonly Context context;

        public Repository(T currentContext)
        {
            _context = currentContext;
            context = new Context();
        }

        public void Save<E>(E entity)
            where E : class, new()
        {
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {
                _context.Set<E>().Add(entity);
                _context.SaveChanges();
                dbContextTransaction.Commit();
                dbContextTransaction.Dispose();
            }
            catch
            {
                dbContextTransaction.Rollback();
            }
        }

        public void Update<E>(E entity)
            where E : class, new()
        {
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {
                _context.Set<E>();
                _context.SaveChanges();
                dbContextTransaction.Commit();
                dbContextTransaction.Dispose();
            }
            catch (DbEntityValidationException)
            {
                dbContextTransaction.Rollback();
            }
        }

        public void Delete<E>(E entity)
            where E : class, new()
        {
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {
                _context.Set<E>().Remove(entity);
                _context.SaveChanges();
                dbContextTransaction.Commit();
                dbContextTransaction.Dispose();
            }
            catch (DbEntityValidationException)
            {
                dbContextTransaction.Rollback();
            }
        }


        public E Find<E>(Expression<Func<E, bool>> expression)
            where E : class, new()
        {
            return _context.Set<E>().Where(expression).FirstOrDefault();
        }

        public IEnumerable<E> Filter<E>(Expression<Func<E, bool>> expression)
            where E : class, new()
        {
            return _context.Set<E>().Where(expression).AsEnumerable();
        }
    }
}
