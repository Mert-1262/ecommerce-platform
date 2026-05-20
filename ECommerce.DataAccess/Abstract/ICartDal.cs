using ECommerce.Entities.Concrete;

namespace ECommerce.DataAccess.Abstract
{
    public interface ICartDal
    {
        Cart GetByUserId(int userId);

        CartItem? GetCartItem(int cartItemId, int userId);

        void AddCartItem(CartItem cartItem);

        void UpdateCartItem(CartItem cartItem);

        void DeleteCartItem(CartItem cartItem);

        void ClearCartItems(int userId);

        void Save();
    }
}
