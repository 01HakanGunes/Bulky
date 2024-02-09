using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View(_unitOfWork.categoryRepo.GetAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (_unitOfWork.categoryRepo.Get(c => c.Name == obj.Name) != null)
			{
				ModelState.AddModelError("Name", "Category with the same name already exists!");
			}

			if (_unitOfWork.categoryRepo.Get(c => c.DisplayOrder == obj.DisplayOrder) != null)
			{
				ModelState.AddModelError("DisplayOrder", "Display order is occupied!");
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.categoryRepo.Add(obj);
				_unitOfWork.Save();
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

			Category? category = _unitOfWork.categoryRepo.Get(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			TempData["cacheName"] = category.Name;
			TempData["cacheDisplayOrder"] = category.DisplayOrder;

			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			string? cacheName = TempData["cacheName"] as string;
			int? cacheDisplayOrder = TempData["cacheDisplayOrder"] as int?;

			if ((obj.Name != cacheName) && (_unitOfWork.categoryRepo.Get(c => c.Name == obj.Name) != null))
			{
				ModelState.AddModelError("Name", "Category with the same name already exists!");
			}

			if ((obj.DisplayOrder != cacheDisplayOrder) && (_unitOfWork.categoryRepo.Get(c => c.DisplayOrder == obj.DisplayOrder) != null))
			{
				ModelState.AddModelError("DisplayOrder", "Display order is occupied!");
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.categoryRepo.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Category edited successfully!";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Remove(int id)
		{
			Category? category = _unitOfWork.categoryRepo.Get(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost]
		public IActionResult Remove(Category obj)
		{
			_unitOfWork.categoryRepo.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Category deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
