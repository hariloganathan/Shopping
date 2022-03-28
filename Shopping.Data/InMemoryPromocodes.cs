using Shopping.Core.Domain;
using Shopping.Data.Interfaces;

namespace Shopping.Data
{
    public class InMemoryPromocodes: IPromocode
    {
        private readonly List<PromoCode> promocodes;
        public InMemoryPromocodes()
        {
            promocodes = new List<PromoCode>() { 
            new PromoCode{ Promocode="TWENTYFIVE",DicountPercentage=0.25m },
            new PromoCode{ Promocode="FIFTY",DicountPercentage=0.50m },
            new PromoCode{ Promocode="SEVENTYFIVE",DicountPercentage=0.75m }
            };
        }

        public List<PromoCode> GetPromocodes()
        {
            return promocodes.ToList();
        }
    }
}
