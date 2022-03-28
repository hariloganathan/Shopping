using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Core.Domain;
using Shopping.Data.Interfaces;

namespace Shopping.Pages.Shopping
{
    public class ListModel : PageModel
    {
        private readonly IProducts productsData;
        private readonly ICart cart;

        public IEnumerable<Product> Products { get; set; }
        public List<CartItem>? cartItems;
        public Decimal Price;

        [BindProperty]
        public int ProductId { get; set; }

        public ListModel(IProducts ProductsData, ICart Cart)
        {
            this.productsData = ProductsData;
            cart = Cart;
            Products = productsData.GetavailableProducts();
        }

        public void OnGet()
        {
            
        }
        public IActionResult OnPost()
        {
            List<CartItem> cartItems = cart.AddToCart(Products.First(p => p.ProductId == ProductId));
            Price = cart.CalculateTotal(cartItems);          
            return Page();
        }
    }
}
