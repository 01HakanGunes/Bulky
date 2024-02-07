using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;

		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			List<Category> objCategoryList = _db.Categories.ToList();
			return View(objCategoryList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (_db.Categories.FirstOrDefault(c => c.Name == obj.Name) != null)
			{
				ModelState.AddModelError("Name", "Category with the same name already exists!");
			}

			if (_db.Categories.FirstOrDefault(c => c.DisplayOrder == obj.DisplayOrder) != null)
			{
				ModelState.AddModelError("DisplayOrder", "Display order is occupied!");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully!";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? category = _db.Categories.Find(id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (_db.Categories.FirstOrDefault(c => c.Name == obj.Name) != null)
			{
				ModelState.AddModelError("Name", "Category with the same name already exists!");
			}

			if (_db.Categories.FirstOrDefault(c => c.DisplayOrder == obj.DisplayOrder) != null)
			{
				ModelState.AddModelError("DisplayOrder", "Display order is occupied!");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category edited successfully!";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Remove(int id)
		{
			Category? category = _db.Categories.FirstOrDefault(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost]
		public IActionResult Remove(Category obj)
		{
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
