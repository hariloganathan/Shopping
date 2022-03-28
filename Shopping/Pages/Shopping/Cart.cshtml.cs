using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.Core.Domain;
using Shopping.Data.Interfaces;

namespace Shopping.Pages.Shopping
{
    public class CartModel : PageModel
    {
        private readonly ICart cartData;
        private readonly IPromocode promoCodeData;
        private readonly List<PromoCode> promocodes;
        public List<CartItem> cartItems;
        public Decimal Price;
        public Decimal TotalDiscount;

        [BindProperty]
        public string? Promocode { get; set; }


        public CartModel(ICart CartData, IPromocode PromoCodeData)
        {
            cartData = CartData;
            promoCodeData = PromoCodeData;
            cartItems = cartData.GetCartItems();
            promocodes = promoCodeData.GetPromocodes();
            Price = cartData.CalculateTotal(cartItems);
        }

        public void OnGet()
        {      
        }
        public IActionResult OnPost()
        {
            if (Promocode != null )
            {
                var promocode = promocodes.FirstOrDefault(p => p.Promocode == Promocode.ToUpper());
                if (promocode == null)
                {
                    ModelState.AddModelError("promocode", "Invalid Promocode");
                }
                TotalDiscount = cartData.CalculateDiscount(cartItems, promocodes.FirstOrDefault(p => p.Promocode == "TWENTYFIVE"));

            }
            return Page();
        }
    }
}
