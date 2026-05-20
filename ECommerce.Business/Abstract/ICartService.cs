using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Abstract
{
    public interface ICartService
    {
        Cart GetMyCart(int userId);

        void AddItem(int userId, AddCartItemDto dto);

        void UpdateItemQuantity(int userId, int cartItemId, UpdateCartItemDto dto);

        void RemoveItem(int userId, int cartItemId);
    }
}
