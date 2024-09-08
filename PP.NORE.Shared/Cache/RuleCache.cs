using PP.NORE.Shared.Contracts;
using PP.NORE.Shared.Models;
using System.Reflection;

namespace PP.NORE.Shared.Cache
{
    public static class RuleCache
    {
        static object lockCache = new();
        static Dictionary<string, IProductRater> cache = [];

        public static IProductRater GetProductRater(RateRequest rateRequest, bool useCache = true)
        {

            var key = Path.Combine(rateRequest.ProductDataPath, rateRequest.Entity, rateRequest.InstanceName, rateRequest.ProductCode, rateRequest.ProductCode + ".dll");
            if (useCache &&  cache.ContainsKey(key)) return cache[key];
            using var fileStream = new FileStream(key,FileMode.Open, FileAccess.Read, FileShare.Read);
               using var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            var product = Assembly.Load(memoryStream.ToArray());
            Type type = product.GetType("RatingEngine");

            object instance = Activator.CreateInstance(type);
            var productRaterInstance = (instance as IProductRater);
            if (useCache) cache.Add(key, productRaterInstance);

            fileStream.Close();
            fileStream.Dispose();
            memoryStream.Close();
            memoryStream.Dispose();

            return productRaterInstance;
        }

		public static IProductRater CreateProductRaterFromBinary(RateRequest rateRequest)
		{			
			var product = Assembly.Load(rateRequest.ProductBinary.ToArray());
			Type type = product.GetType("RatingEngine");

			object instance = Activator.CreateInstance(type);
			var productRaterInstance = (instance as IProductRater);
			

			return productRaterInstance;
		}


	}
}
