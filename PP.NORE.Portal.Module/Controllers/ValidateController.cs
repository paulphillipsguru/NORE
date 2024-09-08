using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using PP.NORE.Builder.Contracts;
using PP.NORE.Portal.Module.BusinessObjects.Base;
using PP.NORE.Portal.Module.BusinessObjects.Products;
using PP.NORE.Portal.Module.Core;
using PP.NORE.Shared.Cache;
using PP.NORE.Shared.Contracts;
using PP.NORE.Shared.Models;
using System.Text.Json;

namespace PP.NORE.Portal.Module.Controllers
{
    public partial class ValidateController : ViewController
	{		
		private readonly ICodeBuilder codeBuilder;
		private readonly IProductRater productRater;
		

		[ActivatorUtilitiesConstructor]
		public ValidateController(IServiceProvider serviceProvider) : this()
		{			
			codeBuilder = serviceProvider.GetService<ICodeBuilder>();
			productRater = serviceProvider.GetService<IProductRater>();

			InitializeComponent();
			


			var validateProduct = new SimpleAction(this, "ValidateProduct", PredefinedCategory.View)
			{
				Caption = "Validate",
				TargetObjectType = typeof(Product),
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject
			};
			validateProduct.Execute += ValidateProduct_Execute;

			
		}


		public ValidateController() {}



		private void ValidateProduct_Execute(object sender, SimpleActionExecuteEventArgs e)
		{
			var currentRecord = e.CurrentObject as Product;

			GenerateProduct(currentRecord);

			currentRecord.Save();
			ObjectSpace.CommitChanges();

			
		}

		protected void GenerateProduct(Product product)
		{
			if (product == null) {
				return;
			}

			

			var bluePrints = new XPCollection<CodeTemplate>(product.Session).Where(p=>p.IsDefault).ToList();
            foreach (var customBP in product.BluePrints)			
            {
				bluePrints.Add(customBP);
            }

            ProductBuilder productBuilder = new(codeBuilder);            

            var results = productBuilder.Process(product, bluePrints);
			if (results.Item1)
			{
				product.ProductPackage = new DevExpress.Persistent.BaseImpl.FileData(product.Session);
				var ratingLibMemoryStream = new MemoryStream(results.Item3);

                product.ProductPackage.LoadFromStream("Product.dll", ratingLibMemoryStream);
				product.Status = ProductStatus.Passed;
				var existing = product.Packages.FirstOrDefault(p => p.EffectiveTo == DateTime.MinValue);


                if (existing == null) {
					var newProductPackage = new ProductPackage(product.Session)
					{
						EffectiveFrom = DateTime.Now,
						LastUpdateDate = DateTime.Now,
						Package = new DevExpress.Persistent.BaseImpl.FileData(product.Session)
					};
					newProductPackage.Package.LoadFromStream("Product.dll", ratingLibMemoryStream);
					product.Packages.Add(newProductPackage);					
				} else
				{
                    existing.Package.LoadFromStream("Product.dll", new MemoryStream(results.Item3));
                }
                Dictionary<string, JsonElement> answers =[];

              
                foreach (var test in product.ProductTests)
                {
					RateRequest rateRequest = System.Text.Json.JsonSerializer.Deserialize<RateRequest>(test.TestPayLoad);
                    rateRequest.ProductBinary = ratingLibMemoryStream;

                    var rator = RuleCache.CreateProductRaterFromBinary(rateRequest);
					var result = rator.RateProduct(rateRequest);
					test.ActualStatus = result.Status;
					test.ActualPremium = result.TotalPremium;
					test.Message = result.Message;
                    test.Passed = (test.ActualStatus ==test.ExpectedStatus && test.ActualPremium == test.ExpectedPremium);
					if (!test.Passed)
					{
                        product.Status = ProductStatus.FailingTests;
                    }
					test.Save();

				}
			}
			else
			{
				product.Status = ProductStatus.HasIssues;
			}
			View.ObjectSpace.CommitChanges();
		}



	}
}
