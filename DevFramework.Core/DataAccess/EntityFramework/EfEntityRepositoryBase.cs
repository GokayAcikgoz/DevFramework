using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                

                var addedEntity = context.Entry(entity); //ilgili nesneye abone olduk
                addedEntity.State = EntityState.Added; // durumunu eklenecek data olarak EF ye bildirdik.
                context.SaveChanges();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {

                context.Database.Connection.Open();
                var deletedEntity = context.Entry(entity);//ilgili nesneye abone olduk
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                
                //Tek bir nesne döndüreceğimiz için singleOrDefault diyerek filtremizi gonderiyoruz.
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                
                //Eger filter null sa context.set diyerek ilgili entitye abone oluyoruz. tolist diyerek döndürüyoruz.
                //filtre doluysa yine aynı şekilde abone oluyoruz. where diyip gönderdigimiz filtreyi döndürüyoruz.
                return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                
                var updatedEntity = context.Entry(entity); //ilgili nesneye abone olduk
                updatedEntity.State = EntityState.Modified; // modifed diyerek degistirildigini anlatıyoruz
                context.SaveChanges();
                return entity;
            }
        }
    }
}
