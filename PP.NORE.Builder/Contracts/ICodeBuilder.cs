using PP.NORE.Shared.Models;

namespace PP.NORE.Builder.Contracts
{
    public  interface ICodeBuilder
    {
        (bool, List<ErrorModel>?, byte[]?) BuildProject(ProjectConfig projectConfig);
    }
}
