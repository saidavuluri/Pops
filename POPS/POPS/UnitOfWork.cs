﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS
{
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly MyDbContext _dbContext;
    //    #region Repositories
    //    public IRepository<Author> AuthorRepository =>
    //       new GenericRepository<Author>(_dbContext);
    //    public IRepository<Book> BookRepository =>
    //       new GenericRepository<Book>(_dbContext);
    //    #endregion
    //    public UnitOfWork(MyDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    public void Commit()
    //    {
    //        _dbContext.SaveChanges();
    //    }
    //    public void Dispose()
    //    {
    //        _dbContext.Dispose();
    //    }
    //    public void RejectChanges()
    //    {
    //        foreach (var entry in _dbContext.ChangeTracker.Entries()
    //              .Where(e => e.State != EntityState.Unchanged))
    //        {
    //            switch (entry.State)
    //            {
    //                case EntityState.Added:
    //                    entry.State = EntityState.Detached;
    //                    break;
    //                case EntityState.Modified:
    //                case EntityState.Deleted:
    //                    entry.Reload();
    //                    break;
    //            }
    //        }
    //    }
    //}
}