using PP.NORE.Shared.Cache;
using PP.NORE.Shared.Contracts;
using PP.NORE.Shared.Models;

namespace PP.NORE.RatingEngine
{
    public class RatingProcessor : IProductRater
    {
        
        // Proxy to rating logic
        public RateResponse RateProduct(RateRequest rateRequest)
        {
            
            
            var rating =  (rateRequest.ProductBinary != null) ? RuleCache.CreateProductRaterFromBinary(rateRequest) 
                : RuleCache.GetProductRater(rateRequest,false);

            var result = rating.RateProduct(rateRequest);
            result.Debug = rateRequest.Debug;
            return result;
        }
    }
}
