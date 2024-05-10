namespace GallaryStore.Repositories
{
    public interface IRepository<T> where T : class 
    {
        public List<T> GetAll();
        public T GetById(int? id);
        public void update(T entity);
        public void delete(T entity);
        public void add(T entity);

        public T getElement(Func<T,bool>func, string? include);
        
        public List<T> getElements(Func<T,bool>func,string? include);


    }
}
