using Microsoft.AspNetCore.Mvc;
using SimpleToDoList.Repository;
using SimpleToDoList.Models;

namespace SimpleToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly IItemRepository _itemRepository;
        public TodoController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public IActionResult Index()
        {
            var items = _itemRepository.GetAll();
            return View(items);
        }
        [HttpPost]
        public IActionResult Create(string Title)
        {
            if (Title != null)
            {
                var newItem = new Models.Item
                {
                    Title = Title,
                    IsCompleted = false
                };
                _itemRepository.add(newItem);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _itemRepository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var olditem = _itemRepository.GetById(id);
            if (olditem != null)
            {
                return View(olditem);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _itemRepository.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                _itemRepository.Update(item);

                var items = _itemRepository.GetAll();
                return Json(new
                {
                    success = true,
                    isCompleted = item.IsCompleted,
                    total = items.Count,
                    completed = items.Count(i => i.IsCompleted),
                    pending = items.Count(i => !i.IsCompleted)
                });
            }
            return Json(new { success = false });
        }
    }
}
