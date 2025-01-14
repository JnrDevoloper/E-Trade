﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ETrade.Devframework.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase <TEntity,TContext> : ETrade.Devframework.Core.DataAccess.IEntityRepository<TEntity>
        where TEntity:class,new ()
        where TContext :DbContext,new()

    {
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter  )
        {
            using (var context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() 
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter )
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
                
            }
        }
    }
}
