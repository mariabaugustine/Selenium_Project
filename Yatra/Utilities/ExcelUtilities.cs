using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatra.Utilities
{
    internal class ExcelUtilities
    {
        public static List<SearchData> ReadExcelData(string excelFilePath, string sheetname)
        {
            List<SearchData> SearchDatalist = new List<SearchData>();
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
                            SearchData searchData = new SearchData
                            {
                                City = GetValueOrDefault(row, "city"),
                                CheckIn=GetValueOrDefault(row,"checkin"),
                                CheckOut=GetValueOrDefault(row,"checkout")

                            };
                            SearchDatalist.Add(searchData);

                        }
                    }
                    else
                    {
                        Console.WriteLine($"sheet'{sheetname}' not found in the excel file");
                    }
                }
            }

            return SearchDatalist;
        }
        static string GetValueOrDefault(DataRow row, string columnName)
        {
            //Console.WriteLine(row + "" + columnName);
            return row.Table.Columns.Contains(columnName) ?
                row[columnName]?.ToString() : null;
        }
    }
    //
    internal class PersonUtilities
    {
        public static List<TravellerData> ReadExcelData(string excelFilePath, string sheetname)
        {
            List<TravellerData> TravellerDatalist = new List<TravellerData>();
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
                            TravellerData searchData = new TravellerData
                            {
                                Email=GetValueOrDefault(row,"email"),
                                Fname=GetValueOrDefault(row,"fname"),
                                Lname=GetValueOrDefault(row,"lname"),
                                Mobile=GetValueOrDefault(row,"mob")
                                

                            };
                            TravellerDatalist.Add(searchData);

                        }
                    }
                    else
                    {
                        Console.WriteLine($"sheet'{sheetname}' not found in the excel file");
                    }
                }
            }

            return TravellerDatalist;
        }
        static string GetValueOrDefault(DataRow row, string columnName)
        {
           // Console.WriteLine(row + "" + columnName);
            return row.Table.Columns.Contains(columnName) ?
                row[columnName]?.ToString() : null;
        }
    }

}

