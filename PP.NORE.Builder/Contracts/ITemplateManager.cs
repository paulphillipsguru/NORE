using PP.NORE.Builder.Models;
using PP.NORE.Shared.Models;

namespace PP.NORE.Builder.Contracts
{
    public interface ITemplateManager
    {
        (bool,string) GetRuleTemplate(ProjectConfig projectConfig);
        (bool, string, LookupModel?) GetDataTemplate(ProjectConfig projectConfig);
    }
}
