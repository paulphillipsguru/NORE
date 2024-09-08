using PP.NORE.Builder.Contracts;
using PP.NORE.Portal.Module.BusinessObjects.Base;
using PP.NORE.Portal.Module.BusinessObjects.Products;
using PP.NORE.Portal.Module.Helpers;
using PP.NORE.Shared.Models;

namespace PP.NORE.Portal.Module.Core
{
	public class ProductBuilder(ICodeBuilder codeBuilder)
	{
		public (bool, List<ErrorModel>?, byte[]?) Process(Product product, IList<CodeTemplate> bluePrints)
		{
			
			// TO DO, GET TENENT INFO
			var modules = bluePrints.Select(p=> new CodeModule() { Code = p.Code, Template = p.Template, IsData = p.IsDataTemplate }).ToList();
			var project = new ProjectConfig { EntityName = "", InstanceName = "", ProductName = product.Code,
				CodeTemplate = modules};

			// If we have a base product, then we clone this into the new product
			if (product.ParentProduct != null)
			{
				if (product.ParentProduct.DataModel != null)
				{

					product.ParentProduct.DataModel.SaveToStream(project.DataModel);
				}

				foreach (var requiredLookup in product.ParentProduct.RequiredLookups)
				{
					project.RequiredLookups.Add(requiredLookup.Code);
				}

				foreach (var decisionRule in product.ParentProduct.Decisions)
				{
					
					project.DecisionRules.Add(new DecisionRuleModel { Code = decisionRule.Code, Algorithm = decisionRule.Algorithm, Description = decisionRule.Message.Clean(), 
						Id = decisionRule.Oid, RuleType = decisionRule.DecisionType, IsBase = true, Message = decisionRule.Message, OutComeStatus = decisionRule.OutComeStatus });
				}


				foreach (var step in product.ParentProduct.Steps)
				{
					RateStep rate = new() { Code = step.Code, Algorithm = step.Algorithm, Id = step.Oid, IsBase = true };

					foreach (var factor in step.Factors)
					{
						var ratingFactor = new RateFactor(factor.Code, factor.Algorithm);
						ratingFactor.IsBase = true;
						ratingFactor.Id = factor.Oid;
						rate.Factors.Add(ratingFactor);
					}

					project.Steps.Add(rate);
				}
			}

			// Now merge any product specific changes (merge)
			if (product.DataModel != null)
			{
				product.DataModel.SaveToStream(project.DataModel);
			}
            foreach (var requiredLookup in product.RequiredLookups)
            {
				if (!project.RequiredLookups.Any(p => p == requiredLookup.Code))
				{
					project.RequiredLookups.Add(requiredLookup.Code);
				}
            }

            foreach (var decisionRule in product.Decisions)
			{
				decisionRule.Status = "Success";
				decisionRule.ValidationMessage = "";
				var existingBasedecisionRule = project.DecisionRules.FirstOrDefault(p => p.Code == decisionRule.Code);
				if (existingBasedecisionRule != null)
				{
                    existingBasedecisionRule.Id = decisionRule.Oid;
                    existingBasedecisionRule.IsBase = false;
                    existingBasedecisionRule.Algorithm = decisionRule.Algorithm;
				}
				else
				{
					project.DecisionRules.Add(new DecisionRuleModel { Code = decisionRule.Code, Algorithm = decisionRule.Algorithm, 
						Description = decisionRule.Message, Id = decisionRule.Oid, RuleType = decisionRule.DecisionType, Message = decisionRule.Message,
                        OutComeStatus = decisionRule.OutComeStatus
                    });
				}

			}

			foreach (var step in product.Steps)
			{
				step.Status = "Success";
				step.ValidationMessage = "";
				var existingStep = project.Steps.FirstOrDefault(p => p.Code == step.Code);
				if (existingStep != null)
				{
					// As we are override the base step, this no longer
					// becomes a base step
					existingStep.Id = step.Oid;
				    
					existingStep.IsBase = false;
					existingStep.Algorithm = step.Algorithm;
					foreach (var factor in step.Factors)
					{
					
						var existingFactor = existingStep.Factors.FirstOrDefault(p => p.Code == factor.Code);
						existingFactor.Id = factor.Oid;
						
						if (existingFactor.Algorithm != null)
						{
							existingFactor.Id = factor.Oid;
							existingFactor.IsBase = true;
							existingFactor.Algorithm = factor.Algorithm;
						}
						else
						{
							var newFactor = new RateFactor(factor.Code, factor.Algorithm);
							newFactor.Id = factor.Oid;
							newFactor.IsBase = false;
							existingStep.Factors.Add(newFactor);
						}
					}
				}
				else
				{
					RateStep newStep = new() { Code = step.Code, Algorithm = step.Algorithm, Id = step.Oid, IsBase = false };
					step.Factors.Reload();
					foreach (var factor in step.Factors)
					{
						factor.ValidationMessage = "";
						factor.Status = "Success";
                        var newFactor = new RateFactor(factor.Code, factor.Algorithm)
                        {
                            Id = factor.Oid,
                            IsBase = false
                        };
                        newStep.Factors.Add(newFactor);
					}

					project.Steps.Add(newStep);

				}
			}

			product.ValidateMessage = "";

			var buildResult = codeBuilder.BuildProject(project);

			if (!buildResult.Item1)
			{

				
				foreach (var error in buildResult.Item2) {					
					if (!error.IsBase )
					{
						//Not base product
						switch (error.ErrorType.ToLower())
						{
							case "decision":
								var decision = product.Decisions.FirstOrDefault(p => p.Oid == error.Id);
                                decision.Status = "Failed";
                                decision.ValidationMessage = error.Message;								

                                break;						
							case "factor":
								var factorWithIssue = product.Steps.SelectMany(p=>p.Factors).Where(f=>f.Oid ==  error.Id).FirstOrDefault();
								factorWithIssue.ValidationMessage = error.Message;
								factorWithIssue.Status = "Failed";
								factorWithIssue.Steps.Status = "Failed";

                                factorWithIssue.Steps.ValidationMessage = $"One or more Factor(s) have failed, please review all factors";
								break;
							case "step":
								var step = product.Steps.FirstOrDefault(p => p.Oid == error.Id);
								step.Status = "Failed";
								step.ValidationMessage = error.Message;
								break;
							case "module":
								product.ValidateMessage = error.Message;
								break;
						

						}

					}
				}
			}

			return buildResult;

		}
	}
}

