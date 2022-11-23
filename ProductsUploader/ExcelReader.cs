using Microsoft.Office.Interop.Excel;
using ProductsUploader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Range = Microsoft.Office.Interop.Excel.Range;
namespace ProductsUploader
{
    public class ExcelReader
    {
        public List<Product> ReadFile()
        {
            try
            {
                
                Application excelApp = new Application();

                if (excelApp == null)
                {
                    Console.WriteLine("Excel library is not installed!!");
                    return null;
                }
                
                //var path=Console.ReadLine();

                //if(string.IsNullOrEmpty(path) && !File.Exists(path))
                //{
                //    Console.WriteLine("Invalid Path Provide");
                //    Console.ReadLine();
                //}
                var validPath = Program.ExcelPath ;
                Workbook excelBook = excelApp.Workbooks.Open(validPath);
                _Worksheet excelSheet = excelBook.Sheets[1];
                Range excelRange = excelSheet.UsedRange;

                int rows = excelRange.Rows.Count;
                int cols = excelRange.Columns.Count;
                var productList=new List<Product>();
                for (int i = 1; i <= rows; i++)
                {
                    
                   // Console.Write("\r\n");
                    var product = new Product();
                    
                    for (int j = 1; j <= cols; j++)
                    {
                        var rangeValue = excelRange.Cells[i, j] as Range;

                        if (rangeValue != null && rangeValue.Value != null)

                        {
                            //Avoid Header
                             if(i != 1)
                            {
                                var data = rangeValue.Value.ToString();
                                //Name,Price,ReleaseDate
                                if (j == 1)
                                {
                                    product.Name = data;
                                }
                                if (j == 2)
                                {
                                    decimal.TryParse(data, out decimal price);
                                    product.Price = price;
                                }
                                if (j == 3)
                                {
                                    DateTime.TryParse(data, out DateTime releaseDate);
                                    product.ReleaseDate =releaseDate;
                                }
                            }
                            
                        }
                    }
                    if(product.Name!= null)
                    {
                        productList.Add(product);
                    }
                   
                }            
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                return productList;
                
            }
            catch (Exception ex) { 
              return null;
            }
            finally { }
        }
    }
}
