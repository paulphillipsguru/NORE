using PP.NORE.Builder.Contracts;
using PP.NORE.Shared.Models;
using ICodeCompiler = PP.NORE.Builder.Contracts.ICodeCompiler;
using PP.NORE.Builder.ReportBuilder;
namespace PP.NORE.Builder
{
    public class CodeBuilder(ITemplateManager templateManager, ICodeCompiler codeCompiler) : ICodeBuilder
    {
        public (bool, List<ErrorModel>?, byte[]?) BuildProject(ProjectConfig projectConfig)
        {
            ArgumentNullException.ThrowIfNull(projectConfig, nameof(projectConfig));

            var dataTemplate = templateManager.GetDataTemplate(projectConfig);

            if (!dataTemplate.Item1)
            {
                var errors = new List<ErrorModel>();
                errors.Add(new ErrorModel { ErrorType = "MODULE", Message = dataTemplate.Item2, Id = 0, IsBase = false });
                return (dataTemplate.Item1, errors, null);
            }

            var codeTemplate = templateManager.GetRuleTemplate(projectConfig);
           
            if (!codeTemplate.Item1) {
                var errors = new List<ErrorModel>();
                errors.Add(new ErrorModel { ErrorType = "MODULE", Message = codeTemplate.Item2, Id = 0, IsBase = false });
                return (codeTemplate.Item1, errors, null);
            }


            var finalCode = """

                using System;
                using PP.NORE.Shared.Contracts;
                using PP.NORE.Shared.Models;
                using System.Collections.Generic;
                using System.Collections.Frozen;
                using System.Linq;
                using Newtonsoft.Json;
                
                """;


            finalCode += dataTemplate.Item2 + "\n" + codeTemplate.Item2;

            var result = codeCompiler.CompileCode(projectConfig, finalCode);

            return result;

        }
         
    }
}
