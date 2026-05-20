using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Contexts;
using ECommerce.Entities.Concrete;

namespace ECommerce.DataAccess.Concrete
{
    public class EfProductDal : IProductDal
    {
        private readonly ECommerceDbContext _context;

        public EfProductDal(ECommerceDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products
                .FirstOrDefault(x => x.Id == id)!;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);

            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);

            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);

            _context.SaveChanges();
        }
        public List<Product> Search(string keyword)
        {
            return _context.Products
                .Where(x => x.Name.Contains(keyword))
                .ToList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _context.Products
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }
    }
}