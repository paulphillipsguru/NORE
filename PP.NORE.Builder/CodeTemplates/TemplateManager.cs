
using Microsoft.Extensions.Configuration;
using PP.NORE.Builder.Contracts;
using PP.NORE.Builder.Lookup;
using PP.NORE.Builder.Models;
using PP.NORE.Shared.Models;
using Scriban;
using System.Text;
namespace PP.NORE.Builder.CodeTemplates
{
    public class TemplateManager() : ITemplateManager
    {        
        public (bool,string, LookupModel?) GetDataTemplate(ProjectConfig projectConfig)
        {
            ArgumentNullException.ThrowIfNull(projectConfig, nameof(projectConfig));

            if (projectConfig.CodeTemplate == null || projectConfig.CodeTemplate.Count() == 0)
            {
                return (false, "No Blueprints have been defined",null);
            }

            
            var lookupModel = new LookupModel
            {
                Models = projectConfig.CreateDataModesFromExcel()
            };

            foreach (var requiredLookup in projectConfig.RequiredLookups)
            {
                if (!lookupModel.Models.Any(p=>p.TableName == requiredLookup))
                {
                    return (false,"Missing Lookup " + requiredLookup, null);
                }
            }
            

            var dataTemplate = projectConfig.CodeTemplate.FirstOrDefault(p => p.IsData)?.Template;
            if (dataTemplate == null) {
                return (false, "No Data Blueprints have been defined", null);
            }


			var template = Template.Parse(dataTemplate).Render(lookupModel);

            return (true, template, lookupModel);
        }

        public (bool,string) GetRuleTemplate(ProjectConfig projectConfig)
        {
            var output = new StringBuilder();

            if (projectConfig.CodeTemplate == null || projectConfig.CodeTemplate.Count() == 0)
            {
                return (false, "No Blueprints have been defined");
            }

            foreach (var template in projectConfig.CodeTemplate.Where(p=>!p.IsData)) {
                output.AppendLine(Template.Parse(template.Template).Render(projectConfig));
            }


            return (true, output.ToString());
        }


    }
}
