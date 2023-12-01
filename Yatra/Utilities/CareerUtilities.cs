using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.Utilities
{
    internal class CareerUtilities
    {
        public static List<CareerData> ReadExcelData(string excelFilePath, string sheetname)
        {
            List<CareerData> CareerDatalist = new List<CareerData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))

                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    var datatable = result.Tables[sheetname];
                    if (datatable != null)
                    {
                        foreach (DataRow row in datatable.Rows)
                        {
                            CareerData careerData = new CareerData
                            {
                                Name = GetValueOrDefault(row, "name"),
                                Mobile = GetValueOrDefault(row, "mobile"),
                                City = GetValueOrDefault(row, "city"),
                                EmailId = GetValueOrDefault(row, "email"),
                                Path= GetValueOrDefault(row,"linkedin"),
                                LinkedInUrl= GetValueOrDefault(row,"linkedin" )

                            };
                            CareerDatalist.Add(careerData);

                        }
                    }
                    else
                    {
                        Console.WriteLine($"sheet'{sheetname}' not found in the excel file");
                    }
                }
            }

            return CareerDatalist;
        }
        static string GetValueOrDefault(DataRow row, string columnName)
        {
            //Console.WriteLine(row + "" + columnName);
            return row.Table.Columns.Contains(columnName) ?
                row[columnName]?.ToString() : null;
        }
    }
}
