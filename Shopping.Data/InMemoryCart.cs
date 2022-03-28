using Shopping.Core;
using Shopping.Core.Domain;
using Shopping.Data.Interfaces;

namespace Shopping.Data
{
    public class InMemoryCart : ICart
    {
        readonly List<CartItem> cartItems;
        public InMemoryCart()
        {

            cartItems = new List<CartItem>();
        }
        public List<CartItem> GetCartItems() => cartItems;
        public List<CartItem> AddToCart(Product? product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var cartItem = cartItems.SingleOrDefault(c => c.ProductId == product.ProductId);
            if (cartItem == null)
            {
                //create a new cart
                cartItem = new CartItem { ProductId = product.ProductId, Product = product, Quantity = 1 };
                cartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            return cartItems;
        }     
        public decimal CalculateTotal(List<CartItem>? cartItems)
        {
            decimal? total = decimal.Zero;
            total = (decimal?)(from items in cartItems select items.Quantity * items?.Product?.Price).Sum();
            return total ?? decimal.Zero;
        }
        public decimal CalculateDiscount(List<CartItem> cartItems, PromoCode? promocode)
        {
            if (promocode == null)
            {
                throw new ArgumentNullException(nameof(promocode));
            }
            decimal total = CalculateTotal(cartItems);
            return total * promocode.DicountPercentage;
        }
    }

}
