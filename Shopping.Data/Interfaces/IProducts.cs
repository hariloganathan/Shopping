using Shopping.Core;
using Shopping.Core.Domain;

namespace Shopping.Data.Interfaces
{
    public interface IProducts
    {
        List<Product> GetavailableProducts();
    }
}
