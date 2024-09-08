using PP.NORE.Shared.Models;

namespace PP.NORE.Shared.Contracts
{
    public interface IProductRater
    {
        RateResponse RateProduct(RateRequest rateRequest);
    }
}
