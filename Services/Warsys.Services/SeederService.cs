using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Warsys.Data;
using Warsys.Data.Models;

namespace Warsys.Services
{
    public class SeederService : ISeederService
    {
        private readonly WarsysDbContext _context;

        public SeederService(WarsysDbContext context)
        {
            _context = context;
        }
        public bool SeedFromExcel(string filePath)
        {
            if (_context.Products.Any())
            {
                return false;
            }

            var products = new List<Product>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IExcelDataReader reader = null;
                if (filePath.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (filePath.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                if (reader == null)
                    return false;

                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                foreach (DataTable table in dataSet.Tables)
                {
                    //Enumerate by rows or columns first at your preference
                    foreach (DataRow row in table.Rows)
                    {
                        var rowData = row.ItemArray.ToList();

                        var product = new Product()
                        {
                            Name = rowData[0].ToString(),
                            ProductCode = rowData[1].ToString()
                        };

                        products.Add(product);
                    }
                }
            }

            _context.Products.AddRange(products);

            var result = _context.SaveChanges();

            return result > 0;
        }

        public bool SeedTest()
        {
            return false;
        }
    }
}
