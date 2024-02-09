namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryRepository categoryRepo { get; }
		IProductRepository productRepo { get; }
		void Save();
	}
}