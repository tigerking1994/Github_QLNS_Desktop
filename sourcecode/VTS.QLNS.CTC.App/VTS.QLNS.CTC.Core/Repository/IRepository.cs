using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IEnumerable<TEntity> FindAll();

        /// <summary>
        /// Find object by Expression.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="t">Specified a new object to create.</param>
        int Add(TEntity t);

        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="t">Specified a existing object to delete.</param>        
        int Delete(TEntity t);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="id">Specified a existing id object to delete.</param>        
        int Delete(Guid id);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        int Update(TEntity t);

        /// <summary>
        /// Update objects changes and save to database.
        /// </summary>
        /// <param name="entities">Specified the object to save.</param>
        int AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        int UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        int AddOrUpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Bulk insert lots data
        /// </summary>
        /// <param name="entities"></param>
        void BulkInsert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Bulk update lots data
        /// </summary>
        /// <param name="entities"></param>
        void BulkUpdate(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        int AddOrUpdate(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        int RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
