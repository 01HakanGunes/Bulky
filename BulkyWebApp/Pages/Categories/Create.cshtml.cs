using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BulkyWebApp.Data;
using BulkyWebApp.Models;


namespace BulkyWebApp.Pages.Categories
{
	[BindProperties]
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }

		public CreateModel(ApplicationDbContext db)
		{
			_db = db;
		}

		public void OnGet()
		{

		}

		public IActionResult OnPost()
		{
			_db.Categories.Add(Category);
			_db.SaveChanges();

			return RedirectToPage("Index");
		}

	}
}