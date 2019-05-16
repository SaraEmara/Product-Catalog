using OfficeOpenXml;
using ProductCatalog.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductCatalog.Service.Services.ExportProductCatalogServices
{
    public static class ExportProductCatalog
    {
        public static byte[] ToExcel(IEnumerable<Product> Products) {

            byte[] result;

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook

                var worksheet = package.Workbook.Worksheets.Add("Current Product"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                int count = 0;
                foreach (PropertyInfo Prop in typeof(Product).GetProperties())
                {
                    worksheet.Cells[1, count + 1].Value = Prop.Name;
                    count++;
                }

                //Add values
                var j = 2;
                foreach (var Product in Products)
                {
                    worksheet.Cells["A" + j].Value = Product.ProductId;
                    worksheet.Cells["B" + j].Value = Product.ProductName;
                    worksheet.Cells["C" + j].Value = Product.ProductPhoto;
                    worksheet.Cells["D" + j].Value = Product.ProductPrice;
                    worksheet.Cells["E" + j].Value = Product.LastUpdatedDate.ToString("MM/dd/yyyy");

                    j++;
                }
                result = package.GetAsByteArray();
            }

            return result;
                
        }
        
    }
}
