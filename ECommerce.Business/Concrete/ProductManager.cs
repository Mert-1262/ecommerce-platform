using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Delete(int id)
        {
            Product product = _productDal.GetById(id);

            _productDal.Delete(product);
        }
        public List<Product> Search(string keyword)
        {
            return _productDal.Search(keyword);
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _productDal.GetByCategory(categoryId);
        }

        public void UpdateStock(int id, int stock)
        {
            if (stock < 0)
            {
                throw new Exception("Stok 0'dan küçük olamaz.");
            }

            Product product = _productDal.GetById(id);

            if (product == null)
            {
                throw new Exception("Ürün bulunamadı.");
            }

            product.Stock = stock;

            _productDal.Update(product);
        }
    }
}