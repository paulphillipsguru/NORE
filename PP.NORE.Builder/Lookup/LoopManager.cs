using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using PP.NORE.Shared.Models;
using System.Data;

namespace PP.NORE.Builder.Lookup
{
	public static class LoopManager
	{
		public static List<DataModel> CreateDataModesFromExcel(this ProjectConfig config)
		{
			var results = new List<DataModel>();

			using var workBook = new Workbook();			

			workBook.LoadDocument(config.DataModel);

			foreach (Worksheet sheet in workBook.Sheets)
			{
				foreach (var table in sheet.Tables)
				{
					var dataTable = sheet.CreateDataTable(table.Range, true);

					var exporter = sheet.CreateDataTableExporter(table.Range, dataTable, true);

					exporter.Export();
					dataTable.TableName = table.Name;
					results.Add(dataTable.GetModel());
				}
			}

			return results;
		}

		public static DataModel GetModel(this DataTable dataTable)
		{
			var model = new DataModel();
			model.TableName = dataTable.TableName;
			foreach (DataColumn col in dataTable.Columns)
			{
				var colDict = new Dictionary<string, object>
				{
					{ "ColumnName", col.ColumnName },
					{ "DataType", col.DataType.ToString() }
				};
				model.ColumnInfo.Add(colDict);

			}

			foreach (DataRow row in dataTable.Rows)
			{
				var dict = new Dictionary<string, object>();
				foreach (DataColumn col in dataTable.Columns)
				{
					dict[col.ColumnName] = row[col];
				}
				model.Data.Add(dict);
			}
			return model;
		}
	}
}
