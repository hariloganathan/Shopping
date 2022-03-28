using Shopping.Core.Domain;

namespace Shopping.Data.Interfaces
{
    public interface ICart
    {
        List<CartItem> GetCartItems();
        List<CartItem> AddToCart(Product? product);
        decimal CalculateTotal(List<CartItem>? cartItems);
        decimal CalculateDiscount(List<CartItem> cartItems,PromoCode? promocode);
    }
}
