using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Contexts;
using ECommerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete
{
    public class EfCartDal : ICartDal
    {
        private readonly ECommerceDbContext _context;

        public EfCartDal(ECommerceDbContext context)
        {
            _context = context;
        }

        public Cart GetByUserId(int userId)
        {
            Cart? cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };

                _context.Carts.Add(cart);

                _context.SaveChanges();
            }

            return cart;
        }

        public CartItem? GetCartItem(int cartItemId, int userId)
        {
            return _context.CartItems
                .Include(i => i.Cart)
                .FirstOrDefault(i => i.Id == cartItemId && i.Cart.UserId == userId);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
        }

        public void DeleteCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }

        public void ClearCartItems(int userId)
        {
            Cart cart = GetByUserId(userId);

            foreach (CartItem item in cart.CartItems.ToList())
            {
                _context.CartItems.Remove(item);
            }

            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
