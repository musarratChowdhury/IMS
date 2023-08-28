using System;
using System.Threading.Tasks;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Entity.Common;
using IMS.Dao;
using NHibernate;

namespace IMS.Services.BaseServices
{
    public class BaseSecondaryService<TEntity> where TEntity :class, IBaseEntity
    {
        private readonly IBaseDao<TEntity> _baseDao;

        protected BaseSecondaryService()
        {
            _baseDao = new BaseDao<TEntity>();
        }

        public async Task<int> GetTotalCount(ISession session)
        {
            var result =await _baseDao.GetTotalCount(session);
            return result;
        }

        public async Task UpdateRank(ChangeRankDto changeRankDto, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    if (changeRankDto.OldRank > changeRankDto.NewRank)
                    {
                        await _baseDao.UpdateRank(sess,true, 
                            changeRankDto.OldRank, changeRankDto.NewRank, changeRankDto.Id);
                    }
                    else if(changeRankDto.OldRank < changeRankDto.NewRank)
                    {
                       await _baseDao.UpdateRank(sess,false, 
                            changeRankDto.OldRank, changeRankDto.NewRank, changeRankDto.Id);
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task Delete(long entityId, ISession sess)
        {
            using (var transaction = sess.BeginTransaction())
            {
                try
                {
                    var entity = await _baseDao.GetById(entityId, sess);
                    if (entity != null)
                    {
                       await _baseDao.Delete(entity, sess);
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> SoftDelete(long entityId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var entity = await _baseDao.GetById(entityId, session);
                    if (entity != null)
                    {
                        entity.Status = 404;
                        await _baseDao.Update(entity, session);
                        await transaction.CommitAsync();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return false;
        }

        protected int GetNextRank(ISession session)
        {
            try
            {
                var highestRank = _baseDao.GetHighestRank(session);
                return highestRank + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
