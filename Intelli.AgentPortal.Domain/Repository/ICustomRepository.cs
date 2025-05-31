using Intelli.AgentPortal.Domain.Core.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Intelli.AgentPortal.Domain.Repository
{
    /// <summary>
    /// The repository for Entities.
    /// </summary>
    public interface ICustomRepository<TEntity>
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
                string orderBy = null,
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
        void Insert(TEntity entity);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Counts the number of rows.
        /// </summary>
        /// <returns>An int values of count of rows.</returns>
        int Count();


        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="userName">The user name of current logged in user.</param>
        /// <param name="transaction">The transaction handler.</param>
        void SaveChanges(string userName, string requestId = null);
        void SaveChanges(string userName, ITransactionHandler transaction, string requestId = null);
        ITransactionHandler GetTransaction();
    }
}
