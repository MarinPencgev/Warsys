using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
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
            if (_context.Transactions.Any())
            {
                return false;
            }

            var products = new List<Product>();
            var transactions = new List<Transaction>();

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

                        //var product = new Product
                        //{
                        //   Name = row[0].ToString(),
                        //   ProductCode = row[1].ToString(),
                        //   ExciseCode = row[2].ToString()
                        //};

                        var direction = _context.FlowDirections.FirstOrDefault(x => x.Direction == row[3].ToString());
                        var productId = _context.Products.FirstOrDefault(x => x.Name == row[4].ToString()).Id;

                        var transaction = new Transaction()
                        {
                            Sequence = row[0].ToString(),
                            StopTime = DateTime.Parse(row[1].ToString()),
                            DeviceId = row[2].ToString(),
                            Direction = direction,
                            ProductId = productId,
                            DocumentRefference = row[5].ToString(),
                            Volume = decimal.Parse(row[6].ToString()),
                            StdVolume = decimal.Parse(row[7].ToString()),
                            Mass = decimal.Parse(row[8].ToString()),
                            DensityT = decimal.Parse(row[9].ToString()),
                            Density15 = decimal.Parse(row[10].ToString()),
                            Temperature = decimal.Parse(row[11].ToString()),
                        };

                        transactions.Add(transaction);

                        //products.Add(product);
                    }
                }
            }

            //_context.Products.AddRange(products);
            _context.Transactions.AddRange(transactions);

            var result = _context.SaveChanges();

            return result > 0;
        }

        public bool SeedTest()
        {
            return false;
        }
    }
}
