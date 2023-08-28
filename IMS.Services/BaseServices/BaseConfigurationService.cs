using FluentNHibernate.Data;
using IMS.BusinessModel.Dto.CommonDtos;
using IMS.BusinessModel.Entity.Common;
using IMS.Dao;
using NHibernate;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace IMS.Services.BaseServices
{
    public interface IBaseConfigurationService<IConfigurationDto, IConfigurationFormData, TEntity> where TEntity : IConfigurationEntity
                                                           
    {
        Task<IEnumerable<ConfigurationDto>> GetAll(ISession session);
        Task Create(ConfigurationFormData dtoFormData, ISession session);
        Task Update(ConfigurationFormData dtoFormData, ISession session);
        Task Delete(long entityId, ISession session);
        ConfigurationDto MapToDto(TEntity entity, ConfigurationDto dto);
        TEntity MapToEntity(ConfigurationFormData dtoForm, TEntity entity);
    }
    public class BaseConfigurationService<TDto, TDtoForm, TEntity> : IBaseConfigurationService<IConfigurationDto, IConfigurationFormData, TEntity> where TEntity :class, IConfigurationEntity
    {
        private IBaseDao<TEntity> _BaseDao = new BaseDao<TEntity>();
        private long _currentUserId = Int64.Parse(ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value);

        public async Task<IEnumerable<ConfigurationDto>> GetAll(ISession session)
        {
            try
            {
                var entities =await _BaseDao.GetAll(session);
                var result  =  new List<ConfigurationDto>();
                foreach (var t in entities)
                {
                    var dto = new ConfigurationDto();
                    result.Add(MapToDto(t, dto));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DropDownDto>> GetDropDownList(ISession session)
        {
            try
            {
                var entities =await _BaseDao.GetAll(session);
                var result = new List<DropDownDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new DropDownDto();
                    result.Add(MapToDropDownDto(entities[i], dto));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Create(ConfigurationFormData dtoFormData, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var entityType = ConfigurationEntity.CreateInstance<TEntity>();
                    var entity = MapToEntity(dtoFormData, entityType);
                    entity.Rank = GetNextRank(session);
                     await _BaseDao.Create(entity, session);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
        public async Task Update(ConfigurationFormData dtoFormData, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var entityType = ConfigurationEntity.CreateInstance<TEntity>();
                    var entity = MapToEntity(dtoFormData, entityType);
                    await _BaseDao.Update(entityType, session);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task Delete(long entityId, ISession session)
        {
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    var entity = await _BaseDao.GetById(entityId, session);
                    if (entity != null)
                    {
                       await _BaseDao.Delete(entity, session);
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        protected int GetNextRank(ISession session)
        {
            try {
                var highestRank = _BaseDao.GetHighestRank(session);
                return highestRank + 1;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ConfigurationDto MapToDto(TEntity entity, ConfigurationDto dto)
        {

            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.Status = entity.Status;
            dto.Rank = entity.Rank;
            dto.CreatedBy = entity.CreatedBy;
            dto.CreationDate = entity.CreationDate;
            dto.ModifiedBy = entity.ModifiedBy;
            dto.ModificationDate = entity.ModificationDate;
            dto.Version = entity.Version;
            dto.BusinessId = entity.BusinessId;

            return dto;
        }

        public TEntity MapToEntity(ConfigurationFormData dtoForm, TEntity entity) 
        {

            entity.Id = dtoForm.Id;
            entity.Name = dtoForm.Name;
            entity.Description = dtoForm.Description;
            entity.Status = dtoForm.Status;
            entity.Rank = dtoForm.Rank;
            entity.CreationDate = DateTime.Now;
            entity.CreatedBy = _currentUserId;
            entity.ModificationDate = DateTime.Now;
            entity.ModifiedBy = 1;
            entity.Version = 1;
            entity.BusinessId = "IMS-1";

            return entity;
        }

        public DropDownDto MapToDropDownDto(TEntity entity, DropDownDto dto)
        {
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Description = entity.Description;

            return dto;
        }
    }
}
