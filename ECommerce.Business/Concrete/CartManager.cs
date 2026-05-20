using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.DTOs;

namespace ECommerce.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;

        private readonly IProductDal _productDal;

        public CartManager(ICartDal cartDal, IProductDal productDal)
        {
            _cartDal = cartDal;

            _productDal = productDal;
        }

        public Cart GetMyCart(int userId)
        {
            return _cartDal.GetByUserId(userId);
        }

        public void AddItem(int userId, AddCartItemDto dto)
        {
            Product product = _productDal.GetById(dto.ProductId);

            if (product == null)
            {
                throw new Exception("Ürün bulunamadı.");
            }

            if (dto.Quantity > product.Stock)
            {
                throw new Exception("Yeterli stok yok.");
            }

            Cart cart = _cartDal.GetByUserId(userId);

            CartItem? existingItem = cart.CartItems
                .FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + dto.Quantity;

                if (newQuantity > product.Stock)
                {
                    throw new Exception("Yeterli stok yok.");
                }

                existingItem.Quantity = newQuantity;

                _cartDal.UpdateCartItem(existingItem);
            }
            else
            {
                CartItem cartItem = new()
                {
                    CartId = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                _cartDal.AddCartItem(cartItem);
            }

            _cartDal.Save();
        }

        public void UpdateItemQuantity(int userId, int cartItemId, UpdateCartItemDto dto)
        {
            CartItem? cartItem = _cartDal.GetCartItem(cartItemId, userId);

            if (cartItem == null)
            {
                throw new Exception("Sepet öğesi bulunamadı.");
            }

            Product product = _productDal.GetById(cartItem.ProductId);

            if (dto.Quantity > product.Stock)
            {
                throw new Exception("Yeterli stok yok.");
            }

            cartItem.Quantity = dto.Quantity;

            _cartDal.UpdateCartItem(cartItem);

            _cartDal.Save();
        }

        public void RemoveItem(int userId, int cartItemId)
        {
            CartItem? cartItem = _cartDal.GetCartItem(cartItemId, userId);

            if (cartItem == null)
            {
                throw new Exception("Sepet öğesi bulunamadı.");
            }

            _cartDal.DeleteCartItem(cartItem);

            _cartDal.Save();
        }
    }
}
