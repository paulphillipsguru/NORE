using PP.NORE.Builder.Contracts;
using PP.NORE.Shared.Models;
using Westwind.Scripting;

namespace PP.NORE.Builder.Compilers
{
    public class DefaultCompiler : ICodeCompiler
    {        public (bool, List<ErrorModel>?, byte[]?) CompileCode(ProjectConfig config, string code)
        {
            var script = new CSharpScriptExecution() 
            { 
                SaveGeneratedCode = false, 
                GeneratedClassName = $"{config.EntityName}.{config.InstanceName}.{config.ProductName}",
                DisableAssemblyCaching=true ,
                CompileWithDebug = false,
                ThrowExceptions = false
            };


            script.AddDefaultReferencesAndNamespaces();
            script.AddNetCoreDefaultReferences();
            script.AddLoadedReferences();
            script.AddAssembly(typeof(Newtonsoft.Json.JsonConvert));
            

            var dll = script.Compile(code);
            var errors = new List<ErrorModel>();
            var result = script.ErrorMessage;
            if (result != null)
            {
                var codeSplit = code.Split("\n");
                var errorLines = result.Split("\n");
                
                foreach (var line in errorLines)
                {
                    if (line.Contains("error"))
                    {
                        var lineNumber = int.Parse(line.Substring(1, line.IndexOf(",") - 1));
						//Find the header
						while (lineNumber>0)
						{
                            var lineText = codeSplit[lineNumber].Trim();
                            if (lineText.StartsWith("//"))
                            {
                                var text = lineText.Substring(2);
                                var temp = lineText.Split(":");

								errors.Add(new ErrorModel { Message = line.Split(":")[2], Id = int.Parse(temp[2]), ErrorType = temp[0].Substring(2), IsBase = bool.Parse(temp[1]) });
                                break;
								
                            }
							lineNumber--;
						}
					}
                }
				return (false, errors, null);

			}

			script = null;
            return (true, null ,dll);  
        }
    }
}
