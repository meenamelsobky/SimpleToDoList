using SimpleToDoList.Models;
namespace SimpleToDoList.Repository
{
    public class ItemRepository : IItemRepository
    {
        List<Item> items;
        public ItemRepository(List<Item> _Items)
        {
            items = _Items;
        }
        public List<Item> GetAll()
        {
            return items.ToList();
        }
        public void add(Item item)
        {
            items.Add(item);
        }
        public void Update(Item item)
        {
            var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Title = item.Title;
                existingItem.IsCompleted = item.IsCompleted;
            }
        }
        public Item GetById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }
        public void Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
