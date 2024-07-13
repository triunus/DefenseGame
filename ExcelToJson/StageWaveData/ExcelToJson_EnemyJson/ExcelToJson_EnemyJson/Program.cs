using OfficeOpenXml;
using Newtonsoft.Json;

namespace ExcelToJsonConverter
{
    public class ExcelToJson
    {
        public Dictionary<string, string> ReadBaseInformation(string filePath)
        {
            var baseInfo = new Dictionary<string, string>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["BaseInformation"];

                baseInfo.Add("JsonFileName", worksheet.Cells[2, 3].Text);
                baseInfo.Add("ExcelFilesPath", worksheet.Cells[3, 3].Text);
                baseInfo.Add("OutputPath", worksheet.Cells[4, 3].Text);
            }

            return baseInfo;
        }

        public Dictionary<string, List<Dictionary<string, object>>> ReadExcelFile(string filePath)
        {
            Dictionary<string, List<Dictionary<string, object>>> workbookData = new Dictionary<string, List<Dictionary<string, object>>>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    var sheetTableData = new List<Dictionary<string, object>>();

                    foreach (var table in worksheet.Tables)
                    {
                        var tablesData = new List<Dictionary<string, object>>();

                        int tableStartRow = table.Address.Start.Row;
                        int tableStartColumn = table.Address.Start.Column;

                        int rowCount = table.Address.Rows;
                        int colCount = table.Address.Columns;

                        for (int rowNumber = 1; rowNumber < rowCount; ++rowNumber)
                        {
                            var tableRowData = new Dictionary<string, object>();

                            for (int columnNumber = 0; columnNumber < colCount; ++columnNumber)
                            {
                                string header = worksheet.Cells[tableStartRow, tableStartColumn + columnNumber].Text;
                                string value = worksheet.Cells[tableStartRow + rowNumber, tableStartColumn + columnNumber].Text;

                                Console.WriteLine($"header : {header}, value : {value}");
                                tableRowData.Add(header, value);
                            }

                            tablesData.Add(tableRowData);
                        }

                        sheetTableData.Add(table.Name, tablesData);
                    }

                    workbookData.Add(worksheet.Name, sheetTableData);
                }
            }

            return workbookData;
        }
    }

    public class Program
    {
        public static void Main()
        {
            ExcelToJson excelToJson = new ExcelToJson();

            Dictionary<string, string> baseInfo = new Dictionary<string, string>();

            Dictionary <string,Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>> allExcelData = new Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, object>>>>>();

            string baseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseInformation.xlsx");
            baseInfo = excelToJson.ReadBaseInformation(baseFilePath);
            /*            foreach (var kvp in baseInfo)
                        {
                            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                        }*/
            if (!Directory.Exists(baseInfo["OutputPath"]))
            {
                Directory.CreateDirectory(baseInfo["OutputPath"]);
            }

            foreach (string excelFile in Directory.GetFiles(baseInfo["ExcelFilesPath"], "*.xlsx"))
            {
                string fileName = Path.GetFileNameWithoutExtension(excelFile);
                var excelData = excelToJson.ReadExcelFile(excelFile);
                allExcelData.Add(fileName, excelData);
            }

            string outputFilePath = Path.Combine(baseInfo["OutputPath"], $"{baseInfo["JsonFileName"]}.json");

            // JSON 데이터로 변환
            string jsonString = JsonConvert.SerializeObject(allExcelData, Formatting.Indented);
            File.WriteAllText(outputFilePath, jsonString);
        }
    }
}