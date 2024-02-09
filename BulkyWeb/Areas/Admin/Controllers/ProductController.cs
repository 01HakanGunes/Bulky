using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			return View(_unitOfWork.productRepo.GetAll());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product obj)
		{
			if (_unitOfWork.productRepo.Get(c => c.Title == obj.Title) != null)
			{
				ModelState.AddModelError("Title", "Product with the same title already exists!");
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.productRepo.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product created successfully!";
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

			Product? category = _unitOfWork.productRepo.Get(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			TempData["cacheTitle"] = category.Title;

			return View(category);
		}

		[HttpPost]
		public IActionResult Edit(Product obj)
		{
			string? cacheTitle = TempData["cacheTitle"] as string;

			if ((obj.Title != cacheTitle) && (_unitOfWork.productRepo.Get(c => c.Title == obj.Title) != null))
			{
				ModelState.AddModelError("Title", "Product with the same name already exists!");
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.productRepo.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product edited successfully!";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Remove(int id)
		{
			Product? category = _unitOfWork.productRepo.Get(c => c.Id == id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost]
		public IActionResult Remove(Product obj)
		{
			_unitOfWork.productRepo.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Product deleted successfully!";
			return RedirectToAction("Index");
		}
	}
}
