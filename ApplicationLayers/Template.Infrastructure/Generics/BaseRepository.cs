using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Core;
using Template.Domain.Exceptions;
using Template.Infrastructure.Context;

namespace Template.Infrastructure.Generics
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BaseRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<int> AddAsync(TEntity objModel)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(objModel);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
                var id = ((TBase)objModel).PrimaryKey;
                return await Task.FromResult(id);
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity objModel, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entity = await GetFirstOrDefaultAsync(predicate);
                if (entity != null)
                {
                    var id = ((TBase)entity).PrimaryKey;
                    // Actualizamos a entity con los datos de objModel
                    entity = _mapper.Map(objModel, entity);
                    ((TBase)entity).PrimaryKey = id;
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                    return await Task.FromResult(id);
                }
                else
                {
                    return await AddAsync(objModel);
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<List<int>> AddRangeAsync(IEnumerable<TEntity> listModel)
        {
            try
            {
                var IdsList = new List<int>();
                foreach (var objModel in listModel)
                {
                    var id = await AddAsync(objModel);
                    IdsList.Add(id);
                }
                return IdsList;
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<List<TEntity>?> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await Task.Run(() => _context.Set<TEntity>().AsNoTracking().Where(predicate).ToList());
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<List<TEntity>?> GetAllAsync()
        {
            try
            {
                return await Task.Run(() => _context.Set<TEntity>().AsNoTracking().ToList());
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task<int> CountAsync()
        {
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().CountAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task UpdateAsync(TEntity? objModel)
        {
            try
            {
                if (objModel != null)
                {
                    _context.Entry(objModel).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task DeleteByIdAsync(object id)
        {
            try
            {
                var entityToDelete = await _context.Set<TEntity>().FindAsync(id);
                if (entityToDelete != null)
                {
                    await DeleteAsync(entityToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task DeleteAsync(TEntity objModel)
        {
            try
            {
                if (_context.Entry(objModel).State == EntityState.Detached)
                {
                    _context.Attach(objModel);
                }
                _context.Remove(objModel);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }

        public virtual async Task DeleteWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var records = _context.Set<TEntity>().Where(predicate);
                if (records.Any())
                {
                    _context.RemoveRange(records);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException(ex.Message);
            }
        }
    }
}
