using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();

        List<Product> Search(string keyword);

        List<Product> GetByCategory(int categoryId);

        Product GetById(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(int id);

        void UpdateStock(int id, int stock);
    }
}