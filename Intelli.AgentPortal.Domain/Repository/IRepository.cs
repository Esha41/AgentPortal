using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Core.Repository
{
    /// <summary>
    /// The repository for Entities.
    /// </summary>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets the Entity.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currentPage">The current page.</param>
        /// <returns>A QueryResult.</returns>
        QueryResult<TEntity> Get(string filter = null,
              string orderBy = "Id desc",
              int pageSize = 10,
              int currentPage = 1,
              params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets the Entity.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>A QueryResult.</returns>
        QueryResult<TEntity> Get(string filter = null,
              string orderBy = "Id desc",
              params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Queries the entities.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>An IQueryable.</returns>
        IQueryable<TEntity> GetQuery(
            string filter = null,
            string orderBy = "Id desc");

        /// <summary>
        /// Gets the list of all active entities.
        /// </summary>
        /// <param name="includes">The sub entities to includes.</param>
        /// <param name="orderBy">The order by clause.</param>
        /// <returns>A QueryResult.</returns>
        Task<QueryResult<TEntity>> GetAllActiveAsync(string orderBy, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets the query result based on passed parameters.
        /// </summary>
        /// <param name="query">The provided query to be used on entity.</param>
        /// <param name="filter">The filter expression.</param>
        /// <param name="includes">The inner objects that needs to be included.</param>
        /// <returns>A QueryResult.</returns>
        QueryResult<TEntity> Get(
              IQueryable<TEntity> query,
              string filter = null,
              params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Gets the query result of the specdified identity including sub entities.
        /// </summary>
        /// <param name="includes">The sub entities needs to be included.</param>
        /// <returns>A QueryResult.</returns>
        QueryResult<TEntity> Get(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Queries the.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>An IQueryable.</returns>
        IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The id of entity.</param>
        /// <returns>A ValueTask.</returns>
        ValueTask<TEntity> GetById(object id);

        /// <summary>
        /// Gets the first or default entity.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>A TEntity.</returns>
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        void Insert(TEntity entity, bool isActive = true);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="id">The id of the entity to be delete.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        void Delete(object id, bool deactivate = true);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="filter">The filter to be applied.</param>
        void Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="ids">The list of id of the entities to be deleted.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        void Delete(int[] ids, bool deactivate = true);

        /// <summary>
        /// Deletes multiple entities on the basis of supplied criteria.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <param name="deactivate">If true, deactivate.</param>
        void Delete(string filter = null, bool deactivate = true);

        /// <summary>
        /// Counts the number of rows.
        /// </summary>
        /// <returns>An int values of count of rows.</returns>
        int Count();

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        /// <returns>An ITransactionHandler.</returns>
        ITransactionHandler GetTransaction();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="userName">The user name of current logged in user.</param>
        /// <param name="transaction">The transaction handler.</param>
        void SaveChanges(string userName, ITransactionHandler transaction, string requestId = null);
    }
}
