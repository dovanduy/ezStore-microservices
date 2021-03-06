﻿using Microservices.DataAccess.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Ws4vn.Microservices.ApplicationCore.Interfaces;
using Ws4vn.Microservices.ApplicationCore.SharedKernel;

namespace Ws4vn.Microservices.Infrastructure.Sql
{
    public class BaseModelRepository<TModel> : IDataAccessWriteRepository<TModel> where TModel : class
    {
        protected readonly DbContext _context;
        internal DbSet<TModel> _dbSet;

        public BaseModelRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TModel>();
        }

        public virtual IQueryable<TModel> Get(Expression<Func<TModel, bool>> filter,
             Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy,
             string includeProperties = "", bool isIncludedIsDeleted = true)
        {
            IQueryable<TModel> query = _dbSet;

            if (typeof(TModel).GetProperty("Deleted") != null && isIncludedIsDeleted)
            {
                var param = Expression.Parameter(typeof(TModel), "x");
                var body = Expression.NotEqual(Expression.Property(param, "Deleted"),
                    Expression.Convert(Expression.Constant(true), typeof(bool)));
                var isDeletedFilter = Expression.Lambda<Func<TModel, bool>>(body, param);
                query = query.Where(isDeletedFilter);
            }

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        public virtual IQueryable<TModel> Get(Expression<Func<TModel, bool>> filter = null,
             string orderBy = null, bool orderAsc = true,
             string includeProperties = "", bool isIncludedIsDeleted = true)
        {
            string methodName = "";
            if (orderAsc)
            {
                methodName = "OrderBy";
            }
            else
            {
                methodName = "OrderByDescending";
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                var orderQuery = _dbSet.ApplyOrder(orderBy, methodName);
                IOrderedQueryable<TModel> orderByFunc(IQueryable<TModel> i) => orderQuery;
                return Get(filter, orderByFunc, includeProperties, isIncludedIsDeleted);
            }
            return Get(filter, null, includeProperties, isIncludedIsDeleted);
        }

        public PagedResult<TModel> GetPaged(Expression<Func<TModel, bool>> filter,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy,
            string includeProperties = "", bool isIncludedIsDeleted = true,
            int page = 1, int pageSize = 20)
        {
            IQueryable<TModel> query = Get(filter, orderBy, includeProperties, isIncludedIsDeleted);
            var result = new PagedResult<TModel>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public PagedResult<TModel> GetPaged(Expression<Func<TModel, bool>> filter = null,
            string orderBy = null, bool orderAsc = true,
            string includeProperties = "", bool isIncludedIsDeleted = true,
            int page = 1, int pageSize = 20)
        {
            string methodName = "";
            if (orderAsc)
            {
                methodName = "OrderBy";
            }
            else
            {
                methodName = "OrderByDescending";
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                var orderQuery = _dbSet.ApplyOrder(orderBy, methodName);
                IOrderedQueryable<TModel> orderByFunc(IQueryable<TModel> i) => orderQuery;
                return GetPaged(filter, orderByFunc, includeProperties, isIncludedIsDeleted);
            }
            return GetPaged(filter, null, includeProperties, isIncludedIsDeleted, page, pageSize);
        }

        public TModel FirstOrDefault(Expression<Func<TModel, bool>> filter = null,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
             string includeProperties = "", bool isIncludedIsDeleted = true)
        {
            return Get(filter, orderBy, includeProperties, isIncludedIsDeleted).FirstOrDefault();
        }

        public void Save(TModel entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(TModel entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(Expression<Func<TModel, bool>> filter = null)
        {
            foreach (var entity in _dbSet.Where(filter))
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void Transaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CurrentTransaction.Commit();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction.Rollback();
        }

        #region disposed

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}