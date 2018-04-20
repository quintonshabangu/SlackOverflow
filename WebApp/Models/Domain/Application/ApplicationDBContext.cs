using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.IoC;
using WebApp.Models.Domain;
using WebApp.Models.Domain.ReferenceModels;
using System.Data.Entity.Infrastructure;

namespace WebApp.Models
{
    public class ApplicationDbContext : IDbContext
    {
        public Database Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostType> PostTypes { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}