﻿using System;
using System.Collections;
using RKSystem.DataAccess.MongoDB.Interfaces;

namespace RKSystem.DataAccess.MongoDB
{
    public class WriteUnitOfWork : IWriteUnitOfWork
    {
        private readonly Hashtable _hashRepository;

        public WriteUnitOfWork(MongoDbContext dbContext)
        {
            _hashRepository = new Hashtable();
            Context = dbContext;
        }

        private MongoDbContext Context { get; }

        public IWriteRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if (!_hashRepository.Contains(key))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);
                _hashRepository[key] = repository;
            }

            return (IWriteRepository<TEntity>) _hashRepository[key];
        }

        #region disposed

        private bool _disposed;

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                }
            _disposed = true;
        }

        #endregion
    }
}