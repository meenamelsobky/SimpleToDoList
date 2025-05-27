namespace SimpleToDoList.Repository
{
    public interface IItemRepository
    {
        List<Models.Item> GetAll();
        void add(Models.Item item);
        void Update(Models.Item item);
        void Delete(int id);
        Models.Item GetById(int id);
    }
}
