using IMS.BusinessModel.Dto.GridData;
using IMS.BusinessModel.Entity.Common;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.DashBoard;
using IMS.BusinessModel.Entity;
using IMS.BusinessModel.Entity.Configuration;
using NHibernate.Linq;
using NHibernate.Transform;

namespace IMS.Dao
{
    public interface IBaseDao<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> GetById(long id, ISession session);
        Task<IList<TEntity>> GetAll(ISession session);
        Task<List<(int Month, decimal Total)>> GetMonthlyTotalSales(ISession session);
        Task Create(TEntity entity, ISession session);
        Task Update(TEntity entity, ISession session);
        Task Delete(TEntity entity, ISession session);
        Task<int> GetTotalCount(ISession session);
        Task<IList<TEntity>> GetAll( ISession session, DataRequest dataRequest);
        Task<IList<CategoryBasedProductCount>> GetCategoryBasedProductCount(ISession session, string sql);
        IList<TEntity> ExecuteRawSqlQuery(ISession session, string sqlQuery);
        void ExecuteRawSqlQuery(ISession session, string sqlQuery, object[] parameters, Type T);
        Task UpdateRank(ISession session, bool isPromoted, int oldRank, int newRank, long id);
        int GetHighestRank(ISession session);
    }

    public class BaseDao<TEntity> : IBaseDao<TEntity> where TEntity :class, IBaseEntity
    {
        //private readonly ISession _session;

        public BaseDao()
        {
          
        }

        public async Task<TEntity> GetById(long id, ISession session)
        {
            return await session.GetAsync<TEntity>(id);
        }

        public async  Task<IList<TEntity>> GetAll(ISession session)
        {
            return await session.Query<TEntity>().ToListAsync();
        }

        public async Task Create(TEntity entity, ISession session)
        {
            await session.SaveAsync(entity);
        }
        
        public async Task<List<(int Month, decimal Total)>> GetMonthlyTotalSales(ISession session)
        {
            try
            {
                var result = await session.Query<SalesOrder>()
                    .Where(order => !order.IsArchived)
                    .GroupBy(order => order.CreationDate.Month)
                    .Select(group => new { Month = group.Key, Total = group.Sum(order => order.TotalAmount) })
                    .ToListAsync();

                return result.Select(item => (Month: item.Month, Total: item.Total)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Update(TEntity entity, ISession session)
        {
            await session.UpdateAsync(entity);
        }
        
        public async Task Delete(TEntity entity, ISession session)
        {
            await session.DeleteAsync(entity);
            await UpdateRankOnDelete(session, entity.Rank);
        }

        public async Task<int> GetTotalCount(ISession session)
        {
            return await session.Query<TEntity>().CountAsync(x => x.Status!=404);
        }

        public IList<TEntity> GetDataBySkipTake(int skip, int take, ISession session)
        {
            return session.Query<TEntity>().Where(x=>x.Status!=404).Skip(skip).Take(take).ToList();
        }

        public async Task<IList<CategoryBasedProductCount>> GetCategoryBasedProductCount(ISession session, string sql)
        {
            return await session.CreateSQLQuery(sql)
                .AddScalar("CategoryName", NHibernateUtil.String)
                .AddScalar("NumberOfProducts", NHibernateUtil.Int32)
                .SetResultTransformer(Transformers.AliasToBean<CategoryBasedProductCount>())
                .ListAsync<CategoryBasedProductCount>();  
        }

        public async Task<IList<TEntity>> GetAll(ISession session, DataRequest dataRequest)
        {
            var criteria = session.CreateCriteria<TEntity>();
           
            // Apply filtering for Status = 404
            criteria.Add(Restrictions.Not(Restrictions.Eq("Status", 404)));
            
            // Apply search criteria if provided in dataRequest.search
            if (dataRequest.Search != null)
            {
                var searchDisjunction = Restrictions.Disjunction();
        
                foreach (var searchItem in dataRequest.Search)
                {
                    var searchDisjunction2 = Restrictions.Disjunction();
            
                    foreach (var field in searchItem.Fields)
                    {
                        switch (searchItem.Operator.ToLower())
                        {
                            case "contains":
                                searchDisjunction2.Add(Restrictions.InsensitiveLike(field, $"%{searchItem.Key}%"));
                                break;
                            // Add other cases for different operators as needed
                        }
                    }

                    searchDisjunction.Add(searchDisjunction2);
                }

                criteria.Add(searchDisjunction);
            }
            // Apply filters if provided in dataRequest.where
            if (dataRequest.Where != null)
            {
                foreach (var filterGroup in dataRequest.Where)
                {
                    var filterGroupCriterion = Restrictions.Conjunction();

                    if (filterGroup.Field!=null)
                    {
                        if (filterGroup.Value is int)
                        {
                            filterGroup.Value = Int64.Parse(filterGroup.Value.ToString());
                        }
                        switch (filterGroup.Operator.ToLower())
                        {
                            case "equal":
                                filterGroupCriterion.Add(Restrictions.Eq(filterGroup.Field, filterGroup.Value));
                                break;
                            case "contains":
                                filterGroupCriterion.Add(Restrictions.InsensitiveLike(filterGroup.Field, $"%{filterGroup.Value}%"));
                                break;
                            case "like":
                                filterGroupCriterion.Add(Restrictions.Like(filterGroup.Field, $"%{filterGroup.Value}%"));
                                break;
                            // Add other cases for different operators as needed
                        }
                    }
                    else
                    {
                        foreach (var predicate in filterGroup.Predicates)
                        {
                            if (predicate.Value != null)
                            {
                                if (predicate.Value is int)
                                {
                                   predicate.Value = Int64.Parse(predicate.Value.ToString());
                                }
                                switch (predicate.Operator.ToLower())
                                {
                                    case "equal":
                                        filterGroupCriterion.Add(Restrictions.Eq(predicate.Field, predicate.Value));
                                        break;
                                    case "contains":
                                        filterGroupCriterion.Add(Restrictions.InsensitiveLike(predicate.Field, $"%{predicate.Value}%"));
                                        break;
                                    case "like":
                                        filterGroupCriterion.Add(Restrictions.Like(predicate.Field, $"%{predicate.Value}%"));
                                        break;
                                    // Add other cases for different operators as needed
                                }
                            }
                        }
                    }
            

                    criteria.Add(filterGroupCriterion);
                }
            }
            if (dataRequest.Sorted!=null)
            {
                foreach (var sortInfo in dataRequest.Sorted)
                {
                    if (sortInfo.Direction.Equals("ascending", StringComparison.OrdinalIgnoreCase))
                    {
                        criteria.AddOrder(Order.Asc(sortInfo.Name));
                    }
                    else if (sortInfo.Direction.Equals("descending", StringComparison.OrdinalIgnoreCase))
                    {
                        criteria.AddOrder(Order.Desc(sortInfo.Name));
                    }
                }
            }
            

            criteria.SetFirstResult(dataRequest.Skip)
                    .SetMaxResults(dataRequest.Take);

            return await criteria.ListAsync<TEntity>();
        }
        
        public IList<TEntity> ExecuteRawSqlQuery(ISession session, string sqlQuery)
        {
            IQuery query = session.CreateSQLQuery(sqlQuery).AddEntity(typeof(TEntity));

            return query.List<TEntity>();
        }
        
        public void ExecuteRawSqlQuery(ISession session, string sqlQuery, object[] parameters, Type T)
        {
            IQuery query = session.CreateSQLQuery(sqlQuery).AddEntity(T);

            for (int i = 0; i < parameters.Length; i++)
            {
                query.SetParameter(i + 1, parameters[i]);
            }
        }

        public async Task UpdateRank(ISession session,bool isPromoted, int oldRank, int newRank, long id)
        {
            var  sql = isPromoted ? $@"
                UPDATE {typeof(TEntity).Name}
                SET Rank = Rank + 1
                WHERE Rank >= {newRank} AND Rank < {oldRank};
                UPDATE {typeof(TEntity).Name}
                SET Rank = {newRank}
                WHERE Id = {id}
            " : $@"
                UPDATE {typeof(TEntity).Name}
                SET Rank = Rank - 1
                WHERE Rank <= {newRank} AND Rank > {oldRank};
                UPDATE {typeof(TEntity).Name}
                SET Rank = {newRank}
                WHERE Id = {id}
            ";
            IQuery query = session.CreateSQLQuery(sql);
            
            await query.ExecuteUpdateAsync();
        }

        private async Task UpdateRankOnDelete(ISession session, int deletedRank)
        {
            var sql = $"UPDATE {typeof(TEntity).Name} SET Rank = Rank - 1 WHERE Rank > {deletedRank}";
            IQuery query = session.CreateSQLQuery(sql);
            await query.ExecuteUpdateAsync();
        }


        public int GetHighestRank(ISession session)
        {
            if(session.Query<TEntity>().ToList().Count!=0)
            {
                return session.Query<TEntity>()
                              .Select(b => b.Rank)
                              .Max();
            }
            else
            {
                return 0;
            }
        }

    }

}
