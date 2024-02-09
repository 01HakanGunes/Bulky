using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		public ICategoryRepository categoryRepo { get; private set; }
		public IProductRepository productRepo { get; private set; }

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			categoryRepo = new CategoryRepository(db);
			productRepo = new ProductRepository(db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}