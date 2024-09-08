using PP.NORE.Shared.Models;

namespace PP.NORE.Builder.Contracts
{
    public interface ICodeCompiler
    {
        (bool, List<ErrorModel>?, byte[]?) CompileCode(ProjectConfig config, string code);
    }
}
