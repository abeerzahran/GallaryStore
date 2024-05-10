using GallaryStore.dbContext;
using GallaryStore.Models;
using GallaryStore.Repositories;

namespace GallaryStore.UnitOfWork
{
    public class unitOfWork<T> where T:class  
    {
        GallaryContext db;
        private IRepository<T> repository;
        public unitOfWork(GallaryContext db)
        {
            this.db = db;
        }
        public IRepository<T> Repository { 
            get 
            { 
                return repository ?? (repository= new GenericRepository<T>(db));
            }
        }

        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}
