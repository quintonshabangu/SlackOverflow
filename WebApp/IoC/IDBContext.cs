using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace WebApp.IoC
{
    public interface IDbContext : IDisposable
    {
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}