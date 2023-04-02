﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MicroserviceDuendeTemplate.Base.Entityes;
using MicroserviceDuendeTemplate.Base.Reposytory;

namespace MicroserviceDuendeTemplate.Base.UnitOfWork;

public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
{
    TContext DbContext { get; }

    Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks);

}

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;

    int SaveChanges();

    Task<int> SaveChangesAsync();

    void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);

    Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false);

    IDbContextTransaction BeginTransaction(bool useIfExists = false);

    void SetAutoDetectChanges(bool value);

    SaveChangesResult LastSaveChangesResult { get; }
}