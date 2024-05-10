
using GallaryStore.dbContext;
using Microsoft.EntityFrameworkCore;

namespace GallaryStore.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class 
    {
        GallaryContext db;
        public GenericRepository(GallaryContext db)
        {
            this.db = db;
        }
        public void add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int? id)
        {
            return db.Set<T>().Find(id);
        }

        public T getElement(Func<T, bool> func, string? include)
        {
            if(include == null)
            {
                return db.Set<T>().Where(func).FirstOrDefault();
            }
            else
            {
                return db.Set<T>().Include(include).Where(func).FirstOrDefault();
            }
        }

        public List<T> getElements(Func<T, bool> func, string? include)
        {
            if(include == null)
            {
                return db.Set<T>().Where(func).ToList();
            }
            else
            {
                return db.Set<T>().Include(include).Where(func).ToList();
            }
        }

        public void update(T entity)
        {
            db.Set<T>().Update(entity); 
        }
        public void savechange()
        {
            db.SaveChanges();
        }
    }
}
