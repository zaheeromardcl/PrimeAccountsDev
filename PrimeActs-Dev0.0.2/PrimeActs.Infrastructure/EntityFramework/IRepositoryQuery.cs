﻿namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IRepositoryQuery<TEntity> where TEntity : class
    {
        //RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        //RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        //RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression);
        //IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount);
        //IQueryable<TEntity> Get();
        //Task<IEnumerable<TEntity>> GetAsync();
        //IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}