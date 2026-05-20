using ECommerce.Entities.Concrete;

namespace ECommerce.DataAccess.Abstract
{
    public interface IProductDal
    {
        List<Product> GetAll();
        List<Product> Search(string keyword);

        List<Product> GetByCategory(int categoryId);
        Product GetById(int id);

        void Add(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}