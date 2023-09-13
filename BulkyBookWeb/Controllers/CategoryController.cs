using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRipo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRipo = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _categoryRipo.GetAll().ToList();
            return View(objCategoryList);
        }
        //get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display order exactly match with name");
            }
            if (ModelState.IsValid) {
                _categoryRipo.Add(obj);
                _categoryRipo.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDb = _categoryRipo.Get(u => u.Id == id);
            if (CategoryFromDb == null) { 
                return NotFound();
            }
            return View(CategoryFromDb);
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display order exactly match with name");
            }
            if (ModelState.IsValid)
            {
                _categoryRipo.update(obj);
                _categoryRipo.Save();
                TempData["success"] = "Category Edit Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //public IActionResult delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return notfound();
        //    }
        //    var obj = _db.categories.find(id);
        //    _db.categories.remove(obj);
        //    _db.savechanges();
        //    return redirecttoaction("index");
        //}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CategoryFromDb = _categoryRipo.Get(u=>u.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        //post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _categoryRipo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRipo.Remove(obj);
            _categoryRipo.Save();
            TempData["success"] = "Category Delete Successfully";
            return RedirectToAction("Index");
        }
        
    }
}

