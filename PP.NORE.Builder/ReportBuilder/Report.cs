using Aspose.Words;
using Aspose.Words.Reporting;
using Newtonsoft.Json;
using PP.NORE.Builder.Models;
using System.Text;

namespace PP.NORE.Builder.ReportBuilder;

public class Report
{
    public byte[] GenerateReport(ReportModel report)
    {
        var document = new Document(new MemoryStream());

        var engine = new ReportingEngine();
        engine.BuildReport(document, ConvertToSchema(report));
        using var output = new MemoryStream();
        document.Save(output, SaveFormat.Docx);
        return output.ToArray();


    }

    public static JsonDataSource ConvertToSchema(dynamic obj)
    {
        var options = new JsonDataLoadOptions
        {
            AlwaysGenerateRootObject = true,
            SimpleValueParseMode = JsonSimpleValueParseMode.Loose
        };
        using var stringReader = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj)));
        var result = new JsonDataSource(stringReader, options);
        return result;
    }

}
